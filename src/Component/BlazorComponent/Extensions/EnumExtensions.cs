using System;
using System.ComponentModel;

namespace BlazorComponent.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum @enum)
        {
            return GetDescription(@enum);
        }

        private static string GetDescription(Enum enumValue)
        {
            var value = enumValue.ToString();

            var field = enumValue.GetType().GetField(value);
            var attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length == 0) return value;

            var descriptionAttribute = (DescriptionAttribute)attrs[0];
            return descriptionAttribute.Description;
        }
    }
}
