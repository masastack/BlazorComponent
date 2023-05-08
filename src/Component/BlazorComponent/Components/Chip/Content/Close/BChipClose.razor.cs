namespace BlazorComponent
{
    public partial class BChipClose<TChip> where TChip : IChip
    {
        private bool Close => Component.Close;

        private string? CloseIcon => Component.CloseIcon;
    }
}
