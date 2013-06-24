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
    /// ControlModel is a view model used by the ControlExtensions to support custom control layout.
    /// </summary>
    public class ControlModel
    {
        /// <summary>
        /// Gets or sets the function that returns the label for the control.
        /// </summary>
        public Func<MvcHtmlString> LabelFor;
        /// <summary>
        /// Gets or sets the function that returns the editor for the control.
        /// </summary>
        public Func<MvcHtmlString> ControlFor;
        /// <summary>
        /// Gets or sets the function that returns the validation message section for the control.
        /// </summary>
        public Func<MvcHtmlString> ValidationMessageFor;
    }
}
