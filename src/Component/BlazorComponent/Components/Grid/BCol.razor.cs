using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    using StringNumber = OneOf<string, int>;

    public class EmbeddedProperty
    {
        public StringNumber Span { get; set; }

        public StringNumber Pull { get; set; }

        public StringNumber Push { get; set; }

        public StringNumber Offset { get; set; }

        public StringNumber Order { get; set; }
    }

    public abstract partial class BCol : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber Flex { get; set; }

        [Parameter]
        public StringNumber Span { get; set; }

        [Parameter]
        public StringNumber Order { get; set; }

        [Parameter]
        public StringNumber Offset { get; set; }

        [Parameter]
        public StringNumber Push { get; set; }

        [Parameter]
        public StringNumber Pull { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Xs { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Sm { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Md { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Lg { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Xl { get; set; }

        [Parameter]
        public OneOf<int, EmbeddedProperty> Xxl { get; set; }

        [CascadingParameter]
        public BRow Row { get; set; }

        private string _hostFlexStyle;

        private string GutterStyle { get; set; }

        internal void RowGutterChanged((int horizontalGutter, int verticalGutter) gutter)
        {
            GutterStyle = "";
            if (gutter.horizontalGutter > 0)
            {
                GutterStyle = $"padding-left: {gutter.horizontalGutter / 2}px; padding-right: {gutter.horizontalGutter / 2}px;";
            }
        }

        private void SetHostClassMap()
        {
            var prefixCls = "ant-col";
            this.CssBuilder.Clear()
                .Add(prefixCls)
                .AddIf(() => $"{prefixCls}-{Span.Value}", () => Span.Value != null)
                .AddIf(() => $"{prefixCls}-order-{Order.Value}", () => Order.Value != null)
                .AddIf(() => $"{prefixCls}-offset-{Offset.Value}", () => Offset.Value != null)
                .AddIf(() => $"{prefixCls}-pull-{Pull.Value}", () => Pull.Value != null)
                .AddIf(() => $"{prefixCls}-push-{Push.Value}", () => Push.Value != null)
                ;

            SetSizeClassMapper(prefixCls, Xs, "xs");
            SetSizeClassMapper(prefixCls, Sm, "sm");
            SetSizeClassMapper(prefixCls, Md, "md");
            SetSizeClassMapper(prefixCls, Lg, "lg");
            SetSizeClassMapper(prefixCls, Xl, "xl");
            SetSizeClassMapper(prefixCls, Xxl, "xxl");
        }

        private void SetSizeClassMapper(string prefixCls, OneOf<int, EmbeddedProperty> parameter, string sizeName)
        {
            parameter.Switch(strNum =>
            {
                CssBuilder.AddIf($"{prefixCls}-{sizeName}-{strNum}", () => strNum > 0);
            }, embedded =>
            {
                CssBuilder
                    .AddIf(() => $"{prefixCls}-{sizeName}-{embedded.Span.Value}", () => embedded.Span.Value != null)
                    .AddIf(() => $"{prefixCls}-{sizeName}-order-{embedded.Order.Value}", () => embedded.Order.Value != null)
                    .AddIf(() => $"{prefixCls}-{sizeName}-offset-{embedded.Offset.Value}", () => embedded.Offset.Value != null)
                    .AddIf(() => $"{prefixCls}-{sizeName}-push-{embedded.Push.Value}", () => embedded.Push.Value != null)
                    .AddIf(() => $"{prefixCls}-{sizeName}-pull-{embedded.Pull.Value}", () => embedded.Pull.Value != null);
            });
        }

        private void SetHostFlexStyle()
        {
            if (this.Flex.Value == null)
                return;

            this._hostFlexStyle = this.Flex.Match(str =>
                {
                    if (Regex.Match(str, "^\\d+(\\.\\d+)?(px|em|rem|%)$").Success)
                    {
                        return $"flex: 0 0 {str}";
                    }

                    return $"flex: {str}";
                },
                num => $"flex: {num} {num} auto");
        }

        protected override void OnInitialized()
        {
            this.Row?.Cols.Add(this);

            this.SetHostClassMap();
            this.SetHostFlexStyle();

            base.OnInitialized();
        }

        protected override void Dispose(bool disposing)
        {
            this.Row?.Cols.Remove(this);

            base.Dispose(disposing);
        }
    }
}
