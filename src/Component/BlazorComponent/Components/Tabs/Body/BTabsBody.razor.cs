namespace BlazorComponent
{
    public partial class BTabsBody<TTabs> : ComponentPartBase<TTabs>
        where TTabs : ITabs
    {
        List<ITabItem> TabItems => Component.TabItems;

        StringNumber Value => Component.Value;
    }
}
