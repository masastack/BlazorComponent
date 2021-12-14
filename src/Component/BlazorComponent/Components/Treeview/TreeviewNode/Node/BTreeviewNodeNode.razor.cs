using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodeNode<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public EventCallback<MouseEventArgs> HandleOnClick => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnClick);

        public bool Selectable => Component.Selectable;

        public bool HasChildren => Component.HasChildren;

        public int Level => Component.Level;
    }
}
