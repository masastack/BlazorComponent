using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BIcon : BDomComponentBase, IIcon, IThemeable, ISizeable,IColorable
    {
        /// <summary>
        /// Attention! End with a space
        /// </summary>
        private static string[] _arrFa5Prefix = new string[] { "fa ", "fab ", "fal ", "far ", "fas " };

        protected string _icon;
        protected string _css;
        protected IconTag _tag;
        private RenderFragment _childContent;

        private string Css => CssProvider.GetClass() + " " + _css;


        [Parameter]
        public IconTag Tag
        {
            get => _tag == IconTag.None ? IconTag.I : _tag;
            set => _tag = value;
        }

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }


        protected override void OnParametersSet()
        {
            if (Click.HasDelegate)
            {
                OnClick = Click;
            }
        }

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


        #region IIcon

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringNumber Size { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }
    

        public Task HandleOnClick(MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISizeable

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        #endregion

        #region  IThemeable

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        #endregion

        #region IColorable

        [Parameter]
        public string Color { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (_tag == IconTag.None)
            {
                var builder = new RenderTreeBuilder();
                ChildContent(builder);

                // TODO: will be changed next release version!
                var frame = builder.GetFrames().Array.FirstOrDefault(u => u.FrameType == RenderTreeFrameType.Text || u.FrameType == RenderTreeFrameType.Markup);

                if (frame.FrameType != RenderTreeFrameType.None)
                {
                    _tag = frame.TextContent.Contains("<svg") ? IconTag.Span : IconTag.I;
                }
            }
        }


        //protected override void BuildRenderTree(RenderTreeBuilder builder)
        //{
        //    //ChildContent?.Invoke(builder);

        //    base.BuildRenderTree(builder);
        //}

    }
}
