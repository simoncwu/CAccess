//$.getScript("/scripts/maintenance-alert.js");
function _initSelectMenus() {
    if ($().selectmenu) {
        $("select").selectmenu({
            appendTo: null,
            select: function () { $(this).change(); }
        });
    }
}

$.getScript("https://ajax.googleapis.com/ajax/libs/webfont/1.4.7/webfont.js", function () {
    WebFont.load({
        google: {
            families: ['Varela']
        },
        active: function () {
            // select menu, moved to webfont loader active event due to dependency on rendered width
            _initSelectMenus();
        }
    });
});

if (navigator.userAgent.indexOf(" MSIE 10.0") >= 0)
    $("html").addClass("ie10plus ie10");
else if (navigator.userAgent.indexOf(" rv:11.0") >= 0)
    $("html").addClass("ie10plus ie11");

// customize widget behaviors
if ($.ui) {
    $.extend($.ui.accordion.prototype.options, {
        multiActive: false
    });
    $.widget("ui.accordion", $.ui.accordion, {
        _eventHandler: function (t) {
            if (this.options.multiActive) {
                this.active = $(t.currentTarget).is(".ui-state-active") ? $(t.currentTarget) : $();
                this.prevShow = this.active.length ? this.active.next() : $();
            }
            this._super(t);
        }
    });
    $.widget("ui.selectmenu", $.ui.selectmenu, {
        _renderItem: function (ul, item) {
            var li = $("<li>", { text: item.label, "class": item.element.attr("data-class") });
            if (item.disabled) {
                li.addClass("ui-state-disabled");
            }
            return li.appendTo(ul);
        }
    });
}

$(function () {
    // full-width SSO menu on scroll
    $(window).scroll(function () {
        var clearTarget = $("#SsoMenu .innermenu");
        var bgTarget = $("#SsoMenu");
        if ($(window).scrollTop() <= 90) {
            var temp = bgTarget;
            bgTarget = clearTarget;
            clearTarget = temp;
        }
        bgTarget.addClass("sso-bg");
        clearTarget.removeClass("sso-bg");
    });

    // fix SPGridView group header column span
    $(".ms-gb td[colspan]").each(function () { $(this).attr("colspan", $(this).parent().next().children().length); });
    // control pop-up behavior
    $("a.popup, li.popup a, a[target=_blank], .sso-app-list a, a[href^='/Announcements/DisclosureStatement.aspx']").on("click", function (e) {
        if (this.href) {
            window.open(this.href, "_blank", "location=yes,menubar=yes,resizable=yes,scrollbars=yes,status=yes,toolbar=yes,titlebar=yes,width=" + screen.width * .8 + ",height=" + screen.height * .7 + ",top=" + screen.height * .1 + ",left=" + screen.width * .1);
            e.stopPropagation();
            return false;
        }
    });
    $(".pseudolink a").click(function () {
        return false;
    });

    // reset AJAX dropdown list selections upon load/reload
    $("select.ajaxAutoPostBack").each(function () {
        if ($("option:selected", this).index() > 0)
            this.selectedIndex = 0;
    });

    // prevent double click of buttons
    $("a.button").one("click", function () {
        $(this).addClass("clicked").click(function (e) { e.preventDefault(); });
    });
    $("form").one("submit", function () {
        setTimeout(function () { $("input[type=submit]").attr("disabled", "").addClass("clicked"); }, 0);
    });

    // auto-focus fields
    $("input[type=text].cp-textinput").first().focus();
    $("input[type=text].error").first().focus();
    $("input[type=password].error").first().focus();

    // initialize widgets
    var initWidgets = function () {
        $(".icons li").hover(function () { $(this).addClass("ui-state-hover"); }, function () { $(this).removeClass("ui-state-hover"); });
        $(".ui-icon-closethick").parent("li").click(function (event) { $(this).closest("div").hide("fade"); });
        $(".cmoToolbar li[title^=Return]").click(function () { $(".contentTabs>.ui-tabs-nav .ui-state-active a")[0].click(); });
        $(".ui-tabs-nav li").unbind("keydown");
        // date picker
        if ($.datepicker) {
            $.datepicker.setDefaults({
                autoSize: true,
                buttonImageOnly: true,
                buttonImage: "/_layouts/images/calendar.gif",
                buttonText: "Calendar",
                showButtonPanel: true,
                showOn: "both"
            });
        }
        // tooltip
        if ($().tooltip) {
            $(".tooltip").tooltip({
                position: {
                    my: "left top",
                    at: "right bottom",
                    collision: "flipfit"
                }
            });
        }
    }

    if (typeof Sys !== "undefined") {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initWidgets();
            _initSelectMenus();
        });
    }
    initWidgets();
});