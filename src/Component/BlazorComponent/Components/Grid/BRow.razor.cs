namespace BlazorComponent
{
    public abstract partial class BRow : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [ApiDefaultValue("div")]
        public virtual string? Tag { get; set; } = "div";

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes>? Align { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes>? AlignContent { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes>? Justify { get; set; }
    }
}