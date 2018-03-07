var UTF8_1ST_OF_2 = 0xc0;
var UTF8_1ST_OF_3 = 0xe0;
var UTF8_1ST_OF_4 = 0xf0;
var UTF8_TRAIL = 0x80;
var HIGH_SURROGATE_BITS = 0xD800;
var LOW_SURROGATE_BITS = 0xDC00;
var SURROGATE_6_BIT = 0xFC00;
var SURROGATE_ID_BITS = 0xF800;
var SURROGATE_OFFSET = 0x10000;
function escapeProperlyCoreCore(str, bAsUrl, bForFilterQuery, bForCallback) {
    var strOut = "";
    var strByte = "";
    var ix = 0;
    var strEscaped = " \"%<>\'&";
    if (typeof (str) == "undefined")
        return "";
    for (ix = 0; ix < str.length; ix++) {
        var charCode = str.charCodeAt(ix);
        var curChar = str.charAt(ix);
        if (bAsUrl && (curChar == '#' || curChar == '?')) {
            strOut += str.substr(ix);
            break;
        }
        if (bForFilterQuery && curChar == '&') {
            strOut += curChar;
            continue;
        }
        if (charCode <= 0x7f) {
            if (bForCallback) {
                strOut += curChar;
            }
            else {
                if ((charCode >= 97 && charCode <= 122) ||
					 (charCode >= 65 && charCode <= 90) ||
					 (charCode >= 48 && charCode <= 57) ||
					 (bAsUrl && (charCode >= 32 && charCode <= 95) && strEscaped.indexOf(curChar) < 0)) {
                    strOut += curChar;
                }
                else if (charCode <= 0x0f) {
                    strOut += "%0" + charCode.toString(16).toUpperCase();
                }
                else if (charCode <= 0x7f) {
                    strOut += "%" + charCode.toString(16).toUpperCase();
                }
            }
        }
        else if (charCode <= 0x07ff) {
            strByte = UTF8_1ST_OF_2 | (charCode >> 6);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | (charCode & 0x003f);
            strOut += "%" + strByte.toString(16).toUpperCase();
        }
        else if ((charCode & SURROGATE_6_BIT) != HIGH_SURROGATE_BITS) {
            strByte = UTF8_1ST_OF_3 | (charCode >> 12);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | ((charCode & 0x0fc0) >> 6);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | (charCode & 0x003f);
            strOut += "%" + strByte.toString(16).toUpperCase();
        }
        else if (ix < str.length - 1) {
            var charCode = (charCode & 0x03FF) << 10;
            ix++;
            var nextCharCode = str.charCodeAt(ix);
            charCode |= nextCharCode & 0x03FF;
            charCode += SURROGATE_OFFSET;
            strByte = UTF8_1ST_OF_4 | (charCode >> 18);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | ((charCode & 0x3f000) >> 12);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | ((charCode & 0x0fc0) >> 6);
            strOut += "%" + strByte.toString(16).toUpperCase();
            strByte = UTF8_TRAIL | (charCode & 0x003f);
            strOut += "%" + strByte.toString(16).toUpperCase();
        }
    }
    return strOut;
}
function escapeProperly(str) {
    return escapeProperlyCoreCore(str, false, false, false);
}
function escapeProperlyCore(str, bAsUrl) {
    return escapeProperlyCoreCore(str, bAsUrl, false, false);
}
function escapeUrlForCallback(str) {
    var iPound = str.indexOf("#");
    var iQues = str.indexOf("?");
    if ((iPound > 0) && ((iQues == -1) || (iPound < iQues))) {
        var strNew = str.substr(0, iPound);
        if (iQues > 0) {
            strNew += str.substr(iQues);
        }
        str = strNew;
    }
    return escapeProperlyCoreCore(str, true, false, true);
}
function PageUrlValidation(url) {
    if (url.substr(0, 4) != "http" && url.substr(0, 1) != "/") {
        var L_InvalidPageUrl_Text = "Invalid page URL: ";
        alert(L_InvalidPageUrl_Text);
        return "";
    }
    else
        return url;
}
var g_currentID;
var g_strDatePickerFrameID = "DatePickerFrame";
var g_strDatePickerImageID = "DatePickerImage";
var g_strDatePickerRangeValidatorID = "DatePickerRangeValidator";
var g_warnonce = 1;
var g_scrollLeft;
var g_scrollTop;
function WindowPosition(elt) {
    var pos = new Object;
    pos.x = 0;
    pos.y = 0;
    while (elt.offsetParent != null
		&& !(elt.tagName == "DIV" && (elt.style.overflow == "auto" ||
		elt.style.overflowX == "auto" || elt.style.overflowY == "auto"))) {
        pos.x += elt.offsetLeft - elt.scrollLeft;
        pos.y += elt.offsetTop - elt.scrollTop;
        elt = elt.offsetParent;
    }
    return pos;
}
function getOffsetTop(elem, value) {
    if (elem == null) return value;
    if (elem.tagName.toUpperCase() == "TD" && elem.style.borderTopStyle != "none") {
        var shift = parseInt(elem.style.borderTopWidth);
        if (!isNaN(shift)) {
            value += shift;
        }
    }
    return getOffsetTop((elem.tagName.toUpperCase() == "BODY") ? elem.parentElement : elem.offsetParent, elem.offsetTop - elem.scrollTop + value);
}
function getOffsetLeft(elem, value) {
    if (elem == null) return value;
    if (elem.tagName.toUpperCase() == "TD" && elem.style.borderLeftStyle != "none") {
        var shift = parseInt(elem.style.borderLeftWidth);
        if (!isNaN(shift)) {
            value += shift;
        }
    }
    return getOffsetLeft((elem.tagName.toUpperCase() == "BODY") ? elem.parentElement : elem.offsetParent, elem.offsetLeft - elem.scrollLeft + value);
}
function getDate(field, serverDate) {
    if (field.value != null)
        return field.value;
    else
        return serverDate;
}
function HLD(elt) {
    HL(elt, "ms-dphighlightedday");
}
function HLM(elt) {
    HL(elt, "ms-dphighlightedmonth");
}
function HL(elt, classname) {
    if (elt.classSave != null) {
        elt.className = elt.classSave;
        elt.classSave = null;
    }
    else {
        elt.classSave = elt.className;
        elt.className = classname;
    }
}
function PositionFrame(thediv) {
    var elt = document.getElementById(thediv);
    var ifrm = window.frameElement;
    if (ifrm == null || elt == null)
        return;
    ifrm.style.width = "171px";
    ifrm.style.height = elt.offsetHeight + "px";
    /*	if (ifrm.style.direction == "rtl") {
    ifrm.style.right = ifrm.style.left - elt.offsetWidth;
    }
    else {
    ifrm.style.left = ifrm.style.right - elt.offsetWidth;
    }
    if (ifrm.style.left < 0) {
    ifrm.style.left = ifrm.style.right;
    ifrm.style.right += elt.offsetWidth;
    }
    var cxBody = window.parent.document.documentElement.offsetWidth;
    if (ifrm.style.right + elt.offsetWidth > cxBody) {
    ifrm.style.right = ifrm.style.left;
    ifrm.style.left -= elt.offsetWidth;
    }*/
    ifrm.style.right = "-111px";
    ifrm.style.top = "15px";
    ifrm.style.display = "block";
    var elm = document.getElementById(g_currentID);
    if (elm == null)
        return;
    try { elm.focus(); } catch (exception) { }
    return;
}
function HideUnhide(nhide, nunhide, id) {
    var eltHide = document.getElementById(nhide);
    if (eltHide != null)
        eltHide.style.display = "none";
    var eltUnhide = document.getElementById(nunhide);
    if (eltUnhide != null)
        eltUnhide.style.display = "block";
    PositionFrame(nunhide);
    g_currentID = id;
}
function datereplace(ourl, pattern, newstr) {
    var str = new String(ourl);
    var res = str.indexOf(pattern);
    if (res != -1) {
        var resString = str.substring(0, res);
        resString += newstr;
        var resapp = str.indexOf("&", res);
        if (resapp != -1) {
            resString += str.substr(resapp + 1);
        }
        return resString;
    }
    else {
        var q = str.indexOf("?");
        if (q == -1) str += "?";
        if (str.charAt(str.length - 1) != '&') str += "&";
        str += newstr;
        return str;
    }
}
function MoveToDate(dt) {
    var ourl = document.location.href;
    var pattern = "date=";
    ourl = datereplace(ourl, pattern, "date=" + escapeProperly(dt) + "&");
    document.location = ourl;
    return true;
}
function OnKeyDown(elem) {
    var evtSource = elem.document.parentWindow.event;
    var nKeyCode = evtSource.keyCode;
    switch (nKeyCode) {
        case 27:
            evtSource.returnValue = false;
            ClosePicker();
            break;
        case 38:
            evtSource.returnValue = false;
            MoveDays(-7);
            break;
        case 40:
            evtSource.returnValue = false;
            MoveDays(7);
            break;
        case 37:
            evtSource.returnValue = false;
            MoveDays(-1);
            break;
        case 39:
            evtSource.returnValue = false;
            MoveDays(1);
            break;
    }
}
function ClosePicker() {
    var ifrm = window.frameElement;
    if (ifrm == null) {
        return;
    }
    ifrm.resultfunc();
    ifrm.style.display = "none";
    ifrm = null;
}
function MoveDays(iday) {
    var stNextID;
    if (g_currentID == null || g_currentID.length < 6)
        return;
    var yr = g_currentID.substr(0, 4) - 0;
    var mon = g_currentID.substr(4, 2) - 0;
    var day = g_currentID.substr(6, 2) - 0;
    if (day + iday < 1) {
        return;
    }
    else {
        stNextID = g_currentID.substr(0, 6) + St2Digits(day + iday);
        var elm = document.getElementById(stNextID);
        if (elm == null)
            return;
        g_currentID = stNextID;
        try { elm.focus(); } catch (exception) { }
    }
}
function St2Digits(w) {
    var st = "";
    if (w < 0)
        return st;
    if (w < 10)
        st += "0";
    st += w;
    return st;
}
function clickDatePicker(field, src, datestr) {
    var date;
    var objField = document.getElementById(field);
    var fieldid;
    if (event != null)
        event.cancelBubble = true;
    if (field == null && this.Picker != null) {
        this.Picker.style.display = "none";
        this.Picker = null;
    }
    else if (objField != null) {
        var fieldelm = document.getElementById(field);
        if (fieldelm != null && fieldelm.isDisabled)
            return;
        date = getDate(objField, datestr);
        fieldid = objField.id;
        var objDatePickerImage = document.getElementById(fieldid + g_strDatePickerImageID);
        clickDatePickerHelper(fieldid, fieldid + g_strDatePickerFrameID, objDatePickerImage, date, src, OnSelectDate, OnPickerFinish);
        document.body.onclick = OnPickerFinish;
    }
}
function clickDatePickerHelper(textboxid, iframeid, objImage, datestr, iframesrc, OnSelectDateCallback, onpickerfinishcallback) {
    var strCurrentResultFieldId = "";
    if (this.Picker != null) {
        this.Picker.style.display = "none";
        strCurrentResultFieldId = this.Picker.resultfield.id;
        if (this.Picker.resultfunc != null) {
            this.Picker.resultfunc();
        }
        this.Picker = null;
    }
    if (strCurrentResultFieldId == textboxid) {
        return;
    }
    if (textboxid != null) {
        this.Picker = document.getElementById(iframeid);
        if (this.Picker == null)
            return;
        g_scrollLeft = document.body.scrollLeft;
        g_scrollTop = document.body.scrollTop;
        if (typeof (this.Picker.attachEvent) == 'object')
            this.Picker.attachEvent("onreadystatechange", OnIframeLoadFinish);
        else
            this.Picker.addEventListener("onreadystatechange", OnIframeLoadFinish, false);
        this.Picker.resultfield = document.getElementById(textboxid);
        this.Picker.OnSelectDateCallback = OnSelectDateCallback;
        this.Picker.resultfunc = onpickerfinishcallback;
        var strNewPickerSrc = PageUrlValidation(iframesrc) + escapeProperly(datestr);
        this.Picker.src = strNewPickerSrc;
        var iframeTop = getOffsetTop(objImage, 1);
        var iframeLeft = getOffsetLeft(objImage, 1);
        var containerTop = getOffsetTop(this.Picker.offsetParent, 1);
        var containerLeft = getOffsetLeft(this.Picker.offsetParent, 1);
        this.Picker.style.top = iframeTop - containerTop + objImage.offsetHeight + 1;
        this.Picker.style.right = iframeLeft - containerLeft + objImage.offsetWidth + 1;
        if (this.Picker.style.direction == "rtl") {
            var cx = this.Picker.offsetParent.offsetWidth;
            this.Picker.style.left = iframeLeft - containerLeft + objImage.offsetWidth + 1;
            this.Picker.style.left = cx - this.Picker.style.left;
        }
        else {
            this.Picker.style.right = iframeLeft - containerLeft + objImage.offsetWidth + 1;
        }
    }
}
function ClickDay(date) {
    var ifrm = window.frameElement
    if (ifrm == null) {
        return MoveToDate(date);
    }
    var eltValidator = window.parent.document.all(ifrm.resultfield.id + g_strDatePickerRangeValidatorID);
    if (eltValidator != null) {
        eltValidator.style.display = "none";
    }
    var OnSelectDateCallback = ifrm.OnSelectDateCallback;
    OnSelectDateCallback(ifrm.resultfield, date);
    var resultfunc = ifrm.resultfunc;
    resultfunc(ifrm.resultfield);
    return true;
}
function OnPickerFinish(resultfield) {
    clickDatePicker(null, "", "");
}
function OnSelectDate(resultfield, date) {
    var autoPostBack = resultfield.attributes.getNamedItem("AutoPostBack");
    var shouldPostBack = (autoPostBack != null && autoPostBack.value == "1" && resultfield.value != date);
    var shouldNotifyChange = (resultfield.value != date);
    resultfield.value = date;
    if ((shouldNotifyChange) && (resultfield.onvaluesetfrompicker) && (!shouldPostBack)) {
        if (typeof (resultfield.onvaluesetfrompicker) == 'function') {
            resultfield.onvaluesetfrompicker();
        }
        else {
            eval(resultfield.onvaluesetfrompicker);
        }
    }
    if (shouldPostBack) window.setTimeout("__doPostBack('" + resultfield.id + "','')", 0);
}
function ChangeDateTimeControlState(id, disable) {
    var elmDate = document.getElementById(g_strDateTimeControlIDs[id]);
    if (elmDate != null)
        elmDate.disabled = disable;
    var elmHours = document.getElementById(g_strDateTimeControlIDs[id] + "Hours");
    if (elmHours != null)
        elmHours.disabled = disable;
    var elmMinutes = document.getElementById(g_strDateTimeControlIDs[id] + "Minutes");
    if (elmMinutes != null)
        elmMinutes.disabled = disable;
    var elmImage = document.getElementById(g_strDateTimeControlIDs[id] + "DatePickerImage");
    if (elmImage != null) {
        if (disable)
            elmImage.src = "/_layouts/images/calendar_grey.gif";
        else
            elmImage.src = "/_layouts/images/calendar.gif";
    }
}
function EnableDateTimeControl(id) {
    ChangeDateTimeControlState(id, false);
}
function DisableDateTimeControl(id) {
    ChangeDateTimeControlState(id, true);
}
function OnIframeLoadFinish(state) {
    if (this.Picker != null &&
		this.Picker.readyState != null &&
		this.Picker.readyState == "complete") {
        document.body.scrollLeft = g_scrollLeft;
        document.body.scrollTop = g_scrollTop;
        this.Picker.style.display = "block";
        document.frames(this.Picker.id).focus();
    }
}
function RecurPatternType_ShowDiv(bShow) {
    var item = document.getElementById("recurCustomDiv");
    if (item != null) { item.style.display = bShow ? 'block' : 'none'; }
}
function RecurPatternType_ShowRecurType(id) {
    var key; var item;
    var a = new Array('recurDailyDiv', 'recurWeeklyDiv', 'recurMonthlyDiv', 'recurYearlyDiv');
    for (key in a) {
        item = document.getElementById(a[key]);
        if (item != null)
        { item.style.display = 'none'; }
    }
    var itemID = document.getElementById(id);
    item = document.getElementById(a[itemID.value - 2]);
    if (item != null) { item.style.display = 'block'; }
    RecurPatternType_ShowDiv((itemID.value == 6) ? false : true);
    if (itemID.value != 6 && g_warnonce == 0) {
        alert(L_WarnkOnce_text);
        g_warnonce++;
    }
}
function RecurType_SetRadioButton1(id) {
    var itemID = document.getElementById(id);
    if (itemID != null) { item.checked = true; }
}
function RecurType_SetRadioButton(trobj, idValue) {
    if (trobj == null) return;
    var childtd1 = trobj.firstChild;
    if (childtd1.nodeType == 1) {
        var str = childtd1.innerHTML;
        str = str.substr(str.indexOf("id=") + 3);
        str = str.substr(0, str.indexOf(" "));
        if (str.indexOf(idValue) > 0) {
            var itemID = document.getElementById(str);
            if (itemID != null) { itemID.checked = true; }
        }
    }
}

