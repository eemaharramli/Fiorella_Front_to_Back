#pragma checksum "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Views/Experts/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dc09d5047a7fc467cb6e51c768212a2db4adabb6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Experts_Index), @"mvc.1.0.view", @"/Views/Experts/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc09d5047a7fc467cb6e51c768212a2db4adabb6", @"/Views/Experts/Index.cshtml")]
    public class Views_Experts_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Fiorella.Models.Expert>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<section id=""experts"">
    <div class=""container"">
        <div class=""row py-5"">
            <div class=""offset-lg-3 col-lg-6"">
                <div class=""section-title"">
                    <h1>Flower Experts</h1>
                    <p class=""text-black-50"">A perfect blend of creativity, energy, communication, happiness and
                        love. Let us arrange
                        a smile for you.</p>
                </div>
            </div>
        </div>
        <div class=""row pb-5"">
            ");
#nullable restore
#line 16 "/Users/elnur/Desktop/Fiorella_Front_to_Back/Fiorella/Fiorella/Views/Experts/Index.cshtml"
       Write(await Component.InvokeAsync("Expert"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            <div class=\"col-lg-3\"></div>\n            <div class=\"col-lg-3\"></div>\n            <div class=\"col-lg-3\"></div>\n        </div>\n    </div>\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Fiorella.Models.Expert>> Html { get; private set; }
    }
}
#pragma warning restore 1591
