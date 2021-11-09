using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPickerEdit
    {
        protected Dictionary<ColorTypes, List<(string, int, string)>> Modes = new()
        {
            {
                ColorTypes.RGBA,
                new List<(string, int, string)>
                {
                    ("R",255,"int"),
                    ("G",255,"int"),
                    ("B",255,"int"),
                    ("A", 1, "float")
                }
            },
            {
                ColorTypes.HSLA,
                new List<(string, int, string)>
                {
                    ("H",360,"int"),
                    ("S",1,"float"),
                    ("L", 1, "float"),
                    ("A", 1, "float")
                }
            },
            {
                ColorTypes.HEXA,
                null
            }
        };

        public List<(string Target, int Value, string Type)> CurrentMode
        {
            get
            {
                return Modes[Mode];
            }
        }

        [Parameter]
        public ColorTypes Mode { get; set; } = ColorTypes.RGBA;

        [Parameter]
        public ColorPickerColor Color { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public bool HideAlpha { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Dictionary<string, object> Attrs { get; set; }

        [Parameter]
        public EventCallback<ColorPickerColor> OnColorUpdate { get; set; }

        public Task HandleOnChange(ChangeEventArgs args)
        {
            if (OnColorUpdate.HasDelegate)
            {
                var hexValue = ColorUtils.ParseHex(args.Value.ToString());
                OnColorUpdate.InvokeAsync(ColorUtils.FromHexa(hexValue));
            }

            return Task.CompletedTask;
        }

        public async Task HandleOnInput(ChangeEventArgs args, string target, string type)
        {           
            var newVal = ParseValue(string.IsNullOrEmpty(args.Value.ToString()) ? "0" : args.Value.ToString(), type);

            if (OnColorUpdate.HasDelegate)
            {
                if (Mode == ColorTypes.RGBA)
                {
                    switch (target.ToUpper())
                    {
                        case "R":
                            Color.Rgba.R = newVal;
                            break;
                        case "G":
                            Color.Rgba.G = newVal;
                            break;
                        case "B":
                            Color.Rgba.B = newVal;
                            break;
                        case "A":
                            Color.Rgba.A = newVal;
                            break;
                        default:
                            break;
                    }

                   await OnColorUpdate.InvokeAsync(ColorUtils.FromRGBA(Color.Rgba));
                }
                else if (Mode == ColorTypes.HSLA)
                {
                    switch (target.ToUpper())
                    {
                        case "H":
                            Color.Hsla.H = newVal;
                            break;
                        case "S":
                            Color.Hsla.S = newVal;
                            break;
                        case "L":
                            Color.Hsla.L = newVal;
                            break;
                        case "A":
                            Color.Hsla.A = newVal;
                            break;
                        default:
                            break;
                    }
                   await OnColorUpdate.InvokeAsync(ColorUtils.FromHSLA(Color.Hsla));
                }
            }
        }

        private static double GetValue(double value, string type = "string")
        {
            if (type == "float") return Math.Round(value * 100, MidpointRounding.AwayFromZero) / 100;
            else if (type == "int") return Math.Round(value, MidpointRounding.AwayFromZero);
            else return 0;
        }

        private static double ParseValue(string value, string type)
        {
            if (type == "float") return double.Parse(value);
            else if (type == "int")
            {
                var isParse = int.TryParse(value, out var val);

                if (isParse) return val;
                else return 0;
            }
            else return 0;
        }
    }
}
