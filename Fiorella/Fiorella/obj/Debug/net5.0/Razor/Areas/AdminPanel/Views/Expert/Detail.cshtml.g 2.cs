#pragma checksum "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "898ec49584d57eaca6589e4a92a5bd5c559b0dd2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminPanel_Views_Expert_Detail), @"mvc.1.0.view", @"/Areas/AdminPanel/Views/Expert/Detail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/_ViewImports.cshtml"
using Fiorella.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/_ViewImports.cshtml"
using Fiorella.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"898ec49584d57eaca6589e4a92a5bd5c559b0dd2", @"/Areas/AdminPanel/Views/Expert/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27b23d00435fa2fe1359a5bb3510ba1e251b4f0b", @"/Areas/AdminPanel/Views/_ViewImports.cshtml")]
    public class Areas_AdminPanel_Views_Expert_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Expert>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
  
    ViewBag.Title = "title";
    Layout = "_AdminLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"main-panel\">\n    <div class=\"content-wrapper\">\n      <div class=\"col-lg-12 stretch-card grid-margin\">\n        <div class=\"card\">\n          <div class=\"card-body\">\n            <h4 class=\"card-title\">");
#nullable restore
#line 13 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
                              Write(Model.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Info</h4>
            <table class=""table table-bordered"">
              <thead>
              <tr>
                <th> Expert Name </th>
                <th> Expert Job </th>
                <th> Expert Image </th>
              </tr>
              </thead>
              <tbody>
              <tr class=""table-info"">
                <td> ");
#nullable restore
#line 24 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
                Write(Model.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                <td> ");
#nullable restore
#line 25 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
                Write(Html.Raw(Model.ExpertJob.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                <td> ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "898ec49584d57eaca6589e4a92a5bd5c559b0dd24901", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 758, "~/img/", 758, 6, true);
#nullable restore
#line 26 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
AddHtmlAttributeValue("", 764, Model.Image, 764, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 26 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Areas/AdminPanel/Views/Expert/Detail.cshtml"
AddHtmlAttributeValue("", 783, Model.Image, 783, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" </td>\n              </tr>\n              </tbody>\n            </table>\n          </div>\n        </div>\n      </div>\n    </div>\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Expert> Html { get; private set; }
    }
}
#pragma warning restore 1591
