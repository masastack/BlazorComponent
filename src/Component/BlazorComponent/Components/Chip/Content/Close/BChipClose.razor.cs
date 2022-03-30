namespace BlazorComponent
{
    public partial class BChipClose<TChip> where TChip : IChip
    {
        public bool Close => Component.Close;

        string CloseIcon => Component.CloseIcon;
    }
}
