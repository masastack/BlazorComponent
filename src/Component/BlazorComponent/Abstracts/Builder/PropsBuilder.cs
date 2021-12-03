using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PropsBuilder
    {
        private readonly Dictionary<string, object> _props = new();

        public PropsBuilder Add(string name, object value)
        {
            _props.Add(name, value);
            return this;
        }

        public PropsBuilder AddChildContent(RenderFragment childContent)
        {
            _props.Add("ChildContent", childContent);
            return this;
        }

        public PropsBuilder AddContent(string name, RenderFragment value)
        {
            _props.Add(name, value);
            return this;
        }

        public PropsBuilder AddContent<TContext>(string name,RenderFragment<TContext> content)
        {
            _props.Add(name, content);
            return this;
        }

        public PropsBuilder AddChildContents(params RenderFragment[] contents)
        {
            RenderFragment childContent = builder =>
            {
                for (int i = 0; i < contents.Length; i++)
                {
                    builder.AddContent(i, contents[i]);
                }
            };
            _props.Add("ChildContent", childContent);
            return this;
        }

        public PropsBuilder AddChildContent(string textContent)
        {
            return AddChildContent(builder => builder.AddContent(0, textContent));
        }

        public IReadOnlyDictionary<string, object> Props
        {
            get
            {
                return _props;
            }
        }
    }
}
