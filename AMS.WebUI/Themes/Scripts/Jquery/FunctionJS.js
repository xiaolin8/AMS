/*
初始化
*/
$(document).ready(function () {
    $('.aspNetHidden').hide();   
    publicobjcss();
    Loading(false);
    GetTabClick();
})

/**
初始化样式
**/
/*****************树表格********************************/
function dndexampleCss() {
    $(".example tbody tr").click(function () {
        $(this).removeClass("tdhover");
        $('.example tr').removeClass("selected");
        $(this).addClass("selected"); //添加选中样式   
    }).hover(function () {
        if (!$(this).hasClass('selected')) {
            $(this).addClass("tdhover");
        }
    }, function () {
        $(this).removeClass("tdhover");
    });
}
function publicobjcss() {
    dndexampleCss();
    /*****************按钮********************************/
    $(".l-btn").hover(function () {
        $(this).addClass("l-btnhover");
        $(this).find('span').addClass("l-btn-lefthover");
    }, function () {
        $(this).removeClass("l-btnhover");
        $(this).find('span').removeClass("l-btn-lefthover");
    });
    $(".tools_bar .tools_btn").hover(function () {
        if ($(this).attr('disabled') != 'disabled') {
            $(this).addClass("tools_btn-hover");
            $(this).find('span').addClass("tools_btn-hover");
        }
    }, function () {
        $(this).removeClass("tools_btn-hover");
        $(this).find('span').removeClass("tools_btn-hover");
    });
    /*****************自定义复选框********************************/
    $(".panelcheck").click(function () {
        if (!$(this).hasClass('checkbuttonOk')) {
            $(this).attr('class', 'checkbuttonOk panelcheck');
            $(this).find('.triangleNo').attr('class', 'triangleOk');

        } else {
            $(this).attr('class', 'checkbuttonNo panelcheck');
            $(this).find('.triangleOk').attr('class', 'triangleNo');
        }
    })

    $(".sub-menu div").click(function () {
        $('.sub-menu div').removeClass("selected")
        $(this).addClass("selected");
    }).hover(function () {
        $(this).addClass("navHover");
    }, function () {
        $(this).removeClass("navHover");
    });
}
//获取动态tab标签当前iframeID
function Current_iframeID() {
    var tabs_container = top.$("#tabs_container");
    return "tabs_iframe_" + tabs_container.find('.selected').attr('id').substr(5);
}
//表单tab切换
function Tabchange(id) {
    $('.ScrollBar').find('.tabPanel').hide();
    $('.ScrollBar').find("#" + id).show();
}
//Tab标签切换
function GetTabClick() {
    $(".tab_list_top div").click(function () {
        $(".tab_list_top div").removeClass("actived");
        $(this).addClass("actived"); //添加选中样式   
    })
}
//关闭当前tab
function ThisCloseTab() {
    var tabs_container = top.$("#tabs_container");
    top.RemoveDiv(tabs_container.find('.selected').attr('id').substr(5));
}
//搜索回车键
document.onkeydown = function (e) {
    if (!e) e = window.event; //火狐中是 window.event
    if ((e.keyCode || e.which) == 13) {
        var btnSearch = document.getElementById("btnSearch");
        if (btnSearch != null) {
            btnSearch.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
            btnSearch.click();
        }
    }
}
/*
屏蔽快捷键F1-F12
*/
function Shieldkeydown() {
    $("*").keydown(function (e) {
        e = window.event || e || e.which;
        if (e.keyCode == 112 || e.keyCode == 113
            || e.keyCode == 114 || e.keyCode == 115
            || e.keyCode == 116 || e.keyCode == 117
            || e.keyCode == 118 || e.keyCode == 119
            || e.keyCode == 120 || e.keyCode == 121
            || e.keyCode == 122 || e.keyCode == 123) {
            e.keyCode = 0;
            return false;
        }
    })
}
/*
回调
*/
function windowload() {
    rePage();
}

/*
刷新当前页面
*/
function rePage() {
    Loading(true);
    window.location.href = window.location.href.replace('#', '');
    return false;
}
function Replace() {
    Loading(true);
    window.location.reload();
    return false;
}
/*
返回上一级
*/
function bntback() {
    window.history.back(-1);
    Loading(true)
}
/*
跳转页面
*/
function Urlhref(url) {
    Loading(true);
    window.location.href = url;
    return false;
}
/*
中间加载对话窗
*/
function Loading(bool) {
    if (bool) {
        top.$("#loading").show();
    } else {
        setInterval(loadinghide, 900);
    }
}
function loadinghide() {
    if (top.document.getElementById("loading") != null) {
        top.$("#loading").hide();
    }
}
/**
Top 加载对话窗
msg:提示信息
time：停留时间ms
type：提示类型（1、success 2、error 3、warning）
**/
function showTopMsg(msg, time, type) {
    MsgTips(time, msg, 300, type);
}
/***
* 自动关闭弹出内容提示
timeOut : 4000,				//提示层显示的时间
msg : "恭喜你!你已成功操作此插件，谢谢使用！",			//显示的消息
speed : 300,				//滑动速度
type : "success"			//提示类型（1、success 2、error 3、warning）
***/
function MsgTips(timeOut, msg, speed, type) {
    $(".tip_container").remove();
    var bid = parseInt(Math.random() * 100000);
    $("body").prepend('<div id="tip_container' + bid + '" class="container tip_container"><div id="tip' + bid + '" class="mtip"><span id="tsc' + bid + '"></span></div></div>');
    var $this = $(this);
    var $tip_container = $("#tip_container" + bid);
    var $tip = $("#tip" + bid);
    var $tipSpan = $("#tsc" + bid);
    //先清楚定时器
    clearTimeout(window.timer);
    //主体元素绑定事件
    $tip.attr("class", type).addClass("mtip");
    $tipSpan.html(msg);
    $tip_container.slideDown(speed);
    //提示层隐藏定时器
    window.timer = setTimeout(function () {
        $tip_container.slideUp(speed);
        $(".tip_container").remove();
    }, timeOut);
    //鼠标移到提示层时清除定时器
    $tip_container.live("mouseover", function () {
        clearTimeout(window.timer);
    });
    //鼠标移出提示层时启动定时器
    $tip_container.live("mouseout", function () {
        window.timer = setTimeout(function () {
            $tip_container.slideUp(speed);
            $(".tip_container").remove();
        }, timeOut);
    });
    $("#tip_container" + bid).css("left", ($(window).width() - $("#tip_container" + bid).width()) / 2);
    //$("#tip_container" + bid).css("top", ($(window).height() - $("#tip_container" + bid).height()) / 2);
}
/**
短暂提示
msg: 显示消息
time：停留时间ms
type：类型 4：成功，5：失败，3：警告
**/
function showTipsMsg(msg, time, type) {
    if (type == 4) {
        top.showTopMsg(msg, time, 'success'); //头部提示,1、success 2、error 3、warning
    } else if (type == 5) {
        top.showTopMsg(msg, time, 'error'); //头部提示,1、success 2、error 3、warning
    } else if (type == 3) {
        top.showTopMsg(msg, time, 'warning'); //头部提示,1、success 2、error 3、warning
    }
}

//窗口关闭
function OpenClose() {
    //art.dialog.close();
    var api = frameElement.api, W = api.opener;
    api.close();
    return true;
}
/*验证
/*id:        表示请求
--------------------------------------------------*/
function IsEditdata(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("很抱歉，您当前未选中任何一行！", 4000, 3);
    } else if (id.split(",").length > 1) {
        isOK = false;
        showTipsMsg("很抱歉，一次只能选择一条记录！", 4000, 3);
    }
    return isOK;
}
function IsDelData(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("很抱歉，您当前未选中任何一行！", 4000, 3);
    }
    return isOK;
}
function IsChecked(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("您当前未选中任何一行！", 4000, 3);
    }
    return isOK;
}
/* 请求Ajax 带返回值
--------------------------------------------------*/
function getAjax(url, parm, callBack) {
    $.ajax({
        type: 'post',
        dataType: "text",
        url: url,
        data: parm,
        cache: false,
        async: false,
        success: function (msg) {
            callBack(msg);
        }
    });
}
/**
初始化样式
**/
/*****************树表格********************************/
function dndexampleCss() {
    $(".example tbody tr").click(function () {
        $(this).removeClass("tdhover");
        $('.example tr').removeClass("selected");
        $(this).addClass("selected"); //添加选中样式   
    }).hover(function () {
        if (!$(this).hasClass('selected')) {
            $(this).addClass("tdhover");
        }
    }, function () {
        $(this).removeClass("tdhover");
    });
}
/*****************树形样式********************************/
function treeAttrCss() {
    $(".black").treeview({
        control: "#treecontrol",
        persist: "cookie",
        cookieId: "treeview-gray"
    });
    treeCss();
}
//树样式
function treeCss() {
    $(".strTree li div").css("cursor", "pointer");
    $(".strTree li div").click(function () {
        $(".strTree li div").removeClass("collapsableselected");
        $(this).addClass("collapsableselected"); //添加选中样式
    }).hover(function () {
        $(this).addClass("collapsablehover"); //添加选中样式
    }, function () {
        $(".strTree li div").removeClass("collapsablehover");
    });
}
/**
数据验证完整性
**/
function CheckDataValid(id) {
    if (!JudgeValidate(id)) {
        return false;
    } else {
        return true;
    }
}

/** div 自应表格高度**/
function divresize(element, height) {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        $(element).css("height", $(window).height() - height);
    }
}
/**iframe自应高度**/
function iframeresize(height) {
    if (height == undefined) {
        height = 0;
    }
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        var iframeMain = $(window).height();
        $("#iframeMainContent").height(iframeMain - height);
    }
}
function divresize_From(height) {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        $(".div-body-From").width($(window).width() - 3);
        $(".div-body-From").css("height", $(window).height() - height);
    }
}
//验证是否为空
function IsNullOrEmpty(str) {
    var isOK = true;
    if (str == undefined || str == "") {
        isOK = false;
    }
    return isOK;
}
/********
接收地址栏参数
key:参数名称
**********/
function GetQuery(key) {
    var search = location.search.slice(1); //得到get方式提交的查询字符串
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == key) {
            return ar[1];
        }
    }
    return "";
}
/**加载表格函数
Begin
obj_ID:元素ID, url:请求地址, colM:表头名称, sort:要显示字段, PageSize：每页大小
**/
var GetRowIndex = -1; //索引号
function PQLoadGrid(obj_ID, url, colM, sort, PageSize, topVisible) {

    GetRowIndex = -1;
    var dataModel = {
        location: "remote",
        sorting: "remote",
        paging: "remote",
        dataType: "JSON",
        method: "GET",
        curPage: 1,
        rPP: PageSize,
        sortIndx: 0,
        sortDir: "up",
        rPPOptions: [20, 30, 40, 50, 100, 500, 1000],
        getUrl: function () {
            var orderField = (this.sortIndx == "0") ? "" : sort[this.sortIndx];
            var sortDir = (this.sortDir == "up") ? "desc" : "asc";
            if (this.curPage == 0) {
                this.curPage = 1;
            }
            return {
                url: url, data: "pqGrid_PageIndex=" + this.curPage + "&pqGrid_PageSize=" +
                    this.rPP + "&pqGrid_OrderField=" + orderField + "&pqGrid_OrderType=" + sortDir + "&pqGrid_Sort=" + escape(sort)
            };
        },
        getData: function (dataJSON) {
            if (dataJSON == null && dataJSON.totalRecords <= 0) {
                showTipsMsg("没有找到您要的相关数据！", 5000, 5);
            }
            return { curPage: dataJSON.curPage, totalRecords: dataJSON.totalRecords, data: dataJSON.data };
        }
    }
    if (!IsNullOrEmpty(topVisible)) {
        topVisible = false;
    }
    $(obj_ID).pqGrid({
        dataModel: dataModel,
        colModel: colM,
        editable: false,
        topVisible: topVisible,
        oddRowsHighlight: false,
        columnBorders: true,
        freezeCols: 0,
        rowSelect: function (evt, ui) {
            GetRowIndex = ui.rowIndxPage;
        }
    });
    pqGridResize(obj_ID, -52, -4);
}
function PQLoadGridNoPage(obj_ID, url, colM, sort) {
    GetRowIndex = -1;
    var dataModel = {
        location: "remote",
        sorting: "remote",
        paging: "",
        dataType: "JSON",
        method: "GET",
        sortIndx: 0,
        sortDir: "up",
        getUrl: function () {
            var orderField = (this.sortIndx == "0") ? "" : sort[this.sortIndx];
            var sortDir = (this.sortDir == "up") ? "desc" : "asc";
            return {
                url: url, data: "pqGrid_OrderField=" + orderField + "&pqGrid_OrderType=" + sortDir + "&pqGrid_Sort=" + escape(sort)
            };
        },
        getData: function (dataJSON) {
            if (dataJSON == null) {
                showTipsMsg("没有找到您要的相关数据！", 5000, 5);
            }
            return { data: dataJSON };
        }
    }
    $(obj_ID).pqGrid({
        dataModel: dataModel,
        colModel: colM,
        editable: false,
        bottomVisible: false,
        oddRowsHighlight: false,
        columnBorders: true,
        freezeCols: 0,
        rowSelect: function (evt, ui) {
            GetRowIndex = ui.rowIndxPage;
        }
    });
    pqGridResize(obj_ID, -36, -4);
}
/**表格自应高度
obj：ID，lose_height:-高度,lose_width:-宽度
**/
function pqGridResize(obj, lose_height, lose_width) {
    var grid_height = $(window).height() + lose_height;
    var grid_width = $(window).width() + lose_width;
    var grid1 = $(obj).pqGrid({
        width: grid_width,
        height: grid_height
    });
}
/**表格自应高度
obj：ID，lose_height:-高度,lose_width:-宽度
**/
function pqGridResizefixed(obj, lose_height, lose_width) {
    var grid_width = $(window).width() + lose_width;
    var grid1 = $(obj).pqGrid({
        width: grid_width,
        height: lose_height
    });
}
/**获取表格列值
obj：ID，rowCode：列号
**/
function GetPqGridRowValue(obj_ID, rowCode) {
    if (GetRowIndex != -1) {
        var DM = $(obj_ID).pqGrid("option", "dataModel");
        var data = DM.data;
        return data[GetRowIndex][rowCode];
    }
    else {
        return null;
    }
}
/**加载表格
end
**/
//自动补全表格
var IndetableRow_autocomplete = 0;
var scrollTopheight = 0;
function autocomplete(Objkey, width, height, data, callBack) {
    if ($('#' + Objkey).attr('readonly') == 'readonly') {
        return false;
    }
    if ($('#' + Objkey).attr('disabled') == 'disabled') {
        return false;
    }
    IndetableRow_autocomplete = 0;
    scrollTopheight = 0;
    var X = $("#" + Objkey).offset().top;
    var Y = $("#" + Objkey).offset().left;
    $("#div_gridshow").html("");
    if ($("#div_gridshow").attr("id") == undefined) {
        $('body').append('<div id="div_gridshow" style="overflow: auto;z-index: 1000;border: 1px solid #A8A8A8;border-top: 0px solid #A8A8A8;width:' + width + ';height:' + height + ';position: absolute; background-color: #fff; display: none;"></div>');
    } else {
        $("#div_gridshow").height(height);
        $("#div_gridshow").width(width);
    }
    var sbhtml = '<table class="tableobj">';
    if (data != "") {
        sbhtml += '<tbody>' + data + '</tbody>';
    } else {
        sbhtml += '<tbody><tr><td style="color:red;text-align:center;width:' + width + ';">没有找到您要的相关数据！</td></tr></tbody>';
    }
    sbhtml += '</table>';
    $("#div_gridshow").html(sbhtml);
    $("#div_gridshow").css("left", Y).css("top", X + 25).show();
    if (data != "") {
        $("#div_gridshow").find('tbody tr').each(function (r) {
            if (r == 0) {
                $(this).addClass('selected');
            }
        });
    }
    $("#div_gridshow").find('tbody tr').click(function () {
        var value = "";
        $(this).find('td').each(function (i) {
            value += $(this).text() + "≌";
        });
        if ($('#' + Objkey).attr('readonly') == 'readonly') {
            return false;
        }
        if ($('#' + Objkey).attr('disabled') == 'disabled') {
            return false;
        }
        callBack(value);
        $("#div_gridshow").hide();
    });
    $("#div_gridshow").find('tbody tr').hover(function () {
        $(this).addClass("selected");
    }, function () {
        $(this).removeClass("selected");
    });
    //任意键关闭
    document.onclick = function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'div_gridshow') {
            if ($(tar).attr("id") == 'div_gridshow' || $(tar).attr("id") == Objkey) {
                $("#div_gridshow").show();
            } else {
                $("#div_gridshow").hide();
            }
        }
    }
}
//方向键上,方向键下,回车键
function autocompletekeydown(Objkey, callBack) {
    $("#" + Objkey).keydown(function (e) {
        switch (e.keyCode) {
            case 38: // 方向键上
                if (IndetableRow_autocomplete > 0) {
                    IndetableRow_autocomplete--
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight -= 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 40: // 方向键下
                var tindex = $("#div_gridshow").find('tbody tr').length - 1;
                if (IndetableRow_autocomplete < tindex) {
                    IndetableRow_autocomplete++;
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight += 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 13:  //回车键
                var value = "";
                $("#div_gridshow").find('tbody tr').each(function (r) {
                    if (r == IndetableRow_autocomplete) {
                        $(this).find('td').each(function (i) {
                            value += $(this).text() + "≌";
                        });
                    }
                });
                if ($('#' + Objkey).attr('readonly') == 'readonly') {
                    return false;
                }
                if ($('#' + Objkey).attr('disabled') == 'disabled') {
                    return false;
                }
                callBack(value);
                $("#div_gridshow").hide();
                break;
            default:
                break;
        }
    })
}
//树下拉框
function combotree(Objkey, width, height, data) {
    $("#" + Objkey).bind("contextmenu", function () {
        return false;
    });
    $("#" + Objkey).css('ime-mode', 'disabled');
    $("#" + Objkey).keypress(function (e) {
        return false;
    });
    if ($('#' + Objkey).attr('readonly') == 'readonly') {
        return false;
    }
    if ($('#' + Objkey).attr('disabled') == 'disabled') {
        return false;
    }
    var X = $("#" + Objkey).offset().top;
    var Y = $("#" + Objkey).offset().left;
    $("#div_treeshow").html("");
    if ($("#div_treeshow").attr("id") == undefined) {
        $('body').append('<div id="div_treeshow" style="overflow: auto;border: 1px solid #A8A8A8;width:' + width + ';height:' + height + ';position: absolute; background-color: #fff; display: none;"></div>');
    } else {
        $("#div_treeshow").height(height);
        $("#div_treeshow").width(width);
    }
    var sbhtml = '';
    if (data != "") {
        sbhtml += '<ul class="black strTree">' + data + '</ul>';
    } else {
        sbhtml += '<ul class="black strTree"><li><div><span style="color:red;">暂无数据</span></div></li></ul>';
    }
    sbhtml += '</table>';
    $("#div_treeshow").html(sbhtml);
    $("#div_treeshow").css("left", Y).css("top", X + 23).show();
    $("#div_treeshow").css("z-index", "1000");
    //任意键关闭
    document.onclick = function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'div_treeshow') {
            if ($(tar).attr("id") == 'ParentName') {
                $("#div_treeshow").show();
            } else {
                $("#div_treeshow").hide();
            }
        }
    }
    treeAttrCss();
}
/*删除数据
/*url:        表示请求路径
/*parm：      条件参数
--------------------------------------------------*/
function delConfig(url, parm, count) {
    DeleteConfig(url, parm, "注：删除操作不可恢复，您确定要继续么？", count);
}
function DeleteConfig(url, parm, Msg, count) {
    if (count == undefined) {
        count = 1;
    }
    showConfirmMsg(Msg, function (r) {
        if (r) {
            getAjax(url, parm, function (rs) {
                if (rs.toLocaleLowerCase() == 'true') {
                    showTipsMsg("成功删除 " + count + " 笔记录。", 2000, 4);
                    windowload();
                } else if (rs.toLocaleLowerCase() == 'false') {
                    showTipsMsg("删除失败，请稍后重试", 4000, 5);
                } else {
                    showTipsMsg(rs, 4000, 3);
                }
            });
        }
    });
}

/**
警告提示
msg: 显示消息
callBack：函数
**/
function showConfirmMsg(msg, callBack) {
    var msg = "<div class='ui_alert'>" + msg + "</div>"
    top.$.dialog({
        id: "confirmDialog",
        lock: true,
        icon: "hits.png",
        content: msg,
        title: "系统提示",
        ok: function () {
            callBack(true)
            return true;
        },
        cancel: function () {
            callBack(false)
            return true;
        }
    });
}
