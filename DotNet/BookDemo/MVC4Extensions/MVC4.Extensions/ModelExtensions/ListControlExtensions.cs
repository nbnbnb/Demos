using MVC4.Models.Entities;
using MVC4.Models.Provider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Xml.Linq;

namespace MVC4.Extensions.ModelExtensions
{
    public static class ListControlExtensions
    {

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper,
            string name, string listName, string value)
        {
            return BoxList(htmlHelper, listName,
                item => htmlHelper.RadioButton(name, item.Value, value == item.Value));
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
            string name, string listName, IEnumerable<string> values)
        {
            return BoxList(htmlHelper, listName, item =>
                CheckBoxWithValue(htmlHelper, name, values.Contains(item.Value), item.Value));
        }

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name,
            string listName, IEnumerable<string> values)
        {
            IEnumerable<SelectListItem> selectListItems =
               GetSelectListItems(listName, m => values.Contains(m));

            return htmlHelper.ListBox(name, selectListItems);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
            string listName, string value)
        {
            IEnumerable<SelectListItem> selectListItems =
                GetSelectListItems(listName, m => value == m);
            return htmlHelper.DropDownList(name, selectListItems);
        }

        private static MvcHtmlString CheckBoxWithValue(this HtmlHelper htmlHelper,
             string name, bool isChecked, string value)
        {
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData
                .TemplateInfo.GetFullHtmlFieldName(name);

            ModelState modelState;

            // ModelState 数据中存在了相同键值的数据
            // 下面需要覆写存在相同键值的 ModelState
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState))
            {
                htmlHelper.ViewData.ModelState.SetModelValue(fullHtmlFieldName,
                    new ValueProviderResult(isChecked, isChecked.ToString(),
                        CultureInfo.CurrentCulture));
            }

            MvcHtmlString html;
            try
            {
                html = htmlHelper.CheckBox(name, isChecked);
            }
            finally
            {
                if (null != modelState)
                {
                    // 恢复原来的 ModelState
                    htmlHelper.ViewData.ModelState[fullHtmlFieldName] = modelState;
                }
            }

            string htmlString = html.ToString();
            var index = htmlString.LastIndexOf('<');

            // 过滤掉类型为 hidden 的 <input> 元素
            XElement element = XElement.Parse(htmlString.Substring(0, index));
            element.SetAttributeValue("value", value);
            return new MvcHtmlString(element.ToString());
        }

        private static MvcHtmlString BoxList(HtmlHelper htmlHelper,
         string listName, Func<NBListItem, MvcHtmlString> elementHtmlAccessor)
        {
            var listItems = ListProviders.Current.GetListItems(listName);
            TagBuilder table = new TagBuilder("table");
            TagBuilder tr = new TagBuilder("tr");
            foreach (var listItem in listItems)
            {
                TagBuilder td = new TagBuilder("td");
                td.InnerHtml += elementHtmlAccessor(listItem).ToHtmlString();
                td.InnerHtml += listItem.Text;
                tr.InnerHtml += td.ToString();
            }
            table.InnerHtml = tr.ToString();
            return new MvcHtmlString(table.ToString());
        }

        private static IList<SelectListItem> GetSelectListItems(string listName, Func<string, bool> check)
        {
            var listItems = ListProviders.Current.GetListItems(listName);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var item in listItems)
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = item.Value,
                    Text = item.Text,
                    Selected = check(item.Value)
                });
            }

            return selectListItems;
        }
    }
}