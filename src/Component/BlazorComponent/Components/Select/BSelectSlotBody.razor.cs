using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectSlotBody<TItem, TValue>
    {
        public List<string> Text => Input.Text;

        public bool Multiple => Input.Multiple;

        public bool Chips => Input.Chips;

        public List<TValue> Values => Input.Values;

        public string Icon => Input.Icon;

        public IReadOnlyList<TItem> Items => Input.Items;

        public Func<TItem, string> ItemText => Input.ItemText;

        public Func<TItem, TValue> ItemValue => Input.ItemValue;

        public Func<TItem, bool> ItemDisabled => Input.ItemDisabled;

        public ElementReference InputRef
        {
            get
            {
                return Input.InputRef;
            }
            set
            {
                Input.InputRef = value;
            }
        }

        public override Task HandleOnBlur(FocusEventArgs args)
        {
            Input.SetVisible(false);
            return base.HandleOnBlur(args);
        }
    }
}
