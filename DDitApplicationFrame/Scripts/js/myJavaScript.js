




(function ($) {
    //清空表单
    $.fn.clearForm = function () {
        var id = $(this).attr("id");
        var objId = document.getElementById(id);
        if (objId == undefined) {
            return;
        }
        for (var i = 0; i < objId.elements.length; i++) {
            if (objId.elements[i].type == "text") {
                objId.elements[i].value = "";
            }
            else if (objId.elements[i].type == "hidden") {
                objId.elements[i].value = "";
            }
            else if (objId.elements[i].type == "password") {
                objId.elements[i].value = "";
            }
            else if (objId.elements[i].type == "radio") {
                objId.elements[i].checked = false;
            }
            else if (objId.elements[i].type == "checkbox") {
                objId.elements[i].checked = false;
            }
            else if (objId.elements[i].type == "select-one") {
                objId.elements[i].options[0].selected = true;
            }
            else if (objId.elements[i].type == "select-multiple") {
                for (var j = 0; j < objId.elements[i].options.length; j++) {
                    objId.elements[i].options[j].selected = false;
                }
            }
            else if (objId.elements[i].type == "textarea") {
                objId.elements[i].value = "";
            }
        }
    }

    //JSON时间转换
    function ChangeDateFormat(val, isDataTime) {
        if (val != null) {
            var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
            //月份为0-11，所以+1，月份小于10时补个0
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

            var Time = date.getHours() + ":" + date.getMinutes();

            if (isDataTime) {
                return date.getFullYear() + "-" + month + "-" + currentDate + " " + Time;
            }

            return date.getFullYear() + "-" + month + "-" + currentDate;
        }
        return "";
    }

    //表单赋值
    $.fn.setForm = function (data) {
        return this.each(function () {
            var input, name;
            if (data == null) { this.reset(); return; }
            for (var i = 0; i < this.length; i++) {
                input = this.elements[i];
                //checkbox的name可能是name[]数组形式
                name = (input.type == "checkbox") ? input.name.replace(/(.+)\[\]$/, "$1") : input.name;
                if (data[name] == undefined) continue;
                switch (input.type) {
                    case "checkbox":
                        if (data[name] == "") {
                            input.checked = false;
                        } else {
                            //数组查找元素
                            if (data[name].indexOf(input.value) > -1) {
                                input.checked = true;
                            } else {
                                input.checked = false;
                            }
                        }
                        break;
                    case "radio":
                        if (data[name] == "") {
                            input.checked = false;
                        } else if (input.value == data[name]) {
                            input.checked = true;
                        }
                        break;
                    case "button": break;
                    default: input.value = data[name];
                }
            }
        });
    }

    //弹出层加载JS
    $.fn.FromEvent = function (content) {
        var cc = $.trim(content.toString());
        var contentCode = cc.substring(13, cc.length - 1);
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.text = contentCode;
        $(this).append(script);
    };

    $.fn.extend({
        "custromTable": function (options) {
            if (options.columns == undefined) return;

            if (options.isNo == undefined || options.isNo == true) {
                options.columns.splice(0, 0, {
                    title: "序号", width: 30, className: "taC", render: function (data, type, row, meta) {
                        var set = meta.settings
                        var api = new $.fn.dataTable.Api(set);
                        var page = api.rows({ page: 'current' }).data().page();
                        return (meta.row + 1) + (page * 10);
                    }
                });
            }
     
            options.ajax = $.extend({},{
                url: "",
                contentType: "application/json",
                type: "POST",
                dataSrc: "dataList",
                data: function (d) {
                    return JSON.stringify(d);
                }
            }, options.ajax);

            var opts = $.extend({}, tableDefault, options);

            var table = this.DataTable(opts)

            if (options.columnsClick == undefined || options.columnsClick == true) {
                table.on('draw.dt', function (e, settings, json) {
                    /*点击表格 添加背景颜色*/
                    $(this).find("tbody tr").on('click', function () {
                        if ($(this).hasClass('selected')) {
                            $(this).removeClass('selected');
                        }
                        else {
                            table.$('tr.selected').removeClass('selected');
                            $(this).addClass('selected');
                        }
                    });
                });
            }

            return table
        }
    });

    var tableDefault = {
        serverSide: true, //开启服务端模式
        processing: true,
        pagingType: "full_numbers", // 分页显示形式 首 上 下 末
        lengthChange: false,   //隐藏选择分页
        searching: false,        //隐藏搜索
        columnDefs: [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        language: {
            zeroRecords: "没有检索到数据",
            lengthMenu: "每页 _MENU_ 条记录",
            processing: "正在加载数据...请耐心等待...",
            infoEmpty: "没有数据",
            info: "第 _START_ 到第 _END_ 条数据；共有 _TOTAL_ 条记录",
            paginate: {
                first: "首页",
                previous: "前页",
                next: "后页",
                last: "尾页"
            }
        },
        scrollY: 400,
        scrollCollapse: false,
        ordering: true,
        order: [[1, 'desc']],
        ajax: {},
        columns: []
    }



})(window.jQuery);

var dditConfig = {
    btn: {
        close: '<span class="glyphicon glyphicon glyphicon-off" aria-hidden="true"></span> 关闭',
        comfrie: '<span class="glyphicon glyphicon glyphicon-ok" aria-hidden="true"></span> 确定'
    },
    hintLayer: {
        icon: 7,
        title: "提示信息",
        skin: 'layerStyle',
        offset: '200px',
        btn: ["取消", "确定"]
    }
}



