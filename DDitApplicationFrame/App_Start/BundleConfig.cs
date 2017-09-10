using System.Web;
using System.Web.Optimization;

namespace DDitApplicationFrame
{
    public class BundleConfig
    {
        
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            //日期控件 日志用
            bundles.Add(new ScriptBundle("~/Scripts/calendar").Include(
                        "~/Scripts/calendar/moment.min.js",
                        "~/Scripts/calendar/fullcalendar.js",
                        "~/Scripts/calendar/locale/zh-cn.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                //jquery
                "~/Scripts/datatables/js/jquery.js",
                "~/Scripts/fancyTree/jquery-ui.custom.js",
                "~/Scripts/js/jquery-ui.js",
                //json
                "~/Scripts/js/json2.js",
                //bootstap库
                "~/Scripts/bootstrap/js/bootstrap.js",
                //表格控件
                "~/Scripts/datatables/js/jquery.dataTables.js",
                "~/Scripts/datatables/js/dataTables.bootstrap.js",
                //jquery验证
                "~/Scripts/js/jquery.validate.js",
                //ajax表单提交
                "~/Scripts/js/jquery.form.js",
                //弹出框控件
                "~/Scripts/alertjs/src/alertify.js",
                //icon图标控件
                "~/Scripts/js/jquery.contextmenu.r2.js",
                "~/Scripts/iconpicker/js/iconPicker.js",
                //下拉框
                "~/Scripts/select2/js/select2.js",
                "~/Scripts/select2/js/i18n/zh-CN.js",
                //图标控件
                "~/Scripts/charts/js/echarts.js",
                "~/Scripts/charts/theme/macarons.js",
                //树形控件
                "~/Scripts/fancyTree/jquery.fancytree.js",
                //上传控件
                "~/Scripts/fileUpload/fileinput.js",
                "~/Scripts/fileUpload/fileinput_locale_zh.js",
                //拖拽控件
                "~/Scripts/js/jquery.sortable.js",
                //富文本编辑器
                "~/Scripts/summernote/summernote.js",
                "~/Scripts/summernote/lang/summernote-zh-CN.js",
                //时间控件
                "~/Scripts/datetimepicker/js/moment-with-locales.js",
                "~/Scripts/datetimepicker/js/bootstrap-datetimepicker.js",
                //gooflow
                "~/Scripts/gooflow/GooFunc.js",
                "~/Scripts/gooflow/GooFlow.js",
                "~/Scripts/gooflow/GooFlow_color.js",
                //自定义JS库
                "~/Scripts/js/myJavaScript.js"
            ));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。

            bundles.Add(new StyleBundle("~/Content").Include("~/Content/MainStyle.css", "~/Content/AssistStyle.css"));
          
            bundles.Add(new StyleBundle("~/Scripts").Include(
                        "~/Scripts/bootstrap/css/bootstrap.css",
                        "~/Scripts/datatables/css/dataTables.bootstrap.css",
                        "~/Scripts/alertjs/themes/alertify.core.css",
                        "~/Scripts/alertjs/themes/alertify.bootstrap.css",
                        "~/Scripts/iconpicker/css/icon-picker.css",
                        "~/Scripts/select2/css/select2.css",
                        "~/Scripts/fancyTree/skin-lion/ui.fancytree.css",
                        "~/Scripts/fileUpload/fileinput.css",
                        "~/Scripts/summernote/summernote.css",
                        "~/Scripts/calendar/fullcalendar.css",
                        "~/Scripts/datetimepicker/css/bootstrap-datetimepicker.css",
                        "~/Scripts/gooflow/fonts/iconflow.css",
                        "~/Scripts/gooflow/GooFlow.css",
                        "~/Scripts/cheack/font-awesome.css",
                        "~/Scripts/cheack/build.css"
                        ));
        }
    }
}