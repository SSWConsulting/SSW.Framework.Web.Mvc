using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSW.Framework.Web.Mvc
{
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

    public static class SelectListItemExtensions
    {
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
