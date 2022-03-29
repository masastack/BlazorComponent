using FluentValidation;
using FluentValidation.Results;
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
            private static readonly ConcurrentDictionary<Type, Action<object, ValidationMessageStore, FieldIdentifier>> _map;
            private static readonly List<Type> _types;

            static ValidationEventSubscriptions()
            {
                _map = new();
                _types = new();
                try
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var referenceAssemblys = assembly.GetReferencedAssemblies().Select(name => Assembly.Load(name)).ToList();
                    referenceAssemblys.Add(assembly);
                    foreach (var referenceAssembly in referenceAssemblys)
                    {
                        _types.AddRange(referenceAssembly.GetTypes().Where(t => t.BaseType?.IsGenericType == true && t.BaseType == typeof(AbstractValidator<>).MakeGenericType(t.BaseType.GenericTypeArguments[0])).ToArray());
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
                var type = _editContext.Model.GetType();
                if (_map.TryGetValue(type, out var validateAction) is false)
                {
                    var fluentValidatorType = _types.Where(t => t.BaseType == typeof(AbstractValidator<>).MakeGenericType(type)).FirstOrDefault();
                    if (fluentValidatorType is not null)
                    {
                        _map[type] = BuildFluentValidate(type, fluentValidatorType);
                    }
                    else
                    {
                        _map[type] = DataAnnotationsValidate;
                    }
                    validateAction = _map[type];
                }
                validateAction(_editContext.Model, _messageStore, field);
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

            private Action<object, ValidationMessageStore, FieldIdentifier> BuildFluentValidate(Type modelType, Type validatorType)
            {
                var model = Expr.BlockParam(typeof(object)).Convert(modelType);
                Var messageStore = Expr.Param<ValidationMessageStore>();
                var field = Expr.BlockParam<FieldIdentifier>();
                var validator = Expr.New(validatorType);
                var validationResult = validator.Method("Validate", model);
                Expr.IfThenElse(field[nameof(FieldIdentifier.FieldName)] == "", () =>
                {
                    messageStore.BlockMethod(nameof(ValidationMessageStore.Clear));
                    Expr.Foreach(validationResult[nameof(FluentValidationResult.Errors)], (item, @continue, @return) =>
                    {
                        var fieldIdentifier = Expr.New<FieldIdentifier>(model, item[nameof(ValidationFailure.PropertyName)]);
                        messageStore.BlockMethod(nameof(ValidationMessageStore.Add), fieldIdentifier, item[nameof(ValidationFailure.ErrorMessage)]);
                    });
                }, () =>
                {
                    messageStore.BlockMethod(nameof(ValidationMessageStore.Clear), field);
                    Expr.Foreach(validationResult[nameof(FluentValidationResult.Errors)], (item, @continue, @return) =>
                    {
                        var index = item[nameof(ValidationFailure.PropertyName)].Method(nameof(string.IndexOf), '.');
                        Expr.IfThen(item[nameof(ValidationFailure.PropertyName)].Method(nameof(string.Substring), index + 1) == field[nameof(FieldIdentifier.FieldName)], () =>
                        {
                            messageStore.BlockMethod(nameof(ValidationMessageStore.Add), field, item[nameof(ValidationFailure.ErrorMessage)]);
                            @return();
                        });
                    });
                });
                var validateAction = messageStore.BuildDefaultDelegate<Action<object, ValidationMessageStore, FieldIdentifier>>();
                return validateAction;
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
