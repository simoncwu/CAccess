using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cfb.CandidatePortal.Web.MvcApplication
{
    public static class CPHelpers
    {
        /// <summary>
        /// Returns HTML markup for each property in the object that is represented by the <see cref="Expression"/> expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <returns></returns>
        public static MvcHtmlString FieldFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var labelTag = new TagBuilder("label") { InnerHtml = model.GetDisplayName() };
            var inputTag = new TagBuilder("input") { };
            inputTag.AddCssClass("form-control");
            html.EditorFor(expression);
            return MvcHtmlString.Create(labelTag.ToString() + inputTag.ToString());
        }
    }
}
