using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{
    /// <summary>
    /// Helper functions to access enum attributes  
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Build a dictionary mapping Enum value to diaplay anme strings
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
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


        /// <summary>
        /// Get display name for am Enum value.
        /// First attempts to Read DisplayAttribute.Name, then falls back to DescriptionAttribute and then just the raw Enum name. 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
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
                // try description attribute
                var attr2 = member.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault();
                if (attr2 != null) return attr2.Description;
            }
            return enumValue;
        }
    }
}
