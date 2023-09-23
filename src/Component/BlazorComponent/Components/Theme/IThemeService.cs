namespace BlazorComponent
{
    public interface IThemeService
    {
        Theme Theme { get; set; }

        bool Dark { get; set; }

        bool RTL { get; set; }

        event Action<bool>? DarkChanged;

        event Action<bool>? RTLChanged;

        event Action<Theme>? ThemeChanged;

        void UpdateTheme();

        void Register(BThemeProvider handle);

        void UnRegister(BThemeProvider handle);
    }
}
