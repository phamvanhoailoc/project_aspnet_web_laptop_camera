#pragma checksum "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da963f8a6ec4525b37d3291803e982e4333f0d03"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_News_List), @"mvc.1.0.view", @"/Views/News/List.cshtml")]
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
#line 1 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\_ViewImports.cshtml"
using WebApp_camera_laptop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\_ViewImports.cshtml"
using WebApp_camera_laptop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
using PagedList.Core.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da963f8a6ec4525b37d3291803e982e4333f0d03", @"/Views/News/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"629424598a4d68b2ea4da5857b10c08283034203", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_News_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PagedList.Core.IPagedList<WebApp_camera_laptop.Models.News>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pager-container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::PagedList.Core.Mvc.PagerTagHelper __PagedList_Core_Mvc_PagerTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
  
    int CurrentPage = ViewBag.CurrentPage;
    int PageNext = CurrentPage + 1;
    CategoriesNews category = ViewBag.CurrentCat;
    ViewData["Title"] = category.CatName + " Page " + ViewBag.CurrentPage;
    Layout = "~/Views/Shared/_Layout.cshtml";
    List<News> baiviethot = ViewBag.Baiviethot;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main>

    <!-- breadcrumb area start -->
    <section class=""breadcrumb__area box-plr-75"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xxl-12"">
                    <div class=""breadcrumb__wrapper"">
                        <nav aria-label=""breadcrumb"">
                            <ol class=""breadcrumb"">
                                <li class=""breadcrumb-item""><a href=""index.html"">Trang chủ</a></li>
                                <li class=""breadcrumb-item active"" aria-current=""page"">Dự án</li>
                            </ol>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- breadcrumb area end -->
    <!-- blog area start -->
    <section class=""blog__area box-plr-75 pb-70"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xxl-2 col-xl-3 col-lg-3"">
                    <div class=""sidebar__widget");
            WriteLiteral(@""">
                        <div class=""sidebar__widget-item mb-35"">
                            <h3 class=""sidebar__widget-title mb-30"">Bài viết nổi bậc</h3>
                            <div class=""sidebar__post rc__post"">
                                <ul>
");
#nullable restore
#line 43 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                     if (baiviethot != null)
                                    {
                                        foreach (var item in baiviethot)
                                        {
                                            string[] imageList = !string.IsNullOrEmpty(item.Thumb) ? item.Thumb.Split(',') : new string[] { "default.jpg" };
                                            string url = $"/baiviet/{item.Alias}-{item.NewId}.html";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                            <li>
                                                <div class=""rc__post d-flex align-items-center"">
                                                    <div class=""rc__post-thumb mr-20"">
                                                        <a");
            BeginWriteAttribute("href", " href=\"", 2469, "\"", 2480, 1);
#nullable restore
#line 52 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
WriteAttributeValue("", 2476, url, 2476, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "da963f8a6ec4525b37d3291803e982e4333f0d037646", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2554, "~/images/news/", 2554, 14, true);
#nullable restore
#line 53 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
AddHtmlAttributeValue("", 2568, imageList[0], 2568, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 53 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
AddHtmlAttributeValue("", 2588, item.Title, 2588, 11, false);

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
            WriteLiteral(@"
                                                        </a>
                                                    </div>
                                                    <div class=""rc__post-content"">
                                                        <h3 class=""rc__post-title"">
                                                            <a");
            BeginWriteAttribute("href", " href=\"", 2956, "\"", 2967, 1);
#nullable restore
#line 58 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
WriteAttributeValue("", 2963, url, 2963, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 58 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                                                      Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                                        </h3>\r\n                                                    </div>\r\n                                                </div>\r\n                                            </li>\r\n");
#nullable restore
#line 63 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-xxl-10 col-xl-9 col-lg-9 order-first order-lg-last"">
                    <div class=""row"">
");
#nullable restore
#line 72 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                         if (Model != null && Model.Count() > 0)
                        {
                            foreach (var item in Model)
                            {
                                string[] imageList = !string.IsNullOrEmpty(item.Thumb) ? item.Thumb.Split(',') : new string[] { "default.jpg" };
                                string url = $"/baiviet/{item.Alias}-{item.NewId}.html";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <div class=""col-xxl-6 col-xl-6 col-lg-6 col-md-6"">
                                    <article class=""postbox__item format-image mb-50 transition-3"">
                                        <div class=""postbox__list "">
                                            <a");
            BeginWriteAttribute("href", " href=\"", 4300, "\"", 4311, 1);
#nullable restore
#line 81 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
WriteAttributeValue("", 4307, url, 4307, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "da963f8a6ec4525b37d3291803e982e4333f0d0312977", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4373, "~/images/news/", 4373, 14, true);
#nullable restore
#line 82 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
AddHtmlAttributeValue("", 4387, imageList[0], 4387, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 82 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
AddHtmlAttributeValue("", 4407, item.Title, 4407, 11, false);

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
            WriteLiteral(@"
                                            </a>
                                        </div>
                                        <div class=""postbox__content"">
                                            <h3 class=""postbox__title"">
                                                <a");
            BeginWriteAttribute("href", " href=\"", 4715, "\"", 4726, 1);
#nullable restore
#line 87 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
WriteAttributeValue("", 4722, url, 4722, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 87 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                                          Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                            </h3>\r\n                                            <div class=\"postbox__meta\">\r\n                                                <p>Ngày đăng: <span>");
#nullable restore
#line 90 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                                               Write(item.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span></p>\r\n\r\n                                            </div>\r\n                                            <div class=\"postbox__text\">\r\n                                                <p>");
#nullable restore
#line 94 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                              Write(item.Scontents);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                            </div>\r\n                                        </div>\r\n                                    </article>\r\n                                </div>\r\n");
#nullable restore
#line 99 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                            }
                            

                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <div class=\"col-xxl-12\" style=\"padding-bottom:10px;\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "da963f8a6ec4525b37d3291803e982e4333f0d0317658", async() => {
            }
            );
            __PagedList_Core_Mvc_PagerTagHelper = CreateTagHelper<global::PagedList.Core.Mvc.PagerTagHelper>();
            __tagHelperExecutionContext.Add(__PagedList_Core_Mvc_PagerTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 105 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.List = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("list", __PagedList_Core_Mvc_PagerTagHelper.List, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __PagedList_Core_Mvc_PagerTagHelper.AspArea = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__PagedList_Core_Mvc_PagerTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-CatId", "PagedList.Core.Mvc.PagerTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 105 "D:\work\project-ecommerce-laptop-camera\project_aspnet_web_laptop_camera\WebApp_camera-laptop\Views\News\List.cshtml"
                                                                                          WriteLiteral(category.CatNewId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __PagedList_Core_Mvc_PagerTagHelper.RouteValues["CatId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-CatId", __PagedList_Core_Mvc_PagerTagHelper.RouteValues["CatId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n    <!-- blog area end -->\r\n\r\n</main>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PagedList.Core.IPagedList<WebApp_camera_laptop.Models.News>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
