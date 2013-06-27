using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{

    /// <summary>
    /// Extensions for working with Enums.
    /// DsiplayNames are cached for performance.
    /// </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// Get user-readable display name (as read from Attrributtes) for an Enum value
        /// </summary>
        /// <typeparam name="T">an enumerated type</typeparam>
        /// <param name="value">enum value</param>
        /// <returns></returns>
        public static string ToDisplayName<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type.");
            }
            return NamesCache<T>.GetDisplayName(value);
        }

        /// <summary>
        /// Get user-readable display name for a nullable Enum value
        /// </summary>
        /// <typeparam name="T">an enumerated type</typeparam>
        /// <param name="value">enum value</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generate list collection of SelectListItem for a Nullable Enumerated Type
        /// </summary>
        /// <typeparam name="T">Must ba an Enum type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Generate list collection of SelectListItem for an Enumerated Type
        /// </summary>
        /// <typeparam name="T">Must ba an Enum type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
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


            /// <summary>
            /// Get the DisplayName (as specified by DisplayName attribute) for an Enum value
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
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

