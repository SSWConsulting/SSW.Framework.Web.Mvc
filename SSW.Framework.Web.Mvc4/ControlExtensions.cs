using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace SSW.Framework.Web.Mvc
{

    public enum ViewModes
    {
        Read,
        Edit
    }

    public static class ControlExtensions
    {
        private const string ViewModeKey = "__VIEWMODE";


        public static ViewModes GetViewMode(this HtmlHelper html)
        {
            object mode;
            if (html.ViewData.TryGetValue(ViewModeKey, out mode) && mode is ViewModes)
            {
                return (ViewModes)mode;
            }
            else
            {
                return ViewModes.Read;
            }
        }


        public static void SetViewMode(this HtmlHelper html, ViewModes mode)
        {
            html.ViewData[ViewModeKey] = mode;
        }

        public static MvcHtmlString DisplayControlFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string templateName = null)
        {
            return ControlFor(html, expression, templateName, ViewModes.Read);
        }

        public static MvcHtmlString ControlFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string templateName = null,
            ViewModes? mode = null,
            object additionalViewData = null,
            string label = null)
        {
            return ControlFor(html, expression, expression, templateName, mode, additionalViewData, label);
        }

        public static MvcHtmlString ControlFor<TModel, TValue, TDisplayValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> editExpression,
            Expression<Func<TModel, TDisplayValue>> displayExpression,
            string templateName = null,
            ViewModes? mode = null,
            object additionalViewData = null,
            string label = null)
        {
            if (!mode.HasValue)
            {
                mode = html.GetViewMode();
            }
            Func<MvcHtmlString> controlFunc;
            switch (mode)
            {
                case ViewModes.Edit:
                    controlFunc = () => html.EditorFor(editExpression, templateName: templateName, additionalViewData: additionalViewData);
                    break;
                case ViewModes.Read:
                    controlFunc = () => html.DisplayFor(displayExpression, templateName: templateName, additionalViewData: additionalViewData);
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Unexpected ViewMode '{0}'.", mode));
            }
            return html.Partial("_Control", new ControlModel()
            {
                LabelFor = () => label == "" ? null : html.LabelFor(editExpression, label, new { @class = "control-label" }),
                ValidationMessageFor = () => html.ValidationMessageFor(editExpression),
                ControlFor = controlFunc
            });
        }
    }
}
