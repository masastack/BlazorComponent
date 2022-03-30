namespace BlazorComponent
{
    public partial class BTextFieldLegend<TValue, TInput> where TInput : ITextField<TValue>
    {
        protected string InnerHTML => Component.LegendInnerHTML;
    }
}
