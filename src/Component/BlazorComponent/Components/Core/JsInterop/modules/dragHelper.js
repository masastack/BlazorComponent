"use strict";
exports.__esModule = true;
exports.resetModalPosition = exports.disableDraggable = exports.enableDraggable = void 0;
var throttle = function (fn, threshold) {
    if (threshold === void 0) { threshold = 160; }
    var timeout;
    var start = +new Date;
    return function () {
        var _this = this;
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i] = arguments[_i];
        }
        var context = this, curTime = +new Date() - 0;
        //总是干掉事件回调
        window.clearTimeout(timeout);
        if (curTime - start >= threshold) {
            //只执行一部分方法，这些方法是在某个时间段内执行一次
            fn.apply(context, args);
            start = curTime;
        }
        else {
            //让方法在脱离事件后也能执行一次
            timeout = window.setTimeout(function () {
                //@ts-ignore
                fn.apply(_this, args);
            }, threshold);
        }
    };
};
var eventMap = new Map();
var defaultOptions = {
    inViewport: true
};
var Dragger = /** @class */ (function () {
    function Dragger(triggler, container, dragInViewport) {
        var _this = this;
        this._triggler = null;
        this._container = null;
        this._options = null;
        this._state = null;
        this._isFirst = true;
        this._style = null;
        this.onMousedown = function (e) {
            var state = _this._state;
            state.isInDrag = true;
            state.mX = e.clientX;
            state.mY = e.clientY;
            _this._container.style.position = "absolute";
            var _a = _this.getContainerPos(), left = _a.left, top = _a.top;
            if (_this._isFirst) {
                state.domMaxY = document.documentElement.clientHeight
                    - _this._container.offsetHeight - 1;
                state.domMaxX = document.documentElement.clientWidth
                    - _this._container.offsetWidth - 1;
                _this._container.style.left = left + 'px';
                _this._container.style.top = top + 'px';
                if (!_this._style) {
                    _this._style = _this._container.getAttribute("style");
                }
                _this._isFirst = false;
            }
            state.domStartX = left;
            state.domStartY = top;
        };
        this.onMouseup = function (e) {
            var state = _this._state;
            state.isInDrag = false;
            var _a = _this.getContainerPos(), left = _a.left, top = _a.top;
            state.domStartX = left;
            state.domStartY = top;
        };
        this.onMousemove = throttle(function (e) {
            var state = _this._state;
            if (state.isInDrag) {
                var nowX = e.clientX, nowY = e.clientY, disX = nowX - state.mX, disY = nowY - state.mY;
                var newDomX = state.domStartX + disX;
                var newDomY = state.domStartY + disY;
                if (_this._options.inViewport) {
                    if (newDomX < 0) {
                        newDomX = 0;
                    }
                    else if (newDomX > state.domMaxX) {
                        newDomX = state.domMaxX;
                    }
                    if (newDomY < 0) {
                        newDomY = 0;
                    }
                    else if (newDomY > state.domMaxY) {
                        newDomY = state.domMaxY;
                    }
                }
                _this._container.style.position = "absolute";
                _this._container.style.margin = "0";
                _this._container.style.paddingBottom = "0";
                _this._container.style.left = newDomX + "px";
                _this._container.style.top = newDomY + "px";
            }
        }, 10).bind(this);
        this.onResize = throttle(function (e) {
            var state = _this._state;
            state.domMaxY = document.documentElement.clientHeight
                - _this._container.offsetHeight - 1;
            state.domMaxX = document.documentElement.clientWidth
                - _this._container.offsetWidth - 1;
            state.domStartY = parseInt(_this._container.style.top);
            state.domStartX = parseInt(_this._container.style.left);
            if (state.domStartY > state.domMaxY) {
                if (state.domMaxY > 0) {
                    _this._container.style.top = state.domMaxY + "px";
                }
            }
            if (state.domStartX > state.domMaxX) {
                _this._container.style.left = state.domMaxX + "px";
            }
        }, 10).bind(this);
        this._triggler = triggler;
        this._container = container;
        this._options = Object.assign({}, defaultOptions, {
            inViewport: dragInViewport
        });
        this._state = {
            isInDrag: false,
            mX: 0,
            mY: 0,
            domStartX: 0,
            domStartY: 0
        };
    }
    Dragger.prototype.getContainerPos = function () {
        var rect = this._container.getBoundingClientRect();
        return {
            left: rect.left,
            top: rect.top
        };
    };
    Dragger.prototype.bindDrag = function () {
        var triggler = this._triggler;
        var options = this._options;
        triggler.addEventListener("mousedown", this.onMousedown, false);
        window.addEventListener("mouseup", this.onMouseup, false);
        document.addEventListener("mousemove", this.onMousemove);
        if (options.inViewport) {
            window.addEventListener("resize", this.onResize, false);
        }
    };
    Dragger.prototype.unbindDrag = function () {
        var triggler = this._triggler;
        triggler.removeEventListener("mousedown", this.onMousedown, false);
        window.removeEventListener("mouseup", this.onMouseup, false);
        document.removeEventListener("mousemove", this.onMousemove);
        if (this._options.inViewport) {
            window.removeEventListener("resize", this.onResize, false);
        }
    };
    Dragger.prototype.resetContainerStyle = function () {
        if (this._style !== null) {
            this._isFirst = true;
            this._container.setAttribute("style", this._style);
        }
    };
    return Dragger;
}());
function enableDraggable(triggler, container, dragInViewport) {
    if (dragInViewport === void 0) { dragInViewport = true; }
    var dragger = eventMap.get(triggler);
    if (!dragger) {
        dragger = new Dragger(triggler, container, dragInViewport);
        eventMap.set(triggler, dragger);
    }
    dragger.bindDrag();
}
exports.enableDraggable = enableDraggable;
function disableDraggable(triggler) {
    var dragger = eventMap.get(triggler);
    if (dragger) {
        dragger.unbindDrag();
    }
}
exports.disableDraggable = disableDraggable;
function resetModalPosition(triggler) {
    var dragger = eventMap.get(triggler);
    if (dragger) {
        dragger.resetContainerStyle();
    }
}
exports.resetModalPosition = resetModalPosition;
