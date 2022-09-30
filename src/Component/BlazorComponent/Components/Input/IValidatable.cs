﻿using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public interface IValidatable
    {
        FieldIdentifier ValueIdentifier { get; set; }

        bool Validate();

        void Reset();

        void ResetValidation();
        
        bool HasError { get; }
    }
}
