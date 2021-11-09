using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltipContent<TTooltip> where TTooltip : ITooltip
    {
        public ElementReference ContentRef
        {
            set { Component.ContentRef = value; }
        }

        protected string TransitionName
        {
            get
            {
                if (!string.IsNullOrEmpty(Transition))
                {
                    return Transition;
                }
                else
                {
                    return Value ? "scale-transition" : "fade-transition";
                }
            }
        }

        public bool Value => Component.Value;

        public bool ShowContent => Component.ShowContent;

        public string Transition => Component.Transition;

        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}