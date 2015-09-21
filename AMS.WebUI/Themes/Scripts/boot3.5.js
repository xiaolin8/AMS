__CreateJSPath = function (js) {
    var scripts = document.getElementsByTagName("script");
    var path = "";
    for (var i = 0, l = scripts.length; i < l; i++) {
        var src = scripts[i].src;
        if (src.indexOf(js) != -1) {
            var ss = src.split(js);
            path = ss[0];
            break;
        }
    }
    var href = location.href;
    href = href.split("#")[0];
    href = href.split("?")[0];
    var ss2 = href.split("/");
    ss2.length = ss2.length - 1;
    href = ss2.join("/");
    if (path.indexOf("https:") == -1 && path.indexOf("http:") == -1 && path.indexOf("file:") == -1 && path.indexOf("\/") != 0) {
        path = href + "/" + path;
    }
    return path;
}

__CheckJS = function (js) {
    var scripts = document.getElementsByTagName("script");
    var isResult = true;
    for (var i = 0, l = scripts.length; i < l; i++) {
        var src = scripts[i].src;
        if (src.indexOf(js) != -1) {
            isResult = false;
            break;
        }
    }
    return isResult;
}

var bootPATH = __CreateJSPath("boot3.5.js");
var lllltion;
function ssssimeout() { }
//debugger
mini_debugger = false;
////change Skin
//mini_changeSkin = false;
////multi-language
//mini_multi_Language = true;
 

document.write('<script src="' + bootPATH + 'miniui3.5/miniuic3.5.js" type="text/javascript" ></sc' + 'ript>');
document.write('<link href="' + bootPATH + 'miniui3.5/themes/default/miniui.css" rel="stylesheet" type="text/css" />');
document.write('<link href="' + bootPATH + 'miniui3.5/themes/icons.css" rel="stylesheet" type="text/css" />');
document.write('<script src="' + bootPATH + 'jquery/Jquery.ajax.handler.js" type="text/javascript" ></sc' + 'ript>');
//document.write('<script src="' + bootPATH + 'Function3.5.js" type="text/javascript" ></sc' + 'ript>');

document.write('<link href="' + bootPATH + 'miniui3.5/themes/bootstrap/skin.css" rel="stylesheet" type="text/css" />');
//document.write('<link href="' + bootPATH + '../Styles/style3.5.css" rel="stylesheet" type="text/css" />');

//input验证
function onInputValidation(e) {
    var control = e.sender;
    if (e.isValid == false) {
        tipMessage(control, e.errorText);
    }
}
//提示信息
function tipMessage(obj, Validatemsg) {
    var Isrequired = false;
    if ($('#message').length > 0) {
        $('#message').html("");
        $("#message").html("<div class=\"note-error\"><div class=\"note-icon-error\"></div><div class=\"note-text\">" + Validatemsg + "</div></div>").slideDown('fast');
    } else {
        top.TipMsg(Validatemsg, 5000, 'error');
    }
    window.setTimeout(docNessageBubbleremove, 5000);
    return false;
}
function docNessageBubbleremove() {
    $('#message').slideUp(300);
}