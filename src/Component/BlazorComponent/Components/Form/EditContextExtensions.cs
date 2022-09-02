using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Options;
using Util.Reflection.Expressions;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace BlazorComponent
{
    public static class EditContextExtensions
    {
        /// <summary>
        /// Enables DataAnnotations validation support for the <see cref="EditContext"/>.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/>.</param>
        /// <returns>A disposable object whose disposal will remove DataAnnotations validation support from the <see cref="EditContext"/>.</returns>
        public static IDisposable EnableValidation(this EditContext editContext, IServiceProvider serviceProvider, bool enableI18n)
        {
            return new ValidationEventSubscriptions(editContext, serviceProvider, enableI18n);
        }

        private class ValidationMessageStorer
        {
            private ValidationMessageStore Store { get; set; }
            public List<FieldIdentifier> Fields { get; } = new();

            public ValidationMessageStorer(EditContext context)
            {
                Store = new ValidationMessageStore(context);
            }

            public void Add(in FieldIdentifier fieldIdentifier, IEnumerable<string> messages)
            {
                Fields.Add(fieldIdentifier);
                Store.Add(fieldIdentifier, messages);
            }

            public void Add(in FieldIdentifier fieldIdentifier, string message)
            {
                Fields.Add(fieldIdentifier);
                Store.Add(fieldIdentifier, message);
            }

            public void Clear(in FieldIdentifier fieldIdentifier)
            {
                Fields.Remove(fieldIdentifier);
                Store.Clear(fieldIdentifier);
            }

            public void Clear()
            {
                Fields.Clear();
                Store.Clear();
            }

            public bool Contains(FieldIdentifier fieldIdentifier)
            {
                return Fields.Contains(fieldIdentifier);
            }
        }

        private sealed class ValidationEventSubscriptions : IDisposable
        {
            private static readonly ConcurrentDictionary<Type, Func<IServiceProvider, object, FluentValidationResult>> _validationResultMap;
            private static readonly ConcurrentDictionary<Type, Func<object, Dictionary<string, object>>> _modelPropertiesMap;
            private static readonly Dictionary<Type, Type> _fluentValidationTypeMap;

            static ValidationEventSubscriptions()
            {
                _validationResultMap = new();
                _modelPropertiesMap = new();
                _fluentValidationTypeMap = new();
                try
                {
                    var referenceAssemblys = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var referenceAssembly in referenceAssemblys)
                    {
                        if (referenceAssembly.FullName.StartsWith("Microsoft.") || referenceAssembly.FullName.StartsWith("System."))
                            continue;

                        var types = referenceAssembly.GetTypes().Where(t =>
                            t.BaseType?.IsGenericType == true && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)).ToArray();
                        foreach (var type in types)
                        {
                            var modelType = type.BaseType.GenericTypeArguments[0];
                            var validatoType = typeof(IValidator<>).MakeGenericType(modelType);
                            _fluentValidationTypeMap.Add(modelType, validatoType);
                        }
                    }
                }
                catch
                {
                }
            }

            private readonly EditContext _editContext;
            private readonly ValidationMessageStorer _messageStore;
            private readonly IServiceProvider _serviceProvider;
            private I18n.I18n? _i18n;


            [MemberNotNullWhen(true, nameof(_i18n))]
            private bool EnableI18n { get; set; }

            public ValidationEventSubscriptions(EditContext editContext, IServiceProvider serviceProvider, bool enableI18n)
            {
                _serviceProvider = serviceProvider;
                _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
                _messageStore = new ValidationMessageStorer(_editContext);

                _editContext.OnFieldChanged += OnFieldChanged;
                _editContext.OnValidationRequested += OnValidationRequested;
                EnableI18n = enableI18n;
                if (EnableI18n)
                {
                    _i18n = _serviceProvider.GetService<I18n.I18n>();
                }
            }

            private void OnFieldChanged(object? sender, FieldChangedEventArgs eventArgs)
            {
                Validate(eventArgs.FieldIdentifier);
            }

            private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
            {
                Validate(new FieldIdentifier(new(), ""));
            }

            private void Validate(FieldIdentifier field)
            {
                if (_fluentValidationTypeMap.ContainsKey(_editContext.Model.GetType()))
                {
                    FluentValidate(_editContext.Model, _messageStore, field);
                }
                else
                {
                    DataAnnotationsValidate(_editContext.Model, _messageStore, field);
                }

                _editContext.NotifyValidationStateChanged();
            }

            private void DataAnnotationsValidate(object model, ValidationMessageStorer messageStore, FieldIdentifier field)
            {
                var validationResults = new List<ValidationResult>();
                if (field.FieldName == "")
                {
                    var validationContext = new ValidationContext(model);
                    Validator.TryValidateObject(model, validationContext, validationResults, true);
                    messageStore.Clear();

                    foreach (var validationResult in validationResults)
                    {
                        if (validationResult == null)
                        {
                            continue;
                        }

                        if (validationResult is EnumerableValidationResult enumerableValidationResult)
                        {
                            foreach (var descriptor in enumerableValidationResult.Descriptors)
                            {
                                foreach (var result in descriptor.Results)
                                {
                                    foreach (var memberName in result.MemberNames)
                                    {
                                        AddValidationMessage(new FieldIdentifier(descriptor.ObjectInstance, memberName), result.ErrorMessage!);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var memberName in validationResult.MemberNames)
                            {
                                AddValidationMessage(new FieldIdentifier(model, memberName), validationResult.ErrorMessage);
                            }
                        }
                    }
                }
                else
                {
                    var validationContext = new ValidationContext(model);
                    Validator.TryValidateObject(field.Model, validationContext, validationResults, true);

                    var validationFields = validationResults.Select(r => r.MemberNames.First()).ToList();
                    var prevFields = messageStore.Fields.Select(f => f.FieldName).ToList();

                    Console.WriteLine($"validationFields:{string.Join(',', validationFields)}");
                    Console.WriteLine($"prevFields:{string.Join(',', prevFields)}");

                    validationFields.Except(prevFields).ForEach(f =>
                    {
                        var result = validationResults.First(r => r.MemberNames.Contains(f));

                        messageStore.Add(_editContext.Field(f), result.ErrorMessage);
                        Console.WriteLine($"add field:{f}");
                    });

                    prevFields.Except(validationFields).ForEach(f =>
                    {
                        messageStore.Clear(_editContext.Field(f));
                        Console.WriteLine($"remove field:{f}");
                    });

                    // foreach (var validationResult in validationResults)
                    // {
                    //     var fieldName = validationResult.MemberNames.First();
                    //     var fieldIdentifier = _editContext.Field(fieldName);
                    //
                    //     messageStore.Add(fieldIdentifier, validationResult.ErrorMessage);
                    // }
                }
            }

            private void FluentValidate(object model, ValidationMessageStorer messageStore, FieldIdentifier field)
            {
                var validationResult = GetValidationResult(model);
                if (field.FieldName == "")
                {
                    messageStore.Clear();
                    var propertyMap = GetPropertyMap(model);
                    foreach (var error in validationResult.Errors)
                    {
                        if (error.PropertyName.Contains("."))
                        {
                            var propertyName = error.PropertyName.Substring(0, error.PropertyName.IndexOf('.'));
                            if (propertyMap.ContainsKey(propertyName))
                            {
                                var modelItem = propertyMap[propertyName];
                                var modelItemPropertyName = error.FormattedMessagePlaceholderValues["PropertyName"].ToString().Replace(" ", "");
                                AddValidationMessage(new FieldIdentifier(modelItem, modelItemPropertyName), error.ErrorMessage);
                            }
                        }
                        else
                        {
                            AddValidationMessage(new FieldIdentifier(model, error.PropertyName), error.ErrorMessage);
                        }
                    }
                }
                else
                {
                    messageStore.Clear(field);
                    if (field.Model == model)
                    {
                        var error = validationResult.Errors.FirstOrDefault(e => e.PropertyName == field.FieldName);
                        if (error is not null)
                        {
                            AddValidationMessage(field, error.ErrorMessage);
                        }
                    }
                    else
                    {
                        var propertyMap = GetPropertyMap(model);
                        var key = propertyMap.FirstOrDefault(pm => pm.Value == field.Model).Key;
                        var errorMessage = validationResult.Errors.FirstOrDefault(e => e.PropertyName == ($"{key}.{field.FieldName}"))?.ErrorMessage;
                        if (errorMessage is not null)
                        {
                            AddValidationMessage(field, errorMessage);
                        }
                    }
                }
            }

            private FluentValidationResult GetValidationResult(object model)
            {
                var type = model.GetType();
                if (_validationResultMap.TryGetValue(type, out var func) is false)
                {
                    var validatorType = _fluentValidationTypeMap[type];
                    var serviceProvider = Expr.BlockParam<IServiceProvider>();
                    var validator = serviceProvider.Method("GetService", Expr.Constant(validatorType)).Convert(validatorType);
                    var modelParamter = Expr.BlockParam(typeof(object)).Convert(type);
                    Var validationResult = validator.Method("Validate", modelParamter);
                    func = validationResult.BuildDefaultDelegate<Func<IServiceProvider, object, FluentValidationResult>>();
                    _validationResultMap[type] = func;
                }

                return func(_serviceProvider, model);
            }

            private Dictionary<string, object> GetPropertyMap(object model)
            {
                var type = model.GetType();
                if (_modelPropertiesMap.TryGetValue(type, out var func) is false)
                {
                    var modelParamter = Expr.BlockParam<object>().Convert(type);
                    Var map = Expr.New<Dictionary<string, object>>();
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                            continue;
                        else
                        {
                            if (property.PropertyType.GetInterfaces().Any(gt => gt == typeof(System.Collections.IEnumerable)))
                            {
                                Var index = -1;
                                Expr.Foreach(modelParamter[property.Name], (item, @continue, @return) =>
                                {
                                    index++;
                                    map[property.Name + "[" + index + "]"] = item.Convert<object>();
                                });
                            }
                            else
                            {
                                map[$"[{property.Name}]"] = modelParamter[property.Name].Convert<object>();
                            }
                        }
                    }

                    func = map.BuildDelegate<Func<object, Dictionary<string, object>>>();
                    _modelPropertiesMap[type] = func;
                }

                return func(model);
            }

            private void AddValidationMessage(in FieldIdentifier fieldIdentifier, string message)
            {
                if (EnableI18n)
                {
                    message = _i18n.T(message, true);
                }

                _messageStore.Add(fieldIdentifier, message);
            }

            public void Dispose()
            {
                _messageStore.Clear();
                _editContext.OnFieldChanged -= OnFieldChanged;
                _editContext.OnValidationRequested -= OnValidationRequested;
                _editContext.NotifyValidationStateChanged();
            }
        }
    }
}
