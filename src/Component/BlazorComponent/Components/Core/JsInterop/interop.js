"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __spreadArray = (this && this.__spreadArray) || function (to, from) {
    for (var i = 0, il = from.length, j = to.length; i < il; i++, j++)
        to[j] = from[i];
    return to;
};
exports.__esModule = true;
exports.disposeObj = exports.resizeTextArea = exports.disposeResizeTextArea = exports.registerResizeTextArea = exports.getTextAreaInfo = exports.getStyle = exports.getMaxZIndex = exports.getInnerText = exports.getScroll = exports.createIconFromfontCN = exports.destroyAllDialog = exports.enableBodyScroll = exports.disableBodyScroll = exports.elementScrollIntoView = exports.removeCls = exports.addCls = exports.css = exports.getWindow = exports.focusDialog = exports.getActiveElement = exports.delElementFrom = exports.addElementTo = exports.delElementFromBody = exports.addElementToBody = exports.getAbsoluteLeft = exports.getAbsoluteTop = exports.addDomEventListenerToFirstChild = exports.removeClsFromFirstChild = exports.addClsToFirstChild = exports.getFirstChildDomInfo = exports.scrollTo = exports.backTop = exports.log = exports.blur = exports.hasFocus = exports.focus = exports.copy = exports.matchMedia = exports.addDomEventListener = exports.getBoundingClientRect = exports.triggerEvent = exports.uploadFile = exports.getObjectURL = exports.getFileInfo = exports.clearFile = exports.fileClickEvent = exports.removeFileClickEventListener = exports.addFileClickEventListener = exports.getDomInfo = exports.getDom = void 0;
exports.insertAdjacentHTML = exports.removePreventEnterOnOverlayVisible = exports.addPreventEnterOnOverlayVisible = exports.removePreventKeys = exports.addPreventKeys = exports.unbindTableHeaderAndBodyScroll = exports.bindTableHeaderAndBodyScroll = exports.resetModalPosition = exports.disableDraggable = exports.enableDraggable = exports.getCursorXY = void 0;
function getDom(element) {
    if (!element) {
        element = document.body;
    }
    else if (typeof element === 'string') {
        if (element === 'document') {
            return document;
        }
        element = document.querySelector(element);
    }
    return element;
}
exports.getDom = getDom;
function getDomInfo(element) {
    var result = {};
    var dom = getDom(element);
    result["offsetTop"] = dom.offsetTop || 0;
    result["offsetLeft"] = dom.offsetLeft || 0;
    result["offsetWidth"] = dom.offsetWidth || 0;
    result["offsetHeight"] = dom.offsetHeight || 0;
    result["scrollHeight"] = dom.scrollHeight || 0;
    result["scrollWidth"] = dom.scrollWidth || 0;
    result["scrollLeft"] = dom.scrollLeft || 0;
    result["scrollTop"] = dom.scrollTop || 0;
    result["clientTop"] = dom.clientTop || 0;
    result["clientLeft"] = dom.clientLeft || 0;
    result["clientHeight"] = dom.clientHeight || 0;
    result["clientWidth"] = dom.clientWidth || 0;
    var absolutePosition = getElementAbsolutePos(dom);
    result["absoluteTop"] = Math.round(absolutePosition.y);
    result["absoluteLeft"] = Math.round(absolutePosition.x);
    return result;
}
exports.getDomInfo = getDomInfo;
function getElementAbsolutePos(element) {
    var res = new Object();
    res.x = 0;
    res.y = 0;
    if (element !== null) {
        if (element.getBoundingClientRect) {
            var viewportElement = document.documentElement;
            var box = element.getBoundingClientRect();
            var scrollLeft = viewportElement.scrollLeft;
            var scrollTop = viewportElement.scrollTop;
            res.x = box.left + scrollLeft;
            res.y = box.top + scrollTop;
        }
    }
    return res;
}
function addFileClickEventListener(btn) {
    if (btn.addEventListener) {
        btn.addEventListener("click", fileClickEvent);
    }
}
exports.addFileClickEventListener = addFileClickEventListener;
function removeFileClickEventListener(btn) {
    btn.removeEventListener("click", fileClickEvent);
}
exports.removeFileClickEventListener = removeFileClickEventListener;
function fileClickEvent() {
    var fileId = this.attributes["data-fileid"].nodeValue;
    var element = document.getElementById(fileId);
    element.click();
}
exports.fileClickEvent = fileClickEvent;
function clearFile(element) {
    element.setAttribute("type", "input");
    element.value = '';
    element.setAttribute("type", "file");
}
exports.clearFile = clearFile;
function getFileInfo(element) {
    if (element.files && element.files.length > 0) {
        var fileInfo = [];
        for (var i = 0; i < element.files.length; i++) {
            var file = element.files[i];
            var objectUrl = getObjectURL(file);
            fileInfo.push({
                fileName: file.name,
                size: file.size,
                objectURL: objectUrl,
                type: file.type
            });
        }
        return fileInfo;
    }
}
exports.getFileInfo = getFileInfo;
function getObjectURL(file) {
    var url = null;
    if (window.URL != undefined) {
        url = window.URL.createObjectURL(file);
    }
    else if (window.webkitURL != undefined) {
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}
exports.getObjectURL = getObjectURL;
function uploadFile(element, index, data, headers, fileId, url, name, instance, percentMethod, successMethod, errorMethod) {
    var formData = new FormData();
    var file = element.files[index];
    var size = file.size;
    formData.append(name, file);
    if (data != null) {
        for (var key in data) {
            formData.append(key, data[key]);
        }
    }
    var req = new XMLHttpRequest();
    req.onreadystatechange = function () {
        if (req.readyState === 4) {
            if (req.status != 200) {
                instance.invokeMethodAsync(errorMethod, fileId, "{\"status\": " + req.status + "}");
                return;
            }
            instance.invokeMethodAsync(successMethod, fileId, req.responseText);
        }
    };
    req.upload.onprogress = function (event) {
        var percent = Math.floor(event.loaded / size * 100);
        instance.invokeMethodAsync(percentMethod, fileId, percent);
    };
    req.onerror = function (e) {
        instance.invokeMethodAsync(errorMethod, fileId, "error");
    };
    req.open('post', url, true);
    if (headers != null) {
        for (var header in headers) {
            req.setRequestHeader(header, headers[header]);
        }
    }
    req.send(formData);
}
exports.uploadFile = uploadFile;
function triggerEvent(element, eventType, eventName) {
    var dom = element;
    var evt = document.createEvent(eventType);
    evt.initEvent(eventName);
    return dom.dispatchEvent(evt);
}
exports.triggerEvent = triggerEvent;
function getBoundingClientRect(element) {
    var dom = getDom(element);
    if (dom && dom.getBoundingClientRect) {
        return dom.getBoundingClientRect();
    }
    return null;
}
exports.getBoundingClientRect = getBoundingClientRect;
function addDomEventListener(element, eventName, preventDefault, invoker) {
    var callback = function (args) {
        var obj = {};
        for (var k in args) {
            if (k !== 'originalTarget') { //firefox occasionally raises Permission Denied when this property is being stringified
                obj[k] = args[k];
            }
        }
        var json = JSON.stringify(obj, function (k, v) {
            if (v instanceof Node)
                return 'Node';
            if (v instanceof Window)
                return 'Window';
            return v;
        }, ' ');
        invoker.invokeMethodAsync('Invoke', json);
        if (preventDefault === true) {
            args.preventDefault();
        }
    };
    if (element == 'window') {
        if (eventName == 'resize') {
            window.addEventListener(eventName, debounce(function () { return callback({ innerWidth: window.innerWidth, innerHeight: window.innerHeight }); }, 200, false));
        }
        else {
            window.addEventListener(eventName, callback);
        }
    }
    else {
        var dom = getDom(element);
        dom.addEventListener(eventName, callback);
    }
}
exports.addDomEventListener = addDomEventListener;
function matchMedia(query) {
    return window.matchMedia(query).matches;
}
exports.matchMedia = matchMedia;
function fallbackCopyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;
    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";
    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();
    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Fallback: Copying text command was ' + msg);
    }
    catch (err) {
        console.error('Fallback: Oops, unable to copy', err);
    }
    document.body.removeChild(textArea);
}
function copy(text) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text);
        return;
    }
    navigator.clipboard.writeText(text).then(function () {
        console.log('Async: Copying to clipboard was successful!');
    }, function (err) {
        console.error('Async: Could not copy text: ', err);
    });
}
exports.copy = copy;
function focus(selector, noScroll) {
    if (noScroll === void 0) { noScroll = false; }
    var dom = getDom(selector);
    if (!(dom instanceof HTMLElement))
        throw new Error("Unable to focus an invalid element.");
    dom.focus({
        preventScroll: noScroll
    });
}
exports.focus = focus;
function hasFocus(selector) {
    var dom = getDom(selector);
    return (document.activeElement === dom);
}
exports.hasFocus = hasFocus;
function blur(selector) {
    var dom = getDom(selector);
    dom.blur();
}
exports.blur = blur;
function log(text) {
    console.log(text);
}
exports.log = log;
function backTop(target) {
    var dom = getDom(target);
    if (dom) {
        slideTo(dom.scrollTop);
    }
    else {
        slideTo(0);
    }
}
exports.backTop = backTop;
function slideTo(targetPageY) {
    var timer = setInterval(function () {
        var currentY = document.documentElement.scrollTop || document.body.scrollTop;
        var distance = targetPageY > currentY ? targetPageY - currentY : currentY - targetPageY;
        var speed = Math.ceil(distance / 10);
        if (currentY == targetPageY) {
            clearInterval(timer);
        }
        else {
            window.scrollTo(0, targetPageY > currentY ? currentY + speed : currentY - speed);
        }
    }, 10);
}
function scrollTo(target) {
    var dom = getDom(target);
    if (dom instanceof HTMLElement) {
        dom.scrollIntoView({
            behavior: "smooth",
            block: "start",
            inline: "nearest"
        });
    }
}
exports.scrollTo = scrollTo;
function getFirstChildDomInfo(element) {
    var dom = getDom(element);
    if (dom.firstElementChild)
        return getDomInfo(dom.firstElementChild);
    return getDomInfo(dom);
}
exports.getFirstChildDomInfo = getFirstChildDomInfo;
function addClsToFirstChild(element, className) {
    var dom = getDom(element);
    if (dom.firstElementChild) {
        dom.firstElementChild.classList.add(className);
    }
}
exports.addClsToFirstChild = addClsToFirstChild;
function removeClsFromFirstChild(element, className) {
    var dom = getDom(element);
    if (dom.firstElementChild) {
        dom.firstElementChild.classList.remove(className);
    }
}
exports.removeClsFromFirstChild = removeClsFromFirstChild;
function addDomEventListenerToFirstChild(element, eventName, preventDefault, invoker) {
    var dom = getDom(element);
    if (dom.firstElementChild) {
        addDomEventListener(dom.firstElementChild, eventName, preventDefault, invoker);
    }
}
exports.addDomEventListenerToFirstChild = addDomEventListenerToFirstChild;
function getAbsoluteTop(e) {
    var offset = e.offsetTop;
    if (e.offsetParent != null) {
        offset += getAbsoluteTop(e.offsetParent);
    }
    return offset;
}
exports.getAbsoluteTop = getAbsoluteTop;
function getAbsoluteLeft(e) {
    var offset = e.offsetLeft;
    if (e.offsetParent != null) {
        offset += getAbsoluteLeft(e.offsetParent);
    }
    return offset;
}
exports.getAbsoluteLeft = getAbsoluteLeft;
function addElementToBody(element) {
    document.body.appendChild(element);
}
exports.addElementToBody = addElementToBody;
function delElementFromBody(element) {
    document.body.removeChild(element);
}
exports.delElementFromBody = delElementFromBody;
function addElementTo(addElement, elementSelector) {
    var parent = getDom(elementSelector);
    if (parent && addElement) {
        parent.appendChild(addElement);
    }
}
exports.addElementTo = addElementTo;
function delElementFrom(delElement, elementSelector) {
    var parent = getDom(elementSelector);
    if (parent && delElement) {
        parent.removeChild(delElement);
    }
}
exports.delElementFrom = delElementFrom;
function getActiveElement() {
    var element = document.activeElement;
    var id = element.getAttribute("id") || "";
    return id;
}
exports.getActiveElement = getActiveElement;
function focusDialog(selector, count) {
    if (count === void 0) { count = 0; }
    var ele = document.querySelector(selector);
    if (ele && !ele.hasAttribute("disabled")) {
        setTimeout(function () {
            ele.focus();
            var curId = "#" + getActiveElement();
            if (curId !== selector) {
                if (count < 10) {
                    focusDialog(selector, count + 1);
                }
            }
        }, 10);
    }
}
exports.focusDialog = focusDialog;
function getWindow() {
    return {
        innerWidth: window.innerWidth,
        innerHeight: window.innerHeight
    };
}
exports.getWindow = getWindow;
function debounce(func, wait, immediate) {
    var _this = this;
    var timeout;
    return function () {
        var context = _this, args = arguments;
        var later = function () {
            timeout = null;
            if (!immediate)
                func.apply(_this, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow)
            func.apply(context, args);
    };
}
;
function css(element, name, value) {
    if (value === void 0) { value = null; }
    if (typeof name === 'string') {
        element.style[name] = value;
    }
    else {
        for (var key in name) {
            if (name.hasOwnProperty(key)) {
                element.style[key] = name[key];
            }
        }
    }
}
exports.css = css;
function addCls(selector, clsName) {
    var _a;
    var element = getDom(selector);
    if (typeof clsName === "string") {
        element.classList.add(clsName);
    }
    else {
        (_a = element.classList).add.apply(_a, clsName);
    }
}
exports.addCls = addCls;
function removeCls(selector, clsName) {
    var _a;
    var element = getDom(selector);
    if (typeof clsName === "string") {
        element.classList.remove(clsName);
    }
    else {
        (_a = element.classList).remove.apply(_a, clsName);
    }
}
exports.removeCls = removeCls;
function elementScrollIntoView(selector) {
    var element = getDom(selector);
    if (!element)
        return;
    element.scrollIntoView({ behavior: 'smooth', block: 'nearest', inline: 'start' });
}
exports.elementScrollIntoView = elementScrollIntoView;
var oldBodyCacheStack = [];
var hasScrollbar = function () {
    var overflow = document.body.style.overflow;
    if (overflow && overflow === "hidden")
        return false;
    return document.body.scrollHeight > (window.innerHeight || document.documentElement.clientHeight);
};
function disableBodyScroll() {
    var body = document.body;
    var oldBodyCache = {};
    ["position", "width", "overflow"].forEach(function (key) {
        oldBodyCache[key] = body.style[key];
    });
    oldBodyCacheStack.push(oldBodyCache);
    css(body, {
        "position": "relative",
        "width": hasScrollbar() ? "calc(100% - 17px)" : null,
        "overflow": "hidden"
    });
    addCls(document.body, "ant-scrolling-effect");
}
exports.disableBodyScroll = disableBodyScroll;
function enableBodyScroll() {
    var _a, _b, _c;
    var oldBodyCache = oldBodyCacheStack.length > 0 ? oldBodyCacheStack.pop() : {};
    css(document.body, {
        "position": (_a = oldBodyCache["position"]) !== null && _a !== void 0 ? _a : null,
        "width": (_b = oldBodyCache["width"]) !== null && _b !== void 0 ? _b : null,
        "overflow": (_c = oldBodyCache["overflow"]) !== null && _c !== void 0 ? _c : null
    });
    removeCls(document.body, "ant-scrolling-effect");
}
exports.enableBodyScroll = enableBodyScroll;
function destroyAllDialog() {
    document.querySelectorAll('.ant-modal-root')
        .forEach(function (e) { return document.body.removeChild(e.parentNode); });
}
exports.destroyAllDialog = destroyAllDialog;
function createIconFromfontCN(scriptUrl) {
    if (document.querySelector("[data-namespace=\"" + scriptUrl + "\"]")) {
        return;
    }
    var script = document.createElement('script');
    script.setAttribute('src', scriptUrl);
    script.setAttribute('data-namespace', scriptUrl);
    document.body.appendChild(script);
}
exports.createIconFromfontCN = createIconFromfontCN;
function getScroll() {
    return { x: window.pageXOffset, y: window.pageYOffset };
}
exports.getScroll = getScroll;
function getInnerText(element) {
    var dom = getDom(element);
    return dom.innerText;
}
exports.getInnerText = getInnerText;
function getMaxZIndex() {
    return __spreadArray([], document.all).reduce(function (r, e) { return Math.max(r, +window.getComputedStyle(e).zIndex || 0); }, 0);
}
exports.getMaxZIndex = getMaxZIndex;
function getStyle(element, styleProp) {
    if (element.currentStyle)
        return element.currentStyle[styleProp];
    else if (window.getComputedStyle)
        return document.defaultView.getComputedStyle(element, null).getPropertyValue(styleProp);
}
exports.getStyle = getStyle;
function getTextAreaInfo(element) {
    var result = {};
    var dom = getDom(element);
    result["scrollHeight"] = dom.scrollHeight || 0;
    if (element.currentStyle) {
        result["lineHeight"] = parseFloat(element.currentStyle["line-height"]);
        result["paddingTop"] = parseFloat(element.currentStyle["padding-top"]);
        result["paddingBottom"] = parseFloat(element.currentStyle["padding-bottom"]);
        result["borderBottom"] = parseFloat(element.currentStyle["border-bottom"]);
        result["borderTop"] = parseFloat(element.currentStyle["border-top"]);
    }
    else if (window.getComputedStyle) {
        result["lineHeight"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("line-height"));
        result["paddingTop"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("padding-top"));
        result["paddingBottom"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("padding-bottom"));
        result["borderBottom"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("border-bottom"));
        result["borderTop"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("border-top"));
    }
    //Firefox can return this as NaN, so it has to be handled here like that.
    if (Object.is(NaN, result["borderTop"]))
        result["borderTop"] = 1;
    if (Object.is(NaN, result["borderBottom"]))
        result["borderBottom"] = 1;
    return result;
}
exports.getTextAreaInfo = getTextAreaInfo;
var funcDict = {};
function registerResizeTextArea(element, minRows, maxRows, objReference) {
    if (!objReference) {
        disposeResizeTextArea(element);
    }
    else {
        objReferenceDict[element.id] = objReference;
        funcDict[element.id + "input"] = function () { resizeTextArea(element, minRows, maxRows); };
        element.addEventListener("input", funcDict[element.id + "input"]);
        return getTextAreaInfo(element);
    }
}
exports.registerResizeTextArea = registerResizeTextArea;
function disposeResizeTextArea(element) {
    element.removeEventListener("input", funcDict[element.id + "input"]);
    objReferenceDict[element.id] = null;
    funcDict[element.id + "input"] = null;
}
exports.disposeResizeTextArea = disposeResizeTextArea;
function resizeTextArea(element, minRows, maxRows) {
    var dims = getTextAreaInfo(element);
    var rowHeight = dims["lineHeight"];
    var offsetHeight = dims["paddingTop"] + dims["paddingBottom"] + dims["borderTop"] + dims["borderBottom"];
    var oldHeight = parseFloat(element.style.height);
    element.style.height = 'auto';
    var rows = Math.trunc(element.scrollHeight / rowHeight);
    rows = Math.max(minRows, rows);
    var newHeight = 0;
    if (rows > maxRows) {
        rows = maxRows;
        newHeight = (rows * rowHeight + offsetHeight);
        element.style.height = newHeight + "px";
        element.style.overflowY = "visible";
    }
    else {
        newHeight = rows * rowHeight + offsetHeight;
        element.style.height = newHeight + "px";
        element.style.overflowY = "hidden";
    }
    if (oldHeight !== newHeight) {
        var textAreaObj = objReferenceDict[element.id];
        textAreaObj.invokeMethodAsync("ChangeSizeAsyncJs", parseFloat(element.scrollWidth), newHeight);
    }
}
exports.resizeTextArea = resizeTextArea;
var objReferenceDict = {};
function disposeObj(objReferenceName) {
    delete objReferenceDict[objReferenceName];
}
exports.disposeObj = disposeObj;
//#region mentions
var Caret_1 = require("./modules/Caret");
function getCursorXY(element, objReference) {
    objReferenceDict["mentions"] = objReference;
    window.addEventListener("click", mentionsOnWindowClick);
    var offset = Caret_1["default"](element);
    return [offset.left, offset.top + offset.height + 14];
}
exports.getCursorXY = getCursorXY;
function mentionsOnWindowClick(e) {
    var mentionsObj = objReferenceDict["mentions"];
    if (mentionsObj) {
        mentionsObj.invokeMethodAsync("CloseMentionsDropDown");
    }
    else {
        window.removeEventListener("click", mentionsOnWindowClick);
    }
}
//#endregion
var dragHelper_1 = require("./modules/dragHelper");
__createBinding(exports, dragHelper_1, "enableDraggable");
__createBinding(exports, dragHelper_1, "disableDraggable");
__createBinding(exports, dragHelper_1, "resetModalPosition");
function bindTableHeaderAndBodyScroll(bodyRef, headerRef) {
    bodyRef.bindScrollLeftToHeader = function () {
        headerRef.scrollLeft = bodyRef.scrollLeft;
    };
    bodyRef.addEventListener('scroll', bodyRef.bindScrollLeftToHeader);
}
exports.bindTableHeaderAndBodyScroll = bindTableHeaderAndBodyScroll;
function unbindTableHeaderAndBodyScroll(bodyRef) {
    if (bodyRef) {
        bodyRef.removeEventListener('scroll', bodyRef.bindScrollLeftToHeader);
    }
}
exports.unbindTableHeaderAndBodyScroll = unbindTableHeaderAndBodyScroll;
function preventKeys(e, keys) {
    if (keys.indexOf(e.key.toUpperCase()) !== -1) {
        e.preventDefault();
        return false;
    }
}
function addPreventKeys(inputElement, keys) {
    if (inputElement) {
        var dom = getDom(inputElement);
        keys = keys.map(function (x) { return x.toUpperCase(); });
        funcDict[inputElement.id + "keydown"] = function (e) { return preventKeys(e, keys); };
        dom.addEventListener("keydown", funcDict[inputElement.id + "keydown"], false);
    }
}
exports.addPreventKeys = addPreventKeys;
function removePreventKeys(inputElement) {
    if (inputElement) {
        var dom = getDom(inputElement);
        dom.removeEventListener("keydown", funcDict[inputElement.id + "keydown"]);
        funcDict[inputElement.id + "keydown"] = null;
    }
}
exports.removePreventKeys = removePreventKeys;
function preventKeyOnCondition(e, key, check) {
    if (e.key.toUpperCase() === key.toUpperCase() && check()) {
        e.preventDefault();
        return false;
    }
}
function addPreventEnterOnOverlayVisible(element, overlayElement) {
    if (element && overlayElement) {
        var dom = getDom(element);
        funcDict[element.id + "keydown:Enter"] = function (e) { return preventKeyOnCondition(e, "enter", function () { return overlayElement.offsetParent !== null; }); };
        dom.addEventListener("keydown", funcDict[element.id + "keydown:Enter"], false);
    }
}
exports.addPreventEnterOnOverlayVisible = addPreventEnterOnOverlayVisible;
function removePreventEnterOnOverlayVisible(element) {
    if (element) {
        var dom = getDom(element);
        dom.removeEventListener("keydown", funcDict[element.id + "keydown:Enter"]);
        funcDict[element.id + "keydown:Enter"] = null;
    }
}
exports.removePreventEnterOnOverlayVisible = removePreventEnterOnOverlayVisible;
function insertAdjacentHTML(position, text) {
    document.head.insertAdjacentHTML(position, text);
}
exports.insertAdjacentHTML = insertAdjacentHTML;
