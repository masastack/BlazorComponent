namespace BlazorComponent
{
    public static class PickerAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyPickerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BPickerTitle<>), typeof(BPickerTitle<IPicker>))
                .Apply(typeof(BPickerBody<>), typeof(BPickerBody<IPicker>))
                .Apply(typeof(BPickerBodyTransition<>), typeof(BPickerBodyTransition<IPicker>))
                .Apply(typeof(BPickerActions<>), typeof(BPickerActions<IPicker>));
        }
    }
}
