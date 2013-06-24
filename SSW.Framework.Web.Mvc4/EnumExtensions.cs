using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{
    public static class EnumExtensions
    {
        public static string ToDisplayName<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type.");
            }
            return NamesCache<T>.GetDisplayName(value);
        }

        public static string ToDisplayName<T>(this T? value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type.");
            }
            if (value.HasValue)
            {
                return NamesCache<T>.GetDisplayName(value.Value);
            }
            else
            {
                return "";
            }
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this T? value) where T : struct, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type.");
            }

            yield return new SelectListItem()
            {
                Selected = !value.HasValue,
                Text = "Select",
                Value = ""
            };

            var names = NamesCache<T>.GetNames().OrderBy(x => x.Key);
            foreach (var pair in names)
            {
                bool selected = value.HasValue && EqualityComparer<T>.Default.Equals(pair.Key, value.Value);
                yield return new SelectListItem()
                {
                    Selected = selected,
                    Text = pair.Value,
                    Value = pair.Key.ToString()
                };
            }
        }


        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this T value) where T : struct, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type.");
            }

            var names = NamesCache<T>.GetNames().OrderBy(x => x.Key);
            foreach (var pair in names)
            {
                bool selected = EqualityComparer<T>.Default.Equals(pair.Key, value);
                yield return new SelectListItem()
                {
                    Selected = selected,
                    Text = pair.Value,
                    Value = pair.Key.ToString()
                };
            }
        }

        private static class NamesCache<T> where T : struct, IConvertible
        {
            private static Dictionary<T, string> _names;

            static NamesCache()
            {
                _names = EnumHelper.GetDisplayNames<T>();
            }

            public static Dictionary<T, string> GetNames()
            {
                return _names;
            }

            public static string GetDisplayName(T value)
            {
                if (!typeof(T).IsEnum)
                {
                    throw new ArgumentException("T must be an enumeration type.");
                }
                string name;
                if (_names.TryGetValue(value, out name))
                {
                    return name;
                }
                else
                {
                    return value.ToString();
                }
            }
        }
    }
}
