var MSOWebPartPageFormName = "aspnetForm";
var currentCtx = null;
var bValidSearchTerm = false;
function SubmitFormPost(url, bForceSubmit) {
    if (typeof (MSOWebPartPageFormName) != "undefined") {
        var form = document.forms[MSOWebPartPageFormName];
        if (null != form) {
            if ((bForceSubmit != undefined && bForceSubmit == true)
				|| !form.onsubmit || (form.onsubmit() != false)) {
                form.action = STSPageUrlValidation(url);
                form.method = "POST";
                if (isPortalTemplatePage(url))
                    form.target = "_top";
                if (!bValidSearchTerm)
                    ClearSearchTerm("");
                form.submit();
            }
        }
    }
}
function ClearSearchTerm(guidView) {
    if (typeof (MSOWebPartPageFormName) != "undefined") {
        var form = document.forms[MSOWebPartPageFormName];
        if (null != form) {
            if (guidView != null) {
                var frmElem = form["SearchString" + guidView];
                if (frmElem != null)
                    frmElem.value = "";
            }
        }
    }
    bValidSearchTerm = true;
}
function StURLSetVar2(stURL, stVar, stValue) {
    var stNewSet = stVar + "=" + stValue;
    var ichHash = stURL.indexOf("#");
    var hashParam;
    if (ichHash != -1) {
        hashParam = stURL.substring(ichHash, stURL.length);
        stURL = stURL.substring(0, ichHash);
    }
    var ichQ = stURL.indexOf("?");
    if (ichQ != -1) {
        var ich = stURL.indexOf("?" + stVar + "=", ichQ);
        if (ich == -1) {
            ich = stURL.indexOf("&" + stVar + "=", ichQ);
            if (ich != -1)
                stNewSet = "&" + stNewSet;
        }
        else {
            stNewSet = "?" + stNewSet;
        }
        if (ich != -1) {
            var re = new RegExp("[&?]" + stVar + "=[^&]*", "");
            stURL = stURL.replace(re, stNewSet);
        }
        else {
            stURL = stURL + "&" + stNewSet;
        }
    }
    else {
        stURL = stURL + "?" + stNewSet;
    }
    if (hashParam != null && hashParam.length > 0)
        stURL = stURL + hashParam;
    return stURL;
}
function MoveToViewDate(strdate, view_type) {
    var wUrl = window.location.href;
    if (strdate != null)
        wUrl = StURLSetVar2(wUrl, "CalendarDate", escapeProperly(strdate));
    if (view_type != null)
        wUrl = StURLSetVar2(wUrl, "CalendarPeriod", view_type);
    SubmitFormPost(wUrl, true);
}
