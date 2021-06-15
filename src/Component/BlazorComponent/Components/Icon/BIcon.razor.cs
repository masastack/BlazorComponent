using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace BlazorComponent
{
    public partial class BIcon : BDomComponentBase
    {
        /// <summary>
        /// Attention! End with a space
        /// </summary>
        private static string[] _arrFa5Prefix = new string[] { "fa ", "fab ", "fal ", "far ", "fas " };

        protected string _icon;
        protected string _css;
        protected IconTag _tag = IconTag.I;

        private string Css => CssProvider.GetClass() + " " + _css;

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Size { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        private RenderFragment _childContent;
        [Parameter]
        public RenderFragment ChildContent
        {
            get
            {
                return _childContent;
            }
            set
            {
                var builder = new RenderTreeBuilder();
                value(builder);

                // TODO: will be changed next release version!
                var frame = builder.GetFrames().Array.FirstOrDefault(u => u.FrameType == RenderTreeFrameType.Text || u.FrameType == RenderTreeFrameType.Markup);

                if (frame.FrameType != RenderTreeFrameType.None)
                {
                    if (frame.TextContent.Contains("<svg"))
                    {
                        // support SVG
                        _icon = frame.TextContent;
                    }
                    else
                    {
                        _icon = frame.TextContent.Trim();

                        // support Font Awesome 5
                        if (_arrFa5Prefix.Any(prefix => _icon.StartsWith(prefix)))
                        {
                            _css = _icon;
                            _icon = null;
                        }
                        // support Material Design Icons
                        else if (_icon.StartsWith("mdi-"))
                        {
                            _css = $"mdi {_icon}";
                            _icon = null;
                        }
                        // support Material Design
                        else
                        {
                            _css = "material-icons";
                        }
                    }
                }

                _childContent = value;
            }
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            var builder = new RenderTreeBuilder();
            ChildContent(builder);

            // TODO: will be changed next release version!
            var frame = builder.GetFrames().Array.FirstOrDefault(u => u.FrameType == RenderTreeFrameType.Text || u.FrameType == RenderTreeFrameType.Markup);

            if (frame.FrameType != RenderTreeFrameType.None)
            {
                _tag = frame.FrameType != RenderTreeFrameType.None ? IconTag.Span : IconTag.I;
            }
        }
    }
}
