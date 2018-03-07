function PageUrlValidation(url) {
    if (url.substr(0, 4) != "http" && url.substr(0, 1) != "/") {
        var L_InvalidPageUrl_Text = "Invalid page URL: ";
        alert(L_InvalidPageUrl_Text);
        return "";
    }
    else
        return url;
}
function Vutf8ToUnicode(rgBytes) {
    var ix = 0;
    var strResult = "";
    var dwch, wch, uch;
    var nTrailBytes, nTrailBytesOrig;
    while (ix < rgBytes.length) {
        if (rgBytes[ix] <= 0x007f) {
            strResult += String.fromCharCode(rgBytes[ix++]);
        }
        else {
            uch = rgBytes[ix++];
            nTrailBytes = ((uch) & 0x20) ? (((uch) & 0x10) ? 3 : 2) : 1;
            dwch = uch & (0xff >>> (2 + nTrailBytes));
            while (nTrailBytes && (ix < rgBytes.length)) {
                --nTrailBytes;
                uch = rgBytes[ix++];
                if (uch == 0) {
                    return strResult;
                }
                if ((uch & 0xC0) != 0x80) {
                    strResult += '?';
                    break;
                }
                dwch = (dwch << 6) | ((uch) & 0x003f);
            }
            if (nTrailBytes) {
                strResult += '?';
                break;
            }
            if (dwch < g_rgdwchMinEncoded[nTrailBytesOrig]) {
                strResult += '?';
                break;
            }
            else if (dwch <= 0xffff) {
                strResult += String.fromCharCode(dwch);
            }
            else if (dwch <= 0x10ffff) {
                dwch -= SURROGATE_OFFSET;
                strResult += String.fromCharCode(
					HIGH_SURROGATE_BITS | dwch >>> 10);
                strResult += String.fromCharCode(
					LOW_SURROGATE_BITS | ((dwch) & 0x003FF));
            }
            else {
                strResult += '?';
            }
        }
    }
    return strResult;
}
function unescapeProperlyInternal(str) {
    if (str == null)
        return "null";
    var ix = 0, ixEntity = 0;
    var strResult = "";
    var rgUTF8Bytes = new Array;
    var ixUTF8Bytes = 0;
    var hexString, hexCode;
    while (ix < str.length) {
        if (str.charAt(ix) == '%') {
            if (str.charAt(++ix) == 'u') {
                hexString = "";
                for (ixEntity = 0; ixEntity < 4 && ix < str.length; ++ixEntity) {
                    hexString += str.charAt(++ix);
                }
                while (hexString.length < 4) {
                    hexString += '0';
                }
                hexCode = parseInt(hexString, 16);
                if (isNaN(hexCode)) {
                    strResult += '?';
                }
                else {
                    strResult += String.fromCharCode(hexCode);
                }
            }
            else {
                hexString = "";
                for (ixEntity = 0; ixEntity < 2 && ix < str.length; ++ixEntity) {
                    hexString += str.charAt(ix++);
                }
                while (hexString.length < 2) {
                    hexString += '0';
                }
                hexCode = parseInt(hexString, 16);
                if (isNaN(hexCode)) {
                    if (ixUTF8Bytes) {
                        strResult += Vutf8ToUnicode(rgUTF8Bytes);
                        ixUTF8Bytes = 0;
                        rgUTF8Bytes.length = ixUTF8Bytes;
                    }
                    strResult += '?';
                }
                else {
                    rgUTF8Bytes[ixUTF8Bytes++] = hexCode;
                }
            }
        }
        else {
            if (ixUTF8Bytes) {
                strResult += Vutf8ToUnicode(rgUTF8Bytes);
                ixUTF8Bytes = 0;
                rgUTF8Bytes.length = ixUTF8Bytes;
            }
            strResult += str.charAt(ix++);
        }
    }
    if (ixUTF8Bytes) {
        strResult += Vutf8ToUnicode(rgUTF8Bytes);
        ixUTF8Bytes = 0;
        rgUTF8Bytes.length = ixUTF8Bytes;
    }
    return strResult;
}
function STSPageUrlValidation(url) {
    return PageUrlValidation(url);
}
function GetUrlKeyValue(keyName, bNoDecode, url) {
    var keyValue = "";
    if (url == null)
        url = window.location.href + "";
    var ndx = url.indexOf("&" + keyName + "=");
    if (ndx == -1)
        ndx = url.indexOf("?" + keyName + "=");
    if (ndx != -1) {
        ndx2 = url.indexOf("&", ndx + 1);
        if (ndx2 == -1)
            ndx2 = url.length;
        keyValue = url.substring(ndx + keyName.length + 2, ndx2);
    }
    if (bNoDecode)
        return keyValue;
    else
        return unescapeProperlyInternal(keyValue);
}
function isPortalTemplatePage(Url) {
    if (GetUrlKeyValue("PortalTemplate") == "1" ||
		GetUrlKeyValue("PortalTemplate", Url) == "1" ||
		(currentCtx != null && currentCtx.isPortalTemplate))
        return true;
    else
        return false;
}
