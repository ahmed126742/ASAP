using System.ComponentModel;

namespace ASAP_Task.WebAPI.Authentication.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Enum Item not found.", nameof(enumValue));
        }
    }
}
