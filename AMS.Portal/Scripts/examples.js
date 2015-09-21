Ext.AutoHide = function () {
    var msgCt;
    function createBox(t, s) {
        return ['<div class="msg">',
                '<div class="x-box-tl"><div class="x-box-tr"><div class="x-box-tc"></div></div></div>',
                '<div class="x-box-ml"><div class="x-box-mr"><div class="x-box-mc" style="font-size=12px;"><h3>', t, '</h3>', s, '</div></div></div>',
                '<div class="x-box-bl"><div class="x-box-br"><div class="x-box-bc"></div></div></div>',
                '</div>'].join('');
    }
    return {
        msg: function (title, format) {
            if (!msgCt) {
                msgCt = Ext.DomHelper.insertFirst(document.body, { id: 'msg-div22', style: 'position:absolute;top:10px;width:300px;margin:0 auto;z-index:20000;' }, true);
            }
            msgCt.alignTo(document, 't-t');
            var s = String.format.apply(String, Array.prototype.slice.call(arguments, 1));
            var m = Ext.DomHelper.append(msgCt, { html: createBox(title, s) }, true);
            m.slideIn('t').pause(1).ghost("t", { remove: true });
        },
        hide: function (v) {
            var msg = Ext.get(v.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement);
            msg.ghost("tr", { remove: true });
        }
    };
}();