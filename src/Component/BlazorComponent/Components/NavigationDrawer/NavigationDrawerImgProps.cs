using OneOf;

namespace BlazorComponent
{
    public class NavigationDrawerImgProps
    {
        public NavigationDrawerImgProps(string height, 
            string width, OneOf<string, SrcObject> src)
        { 
            Height = height;
            Width = width;
            Src = src;
        }

        public string Height { get; set; }

        public string Width { get; set; }

        public OneOf<string, SrcObject> Src { get; set; }
    }
}
