(function ($) {
    var g11nCulture = $.g11nCulture = {};
    $.extend({
        getG11NValue: function (strValue, defaultValue) {
            if ($.g11nCulture.hasOwnProperty(strValue)) {
                return $.g11nCulture[strValue];
            } else {
                if (defaultValue) {
                    return defaultValue;
                }
                return strValue;
            }
        },
        getG11NValueTip: function (strValue, defaultValue) {
            strValue = strValue + "_tip";
            if ($.g11nCulture.hasOwnProperty(strValue)) {
                return $.g11nCulture[strValue];
            } else {
                if (defaultValue) {
                    return defaultValue;
                }
                return strValue;
            }
        }
    });
})(jQuery);

 