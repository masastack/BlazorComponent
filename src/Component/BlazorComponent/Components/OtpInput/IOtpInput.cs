namespace BlazorComponent
{
    public interface IOtpInput : IHasProviderComponent
    {
        OtpInputType Type { get; set; }

        bool Readonly { get; set; }

        bool Disabled { get; set; }

        Task OnPasteAsync(BOtpInputEventArgs<PasteWithDataEventArgs> args);

        List<ElementReference> InputRefs { get; set; }
    }
}
