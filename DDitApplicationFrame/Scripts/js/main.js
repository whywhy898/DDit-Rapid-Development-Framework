
var arrTile = {};
setLink(getCookie('color'));

$('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
$("#tree>ul>li").children('ul').find('li').hide();	//隐藏掉所有的子菜单 
//children:只考虑子元素而不考虑所有后代元素

$('.tree li.parent_li > span').on('click', function (e) {
    var children = $(this).parent('li.parent_li').find(' > ul > li');
    if (children.is(":visible")) {		//visible:可见性
        children.slideUp('fast');
    } else {
        children.slideDown('fast');		//slideDown:滑动方式显示隐藏 $(selector).slideDown(speed,callback)
    }
    e.stopPropagation();
});

$("table[role='grid']").find('tr').css({"height":"37px"})

$('.main .sy').show().siblings().hide();


//加载窗体
function loadifarme(aId,aDiv, aUrl) {
    $("div[msg=" + aDiv + "]").remove();

    var f = $("<iframe class='fw99 fh99 displayn' id='" + aId + "' src='" + aUrl + "'></iframe>");
    $("<div msg='" + aDiv + "' class='fw100 fh100 tan-cent'><img src='../../Images/loadimg.gif' /></div>").appendTo("#Main").append(f).show()

    f.load(function () {
        $(this).prev("img").remove();
        $(this).show();
        var head = this.contentWindow.document.getElementsByTagName("head");
        var color = getCookie('color') == null ? "colora" : getCookie('color');

        var linkColor = $("<link rel='stylesheet' type='text/css' href='/Content/css/" + color + ".css' id='color'>");
        $(head).append(linkColor);
    });
}

function OpenNewTab(id, url, name, imgurl) {
    if (imgurl == undefined)
        imgurl = "";
    if (!arrTile[name]) {       //判断是不是已添加了li
        loadifarme(id, name, url);
        $('#tit ul').append($("<li></li>").attr({
            'msg': name,
            'class': 'boxbg',
            'data-url': url
        }).html('<img/>' + name + '<b></b>').find('img').attr("class", imgurl).css({ "margin-top": "-3px" }).parent());
        arrTile[name] = 1;
    }
    $('div[msg=' + name + ']').show().siblings().hide();
    $('#tit').find('li[msg=' + name + ']').addClass('boxbg').siblings().removeClass('boxbg');
    cilcktitle()
}

$('#tree').on("click", "span[data-div]", function () {
    var aDiv = $(this).data('div');
    var aUrl = $(this).data('url');
    if (aUrl == "")
        return;
    var amenuID = $(this).data('menuid');
    var oImg = $(this).find("i").attr("class");
    var ifameId = aUrl.split("/").join('');

    OpenNewTab(ifameId, aUrl, aDiv, oImg);

})


$(function () {

    //点击Tab页关闭
    $('#tit').on("click", "b", function (e) {
        var $this = $(this).parent('li'),	//查找b的唯一父元素li 
            msg = $this.attr('msg');	//返回文档中li的参数msg
        var mdiv = $('.main div[msg="' + msg + '"]'),
            mMsg = mdiv.siblings('div[msg]');
        var thisIndex = $(this).parent().index();//获取被点击li的索引值
        $this.remove();
        mdiv.hide();
        arrTile[msg] = 0;
        $('#jqContextMenu').hide();

        //首页后面有其他tab栏
        if ($("#tit li").length) {
            //如果当前在首页栏，删除首页后面的选项卡,tab栏不切换
            if ($("#tit span").hasClass('boxbg')) {
                $("#tit li").removeClass('boxbg');
                $(".main div[msg]").hide();
                $(".sy").show();
            } else if ($(this).parent().hasClass('boxbg')) {
                //如果当前位置不在首页，当前点击的li是活动的，就显示它的前一个
                $("#tit li").eq(thisIndex - 1).addClass('boxbg').siblings().removeClass('boxbg');
                var msgs = $("#tit li").eq(thisIndex - 1).attr("msg");
                // console.log(msgs);
                $('.main div[msg="' + msgs + '"]').show().siblings().hide();
            }
        } else {
            $(".sy").show();
        }
        e.stopPropagation();
    })

    //点击所有Tab页面效果
    $('#tit').on('click', 'li,span', function () {
        var $this = $(this),
        msg = $this.attr('msg');
        $('#tit').find('span,li').removeClass('boxbg');
        if ($this.attr('msg')) {
            $this.addClass('boxbg');
        } else {
            $this.addClass('boxbg');
        }
        if (msg) {
            $('.main div[msg="' + msg + '"]').show().siblings().hide();
        } else {
            $('.main div[msg]').hide();
            $('.sy').show();
        }
        $('#tit span').removeClass('boxbg');
    });
})


//点击右键效果
function cilcktitle() {
    $('#tit li').contextMenu('myMenu1', {
        bindings: {
            'open': function (t) {
                var aUrl = $(t).data('url');
                var aDiv = $(t).attr('msg');
                var ifameId = aUrl.split("/").join('');
                loadifarme(ifameId,aDiv, aUrl);
            },
            'delete': function (t) {
                //console.log(t)
                var $t = $(t);		//t代表li
                if ($t.hasClass('boxbg')) {		//如果存在含有boxbg的li
                    var sibl = $t.removeClass('boxbg').siblings('li:not(:hidden)').eq(0).addClass('boxbg');
                    //移除li中所有boxbg 并且去除所有同级不可见li 然后给第一个li添加boxbg
                    var aDivs = $t.attr('msg'); //设置当前li属性 msg
                    $('.main div[msg=' + aDivs + ']').hide();	//隐藏main下msg属性值与li属性值相同的div
                    var oDivs = $t.siblings('li:not(:hidden)').eq(0).attr('msg');	//设置li.boxbg 属性 msg
                    $('.main div[msg=' + oDivs + ']').show();	//显示main下msg属性值与li.boxbg属性值相同的div
                    // 0 、undefined、null、做判断的时候都是false  其他都是true
                    if (!sibl.length) {	//判断长度是否为0--flase  !flase--true length是0的话就是true  执行show
                        $('.sy').show();
                    }
                    $t.remove();
                } else {
                    $t.remove();
                }
                arrTile[$t.attr('msg')] = 0;
            },
            //右键关闭其他所有li
            'clse': function (t) {
                var $t = $(t);
                var tmsg = $t.attr('msg');
                arrTile = {};	//赋值为空(把之前那些msg给清掉)
                arrTile[$t.attr('msg')] = 1;
                $t.show().siblings().remove();
                $t.addClass('boxbg').show().siblings().remove();
                $('.main div[msg=' + tmsg + ']').show().siblings().hide();
            }
        }
    })
}

$("#messUl").on("click", "[name='layerMes']", function () {
    var aUrl = "/Message/UserMessagePage"
    var aDiv = "我的消息";
    if (!arrTile[aDiv]) {       //判断是不是已添加了li
        loadifarme(aDiv, aUrl);
        //获取左边相对应图标
        $('#tit ul').append($("<li></li>").attr({
            'msg': aDiv,
            'class': 'boxbg',
            'data-url': aUrl
        }).html('<img/>' + aDiv + '<b></b>').find('img').attr('class', "glyphicon glyphicon-text16").css({ "margin-top": "-3px" }).parent());
        arrTile[aDiv] = 1;
    }
    //点击左侧列表  右边显示相对应内容
    $('div[msg=' + aDiv + ']').show().siblings().hide();
    $('#tit').find('li[msg=' + aDiv + ']').addClass('boxbg').siblings().removeClass('boxbg');

    cilcktitle()
})

$("#myMes").click(function () {
    var aUrl = "/Message/UserMessagePage"
    var aDiv = "我的消息";
    if (!arrTile[aDiv]) {       //判断是不是已添加了li
        loadifarme(aDiv, aUrl);
        //获取左边相对应图标
        $('#tit ul').append($("<li></li>").attr({
            'msg': aDiv,
            'class': 'boxbg',
            'data-url': aUrl
        }).html('<img/>' + aDiv + '<b></b>').find('img').attr('class', "glyphicon glyphicon-text16").css({ "margin-top": "-3px" }).parent());
        arrTile[aDiv] = 1;
    }
    //点击左侧列表  右边显示相对应内容
    $('div[msg=' + aDiv + ']').show().siblings().hide();
    $('#tit').find('li[msg=' + aDiv + ']').addClass('boxbg').siblings().removeClass('boxbg');

    cilcktitle()
})

$('.tree li>span+ul li span').hover(function () {
    $(this).attr('class', 'boxbg');
}, function () {
    $(this).removeClass('boxbg');
})

//获取类高度与窗口高度-64px相同
$('.treebox').css('height', ($(window).height() - 92 + 'px'));
$('.main').css('height', ($(window).height() - 133 + 'px'));


//点击左侧箭头小图标变化
$('.xlj').on('click', function () {
    $(this).toggleClass("selected");
})
//点击按钮切换 左下角图标
$('.sy .btn').on('click', function () {
    $(this).toggleClass("btnn");
})

//点击弹出 改变肤色
$('#fu li').on("click", function () {
    var thisId = $(this).attr('id');	//设置当前li的ID 
    setLink(thisId);
    setCookie('color', thisId, 30);
})

//点击个人中心tab切换
$('.tabchange').find('a').click(function () {
    $('.tabchange').find('a').attr('class', '');
    $('.tabchange').find('div.tabox').css('display', 'none');
    $(this).attr('class', 'boxbg');
    $('.tabchange').find('div.tabox').eq($(this).index()).css('display', 'block');
})



function setLink(colorLinkName) {
    var bgcolor = $('#color');	//js 修改 link 的id="bgcolor" 动态修改引用的href="css/camera.css" 部分
    if (colorLinkName != null) {
        bgcolor.attr('href', '/Content/css/' + colorLinkName + '.css');
    }
    $('.dropdown-menu').addClass('boxbg');

    $('.treebox').addClass('boxbg');

    if (window.frames.length > 0) {
        var frms = window.document.getElementsByTagName("iframe");
        $.each(frms,function(i,v){
            var element = v.contentWindow.document.getElementById("color")
            $(element).attr('href', '/Content/css/' + colorLinkName + '.css');
        })
    }
}

//设置取cookies
function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}

//读取cookies
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg)) {
        return unescape(arr[2]);
    }
    else {
        return null;
    }
}

function daytime() {
    var timee = new Date();
    var hh = timee.getHours();
    var mm = timee.getMinutes();
    var ss = timee.getSeconds();
    var yy = timee.getFullYear();
    var MM = timee.getMonth() + 1;
    var rr = timee.getDate();
    var ww = timee.getDay();
    var days = ["日", "一", "二", "三", "四", "五", "六"]
    var n = yy + "-" + MM + "-" + rr + "   " + hh + ":" + mm + ":" + ss;
    var d = "星期" + days[ww];
    setTimeout(daytime, 1000);
    $("#time b").text(d)
    $("#time span").text(n)
}














