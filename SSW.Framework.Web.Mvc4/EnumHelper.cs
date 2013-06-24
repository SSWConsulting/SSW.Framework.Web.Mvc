using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{
    public static class EnumHelper
    {
        public static Dictionary<TEnum, string> GetDisplayNames<TEnum>() where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);
            var result = new Dictionary<TEnum, string>();
            if (enumType.IsEnum)
            {
                var values = Enum.GetValues(enumType).OfType<TEnum>();
                foreach (var value in values)
                {
                    var name = GetDisplayName(enumType, value.ToString());
                    result.Add(value, name);
                }
            }
            return result;
        }
        public static string GetDisplayName(Type enumType, string enumValue)
        {
            var member = enumType.GetMember(enumValue).FirstOrDefault();
            if (member != null)
            {
                var attr = member.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    if (attr.ResourceType == null)
                    {
                        return attr.Name;
                    }
                    else
                    {
                        return attr.GetName();
                    }
                }
            }
            return enumValue;
        }
    }
}
