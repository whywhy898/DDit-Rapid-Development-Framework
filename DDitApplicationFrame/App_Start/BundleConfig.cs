using System.Web;
using System.Web.Optimization;

namespace DDitApplicationFrame
{
    public class BundleConfig
    {
        
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/datatables/js/jquery.js",
                "~/Scripts/fancyTree/jquery-ui.custom.js",
                "~/Scripts/js/jquery-ui-1.8.24.js",
                "~/Scripts/js/json2.js",
                "~/Scripts/bootstrap/js/bootstrap.js",
                "~/Scripts/datatables/js/jquery.dataTables.js",
                "~/Scripts/datatables/js/dataTables.bootstrap.js",
                "~/Scripts/js/jquery.validate.js",
                "~/Scripts/js/jquery.form.js",
                "~/Scripts/alertjs/src/alertify.js",
                "~/Scripts/js/jquery.contextmenu.r2.js",
                "~/Scripts/iconpicker/js/iconPicker.js",
                "~/Scripts/select2/js/select2.js",
                "~/Scripts/charts/js/echarts.js",
                "~/Scripts/charts/theme/macarons.js",
                "~/Scripts/fancyTree/jquery.fancytree.js",
                "~/Scripts/fileUpload/fileinput.js",
                "~/Scripts/fileUpload/fileinput_locale_zh.js",
              //  "~/Scripts/layer/layer.js",
                "~/Scripts/js/jquery.sortable.js",
                "~/Scripts/summernote/summernote.js",
                "~/Scripts/summernote/lang/summernote-zh-CN.js",
                "~/Scripts/js/myJavaScript.js"
            ));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Scripts/bootstrap/css/1").Include(
                                                         "~/Scripts/bootstrap/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Scripts/datatables/css/1").Include("~/Scripts/datatables/css/dataTables.bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/1").Include("~/Content/MainStyle.css", "~/Content/learunui-accordion.css", "~/Content/AssistStyle.css"));
            bundles.Add(new StyleBundle("~/Scripts/alertjs/themes/1").Include("~/Scripts/alertjs/themes/alertify.core.css", "~/Scripts/alertjs/themes/alertify.bootstrap.css"));
            bundles.Add(new StyleBundle("~/Scripts/iconpicker/css/1").Include("~/Scripts/iconpicker/css/icon-picker.css"));
            bundles.Add(new StyleBundle("~/Scripts/select2/css/1").Include("~/Scripts/select2/css/select2.css"));
            bundles.Add(new StyleBundle("~/Scripts/fancyTree/skin-lion/1").Include("~/Scripts/fancyTree/skin-lion/ui.fancytree.css"));
            bundles.Add(new StyleBundle("~/Scripts/fileUpload/1").Include("~/Scripts/fileUpload/fileinput.css"));
            bundles.Add(new StyleBundle("~/Scripts/summernote/1").Include("~/Scripts/summernote/summernote.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}