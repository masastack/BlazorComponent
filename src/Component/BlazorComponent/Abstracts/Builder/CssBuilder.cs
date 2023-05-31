﻿namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string? Class => GetClass();

        public string? GetClass()
        {
            var classList = Mapper
                            .Where(kv => kv.Value())
                            .Select(kv => kv.Key()?.Trim())
                            .Where(css => !string.IsNullOrWhiteSpace(css))
                            .Select(css =>
                            {
                                if (Prefix == null)
                                {
                                    return css;
                                }

                                return css!.StartsWith(Prefix) ? css : $"{Prefix}{css}";
                            })
                            .ToList();

            if (!classList.Any())
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }
    }
}
