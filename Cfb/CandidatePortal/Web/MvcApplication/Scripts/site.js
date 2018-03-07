var CFBCP = {
    uiInit: function () {
        $(window).on("popstate", function (e) { location.reload(); });
        $("header .navbar-toggle").click(function () {
            if ($($(this).data("target")).hasClass("collapsing")) return false;
            var html = $("html");
            var locked = html.hasClass("locked");
            var offset = locked ? parseInt(html.css("top")) * -1 : 7 - $(this).offset().top;
            html.toggleClass("locked");
            locked ? window.scrollTo(0, offset) : html.css("top", offset);
        });
    },
    ajaxUiInit: function () {
        $(".ajax-trigger").click(function () { $(this).closest("form").trigger("submit"); });
        $("[data-ajax-trigger=change]").on("change", function () { $(this).closest("form").trigger("submit"); });

        $(".input-group-addon + input[type=date]").datepicker({
            changeDate: function () {
                $(this).datepicker("hide");
            },
            format: "yyyy-mm-dd"
        });

        $("abbr").tooltip();

        $(".glyphicon-question-sign[data-toggle=tooltip], .label[data-toggle=tooltip]").tooltip();

        $("[data-toggle=row-collapse]").click(function () {
            $(this).next().find(".collapse").collapse("toggle");
            $(this).find(".glyphicon-chevron-right, .glyphicon-chevron-down").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
        });
    },
    GetAntiForgeryToken: function () {
        var tokenWindow = window;
        var tokenName = "__RequestVerificationToken";
        var tokenField = $(tokenWindow.document).find("input[type='hidden'][name='" + tokenName + "']");
        if (tokenField.length == 0) {
            return null;
        } else {
            return {
                name: tokenName,
                value: tokenField.val()
            };
        }
    },

}
$(function () {
    CFBCP.uiInit();
    CFBCP.ajaxUiInit();
    $.ajaxPrefilter(function (options, localOptions, jqXHR) {
        var token = CFBCP.GetAntiForgeryToken();
        jqXHR.setRequestHeader(token.name, token.value);
    });
});