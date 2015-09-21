//<!-- 多语言
//<script src="../../../Themes/Scripts/jquery.glob.text.js"></script>
//-->

$("[G11N-ID]").each(function () {
    var vTagName = $(this)[0].tagName;
    var vId = $(this).attr("G11N-ID");

    if (vTagName == 'SPAN') {
        $(this).html($.getG11NValue(vId));
        return true;
    }
    if (vTagName == 'INPUT') {
        $(this).val($.getG11NValue(vId));
        return true;
    }
    if (true) {
        $(this).html($.getG11NValue(vId));
    }

});


var tip = new mini.ToolTip();
tip.set({
    target: document,
    selector: '.globshowtip',
    onbeforeopen: function (e) {
        e.cancel = false;
    },
    onopen: function (e) {
        var el = e.element;
        var id = $(el).attr("g11n-id");
        var showtitle = $.getG11NValueTip(id);
        tip.setContent(showtitle);
    }
});
