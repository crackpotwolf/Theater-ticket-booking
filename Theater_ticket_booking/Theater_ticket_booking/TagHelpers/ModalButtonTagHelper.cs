using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;

namespace Theater_ticket_booking.TagHelpers
{
    public class ModalButtonTagHelper : TagHelper
    {
        public string Method { get; set; }

        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            // присваивает атрибуту href значение из address
            output.Attributes.SetAttribute("href", "#");
            output.Attributes.SetAttribute("data-toggle", "modal");
            output.Attributes.SetAttribute("data-target", "#ModalPartial");

            switch (Method)
            {
                case ModalButtonMethods.Create:
                    output.PostContent.SetHtmlContent("<i class=\"fas fa fa-plus\"></i>");
                    output.AddClass("btn-success", HtmlEncoder.Default);
                    output.AddClass("br20px", HtmlEncoder.Default);
                    output.AddClass("p4px10px", HtmlEncoder.Default);
                    break;
                case ModalButtonMethods.Edit:
                    output.Content.SetHtmlContent("<i class=\"fas fa-pencil-alt\"></i>");
                    output.AddClass("btn-primary", HtmlEncoder.Default);
                    break;
                case ModalButtonMethods.Delete:
                    output.Content.SetHtmlContent("<i class=\"fas fa-trash\"></i>");
                    output.AddClass("btn-danger", HtmlEncoder.Default);
                    break;
                case ModalButtonMethods.Execute:
                    output.Content.SetHtmlContent("<i class=\"fas fa-plus\"></i>");
                    output.AddClass("btn-primary", HtmlEncoder.Default);
                    break;
            }

        }

        public static class ModalButtonMethods
        {
            public const string Create = "create";
            public const string Edit = "edit";
            public const string Delete = "delete";
            public const string Execute = "execute";
        }
    }

}