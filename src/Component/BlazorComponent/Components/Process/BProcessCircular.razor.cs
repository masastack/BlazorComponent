using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    using StringNumber = OneOf<string, int>;

    public abstract partial class BProcessCircular : BDomComponentBase
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber? Size { get; set; } = 32;

        [Parameter]
        public StringNumber? Rotate { get; set; } = 0;

        /// <summary>
        /// TODO: 延迟
        /// </summary>
        [Parameter]
        public int Delay { get; set; }

        /// <summary>
        /// TODO: 自定义描述文案
        /// </summary>
        [Parameter]
        public string Tip { get; set; }

        /// <summary>
        /// 加载指示符
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void SetComponentClass()
        {
            CssBuilder
                .Add("m-progress-circular")
                .AddIf("m-progress-circular--indeterminate", () => Indeterminate);

            StyleBuilder
                .AddIf(() => Size.Value.Match(
                        str => $"height: {str}; width: {str}",
                        num => $"height: {num}px; width: {num}px"),
                    () => Size.HasValue);

            if (!string.IsNullOrWhiteSpace(Color))
            {
                if (Color.StartsWith("#"))
                {
                    StyleBuilder.Add($"color: {Color}");
                }
                else
                {
                    CssBuilder.Add($"{Color}--text");
                }
            }
        }
    }
}
