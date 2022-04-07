using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
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
        public static IDisposable EnableValidation(this EditContext editContext)
        {
            return new ValidationEventSubscriptions(editContext);
        }

        private sealed class ValidationEventSubscriptions : IDisposable
        {
            private static readonly ConcurrentDictionary<Type, Func<object, FluentValidationResult>> _validationResultMap;
            private static readonly ConcurrentDictionary<Type, Func<object, Dictionary<string, object>>> _modelPropertiesMap;
            private static readonly Dictionary<Type, Type> _fluentValidationTypeMap;

            static ValidationEventSubscriptions()
            {
                _validationResultMap = new();
                _modelPropertiesMap = new();
                _fluentValidationTypeMap = new();
                try
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var referenceAssemblys = assembly.GetReferencedAssemblies().Select(name => Assembly.Load(name)).ToList();
                    referenceAssemblys.Add(assembly);
                    foreach (var referenceAssembly in referenceAssemblys)
                    {
                        var types = referenceAssembly.GetTypes().Where(t => t.BaseType?.IsGenericType == true && t.BaseType == typeof(AbstractValidator<>).MakeGenericType(t.BaseType.GenericTypeArguments[0])).ToArray();
                        foreach (var type in types)
                        {
                            _fluentValidationTypeMap.Add(type.BaseType.GenericTypeArguments[0], type);
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }

            private readonly EditContext _editContext;
            private readonly ValidationMessageStore _messageStore;

            public ValidationEventSubscriptions(EditContext editContext)
            {
                _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
                _messageStore = new ValidationMessageStore(_editContext);

                _editContext.OnFieldChanged += OnFieldChanged;
                _editContext.OnValidationRequested += OnValidationRequested;
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

            private void DataAnnotationsValidate(object model, ValidationMessageStore messageStore, FieldIdentifier field)
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
                                        messageStore.Add(new FieldIdentifier(descriptor.ObjectInstance, memberName), result.ErrorMessage!);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var memberName in validationResult.MemberNames)
                            {
                                messageStore.Add(new FieldIdentifier(model, memberName), validationResult.ErrorMessage);
                            }
                        }
                    }
                }
                else
                {
                    var validationContext = new ValidationContext(field.Model);
                    Validator.TryValidateObject(field.Model, validationContext, validationResults, true);
                    messageStore.Clear(field);
                    foreach (var validationResult in validationResults)
                    {
                        if (validationResult.MemberNames.Contains(field.FieldName))
                        {
                            messageStore.Add(field, validationResult.ErrorMessage);
                            return;
                        }
                    }
                }
            }

            private void FluentValidate(object model, ValidationMessageStore messageStore, FieldIdentifier field)
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
                                var modelItemPropertyName = error.FormattedMessagePlaceholderValues["PropertyName"].ToString().Replace(" ","");
                                messageStore.Add(new FieldIdentifier(modelItem, modelItemPropertyName), error.ErrorMessage);
                            }
                        }
                        else
                        {
                            messageStore.Add(new FieldIdentifier(model, error.PropertyName), error.ErrorMessage);
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
                            messageStore.Add(field, error.ErrorMessage);
                        }
                    }
                    else
                    {
                        var propertyMap = GetPropertyMap(model);
                        var key = propertyMap.FirstOrDefault(pm => pm.Value == field.Model).Key;
                        var errorMessage = validationResult.Errors.FirstOrDefault(e => e.PropertyName == ($"{key}.{field.FieldName}"))?.ErrorMessage;
                        if (errorMessage is not null)
                        {
                            messageStore.Add(field, errorMessage);
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
                    var modelParamter = Expr.BlockParam(typeof(object)).Convert(type);
                    var validator = Expr.New(validatorType);
                    Var validationResult = validator.Method("Validate", modelParamter);
                    func = validationResult.BuildDefaultDelegate<Func<object, FluentValidationResult>>();
                    _validationResultMap[type] = func;
                }
                return func(model);
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
