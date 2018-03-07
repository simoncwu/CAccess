var $buoop = { vs: { i: navigator.userAgent.indexOf("MSIE 8.0; Windows NT 5") > 0 ? 7 : 8} }
$(function () {
    $.getScript("//browser-update.org/update.js");
});