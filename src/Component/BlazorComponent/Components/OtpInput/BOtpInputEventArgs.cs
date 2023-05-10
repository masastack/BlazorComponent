namespace BlazorComponent
{
    public class BOtpInputEventArgs<T> where T : EventArgs
    {
        public T Args { get; set; }

        public int Index { get; set; }

        public BOtpInputEventArgs(T args)
        {
            Args = args;
        }

        public BOtpInputEventArgs(T args, int index)
        {
            Args = args;
            Index = index;
        }

    }
}
