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
    /// <summary>
    /// HtmlHelper extensiuon for using enums
    /// </summary>
    public static class EnumHtmlExtensions
    {

        /// <summary>
        /// build a dropDownList for an enum value
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression) where TProperty : struct, IConvertible, IComparable
        {
            if (!typeof(TProperty).IsEnum) throw new ApplicationException("EnumDropDownFor must be used with an enum");
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            TProperty value = (TProperty)metaData.Model;
            return htmlHelper.DropDownListFor(expression, value.ToSelectListItems());
        }


        /// <summary>
        /// build a dropDownList for an enum value
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        object htmlAttributes) where TProperty : struct, IConvertible, IComparable
        {
            if (!typeof(TProperty).IsEnum) throw new ApplicationException("EnumDropDownFor must be used with an enum");
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            TProperty value = (TProperty)metaData.Model;
            return htmlHelper.DropDownListFor(expression, value.ToSelectListItems(), htmlAttributes);
        }


        /// <summary>
        /// build a dropDownList for an enum value
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        string optionLabel,
        object htmlAttributes) where TProperty : struct, IConvertible, IComparable
        {
            if (!typeof(TProperty).IsEnum) throw new ApplicationException("EnumDropDownFor must be used with an enum");
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            TProperty value = (TProperty)metaData.Model;
            return htmlHelper.DropDownListFor(expression, value.ToSelectListItems(), optionLabel, htmlAttributes);
        }


    }
}
