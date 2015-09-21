/* 使用例子
function GetData() {
    var Url = "../../CommonModule/FrameHandler/NavigationHandler.ashx";
    var param = new AjaxHandler.Param(Url);
    param.json = "LoadNaviMenu";
    AjaxHandler.Query.Post(param, loadSuccess, function loadError() { });
}
function loadSuccess(data) {
    var result=eval("(" + data + ")");
}


.cs程序
  string jsonText = Server.UrlDecode(Request["json"]);
*/

if (!window.AjaxHandler) { window.AjaxHandler = {}; }
if (!AjaxHandler.Query) { AjaxHandler.Query = {}; }
AjaxHandler.Param = function (handler)
{
    this.HandlerURL = handler;
}

AjaxHandler.Query.Post = function (param, onSuccess, onError)
{
    var _handlerUrl = param.HandlerURL;
    var _param = param;

    $.ajax({ type: "post", cache: false, url: _handlerUrl, data: _param, dataType: "json", success: onSuccess, error: onError });
}

AjaxHandler.Query.PostHtml = function (param, onSuccess, onError) {
    var _handlerUrl = param.HandlerURL;
    var _param = param;
    $.ajax({ type: "post", cache: false, url: _handlerUrl, data: _param, dataType: "html", success: onSuccess, error: onError });
}
