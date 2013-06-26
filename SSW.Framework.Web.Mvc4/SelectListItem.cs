using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{
    /// <summary>
    /// Extend SelectListItem with a Generic Typed Value. 
    /// Uses ConvertHelper to perform type conversion
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class SelectListItem<TValue> : SelectListItem
    {
        public new TValue Value
        {
            get
            {
                return (TValue)ConvertHelper.ChangeType(base.Value, typeof(TValue));
            }
            set
            {
                base.Value = value.ToString();
            }
        }
    }

    /// <summary>
    /// Extensions for IEnumerable collectiojns of SelectListItem 
    /// </summary>
    public static class SelectListItemExtensions
    {
        /// <summary>
        /// Set selected property on matching SelectListItem within a collecion 
        /// </summary>
        /// <param name="items">Collection of SelectListItem</param>
        /// <param name="value">value to match</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> SelectValue(this IEnumerable<SelectListItem> items, string value)
        {
            foreach (var item in items)
            {
                if (value == null)
                {
                    item.Selected = item.Value == null;
                }
                else
                {
                    item.Selected = value.Equals(item.Value);
                }
                yield return item;
            }
        }


        /// <summary>
        /// Set selected property on matching SelectListItem within a collecion.
        /// This overload supports generic typed value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="items"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem<TValue>> SelectValue<TValue>(this IEnumerable<SelectListItem<TValue>> items, TValue value)
        {
            foreach (var item in items)
            {
                if (value == null)
                {
                    item.Selected = item.Value == null;
                }
                else
                {
                    item.Selected = value.Equals(item.Value);
                }
                yield return item;
            }
        }
    }
}
