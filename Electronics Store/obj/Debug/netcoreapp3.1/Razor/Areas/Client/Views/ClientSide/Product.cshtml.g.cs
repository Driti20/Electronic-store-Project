#pragma checksum "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "034a10a2cc411f191d4bec0987b83300130fb45c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Client_Views_ClientSide_Product), @"mvc.1.0.view", @"/Areas/Client/Views/ClientSide/Product.cshtml")]
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
#line 1 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\_ViewImports.cshtml"
using Electronics_Store;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\_ViewImports.cshtml"
using Electronics_Store.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"034a10a2cc411f191d4bec0987b83300130fb45c", @"/Areas/Client/Views/ClientSide/Product.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d75b33faddc1e3a211bd34e77ee73e0a693da2af", @"/Areas/Client/Views/_ViewImports.cshtml")]
    public class Areas_Client_Views_ClientSide_Product : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Electronics_Store.Areas.ProductArea.Models.Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-lg-12 product-image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
  
    ViewData["Title"] = "Product";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"col-12\">\r\n        <h3 class=\"pb-5 pt-3\">");
#nullable restore
#line 9 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                         Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n        <div class=\"row pb-5\">\r\n            <div class=\"col-lg-6 col-md-5\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "034a10a2cc411f191d4bec0987b83300130fb45c4551", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 308, "~/ImageOfProduct/", 308, 17, true);
#nullable restore
#line 12 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
AddHtmlAttributeValue("", 325, Model.Image, 325, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-lg-1\">\r\n\r\n            </div>\r\n            <div class=\"col-lg-5 col-md-4\">\r\n                <h5 class=\"pb-4\">");
#nullable restore
#line 18 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                            Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <div class=\"col-lg-12 row\">\r\n                    <div class=\"col-lg-5\">\r\n                        <p>Price: </p>\r\n                    </div>\r\n                    <div class=\"col-lg-5\">\r\n                        <p>");
#nullable restore
#line 24 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" €</p>
                    </div>
                </div>
                <div class=""col-lg-12 row"">
                    <div class=""col-lg-5"">
                        <p>Color: </p>
                    </div>
                    <div class=""col-lg-5"">
                        <p>");
#nullable restore
#line 32 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Color.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                    </div>
                </div>
                <div class=""col-lg-12 row"">
                    <div class=""col-lg-5"">
                        <p>Brand: </p>
                    </div>
                    <div class=""col-lg-5"">
                        <p>");
#nullable restore
#line 40 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Brand.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                    </div>
                </div>
                <div class=""col-lg-12 row"">
                    <div class=""col-lg-5"">
                        <p>Material: </p>
                    </div>
                    <div class=""col-lg-5"">
                        <p>");
#nullable restore
#line 48 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Material.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                    </div>
                </div>
                <div class=""col-lg-12 row"">
                    <div class=""col-lg-5"">
                        <p>Weight: </p>
                    </div>
                    <div class=""col-lg-5"">
                        <p>");
#nullable restore
#line 56 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Weight);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" kg</p>
                    </div>
                </div>
                <div class=""col-lg-12 row"">
                    <div class=""col-lg-5"">
                        <p>Warranty duration: </p>
                    </div>
                    <div class=""col-lg-5"">
                        <p>");
#nullable restore
#line 64 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                      Write(Model.Warranty.Duration);

#line default
#line hidden
#nullable disable
            WriteLiteral(" years</p>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 67 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                 if (ViewData["AvailableInStock"] != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""col-lg-12 row"">
                        <div class=""col-lg-5"">
                            <p>Availability: </p>
                        </div>
                        <div class=""col-lg-5"">
                            <p>");
#nullable restore
#line 74 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                          Write(ViewData["AvailableInStock"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 77 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n        <div>\r\n            <h4>Product Description: </h4>\r\n            <p>");
#nullable restore
#line 82 "C:\Users\LED COM\Source\Repos\Electronics Store Project\Electronics Store\Electronics Store\Areas\Client\Views\ClientSide\Product.cshtml"
          Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Electronics_Store.Areas.ProductArea.Models.Product> Html { get; private set; }
    }
}
#pragma warning restore 1591