﻿using System.Runtime.CompilerServices;

namespace BlazorComponent;

public static class InvalidOperationExceptionExtensions
{
    public static void ThrowIfNull([NotNull]this object? parameter, string component, [CallerArgumentExpression("parameter")] string? parameterName = null)
    {
       _ = parameter ?? throw new InvalidOperationException($"The {component} component requires a non-null value for the '{parameterName}' parameter.");
    }
}
