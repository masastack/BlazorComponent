!function o(i,r,l){function s(t,e){if(!r[t]){if(!i[t]){var n="function"==typeof require&&require;if(!e&&n)return n(t,!0);if(d)return d(t,!0);throw(n=new Error("Cannot find module '"+t+"'")).code="MODULE_NOT_FOUND",n}n=r[t]={exports:{}},i[t][0].call(n.exports,function(e){return s(i[t][1][e]||e)},n,n.exports,o,i,r,l)}return r[t].exports}for(var d="function"==typeof require&&require,e=0;e<l.length;e++)s(l[e]);return s}({1:[function(e,t,n){"use strict";Object.defineProperty(n,"__esModule",{value:!0});e=e("./src/interop");window.BlazorComponent={interop:e}},{"./src/interop":2}],2:[function(e,t,n){"use strict";function o(e){if(Array.isArray(e)){for(var t=0,n=Array(e.length);t<e.length;t++)n[t]=e[t];return n}return Array.from(e)}function l(e){if(e){if("string"==typeof e){if("document"===e)return document;e=document.querySelector(e)}}else e=document.body;return e}function s(e){var t=1<arguments.length&&void 0!==arguments[1]?arguments[1]:"body",n={},o=l(e);return o.style&&"none"===o.style.display?((e=o.cloneNode(!0)).style.display="inline-block",e.style["z-index"]=-1e3,document.querySelector(t).appendChild(e),n=i(e),document.querySelector(t).removeChild(e)):(console.log("dom.offsetWidth",o.offsetWidth),n=i(o)),n}function i(e){var t={};t.offsetTop=e.offsetTop||0,t.offsetLeft=e.offsetLeft||0,t.scrollHeight=e.scrollHeight||0,t.scrollWidth=e.scrollWidth||0,t.scrollLeft=e.scrollLeft||0,t.scrollTop=e.scrollTop||0,t.clientTop=e.clientTop||0,t.clientLeft=e.clientLeft||0,t.clientHeight=e.clientHeight||0,t.clientWidth=e.clientWidth||0;e=function(e){var t=new Object;{var n,o;t.x=0,t.y=0,null===e||e.getBoundingClientRect&&(o=document.documentElement,n=e.getBoundingClientRect(),e=o.scrollLeft,o=o.scrollTop,t.offsetWidth=n.width,t.offsetHeight=n.height,t.relativeLeft=n.left,t.relativeTop=n.top,t.absoluteLeft=n.left+e,t.absoluteTop=n.top+o)}return t}(e);return t.offsetWidth=Math.round(e.offsetWidth)||0,t.offsetHeight=Math.round(e.offsetHeight)||0,t.relativeLeft=Math.round(e.relativeLeft)||0,t.relativeTop=Math.round(e.relativeTop)||0,t.absoluteLeft=Math.round(e.absoluteLeft)||0,t.absoluteTop=Math.round(e.absoluteTop)||0,t}function r(){var e=this.attributes["data-fileid"].nodeValue;document.getElementById(e).click()}function d(e){var t=null;return null!=window.URL?t=window.URL.createObjectURL(e):null!=window.webkitURL&&(t=window.webkitURL.createObjectURL(e)),t}function a(e){e=l(e);return e&&e.getBoundingClientRect?e.getBoundingClientRect():null}function c(e,t,i,r){function n(e){var t,n={};for(t in e)"originalTarget"!==t&&(n[t]=e[t]);var o=JSON.stringify(n,function(e,t){return t instanceof Node?"Node":t instanceof Window?"Window":t}," ");r.invokeMethodAsync("Invoke",o),!0===i&&e.preventDefault()}var o;"window"==e?"resize"==t?window.addEventListener(t,p(function(){return n({innerWidth:window.innerWidth,innerHeight:window.innerHeight})},200,!1)):window.addEventListener(t,n):(e=o=l(e),"scroll"==t?e.addEventListener(t,p(function(){return n(s(o))},200,!1)):e.addEventListener(t,n))}function u(n){var o=setInterval(function(){var e=document.documentElement.scrollTop||document.body.scrollTop,t=Math.ceil((e<n?n-e:e-n)/10);e==n?clearInterval(o):window.scrollTo(0,e<n?e+t:e-t)},10)}function f(){return document.activeElement.getAttribute("id")||""}function p(o,i,r){var l,s=this,d=arguments;return function(){var e=s,t=d,n=r&&!l;clearTimeout(l),l=setTimeout(function(){l=null,r||o.apply(s,t)},i),n&&o.apply(e,t)}}function m(e,t){if("string"==typeof t)e.style[t]=2<arguments.length&&void 0!==arguments[2]?arguments[2]:null;else for(var n in t)t.hasOwnProperty(n)&&(e.style[n]=t[n])}function h(e,t){e=l(e);"string"==typeof t?e.classList.add(t):(e=e.classList).add.apply(e,o(t))}function g(e,t){e=l(e);"string"==typeof t?e.classList.remove(t):(e=e.classList).remove.apply(e,o(t))}Object.defineProperty(n,"__esModule",{value:!0}),n.registerResizeTextArea=n.getTextAreaInfo=n.getStyle=n.getMaxZIndex=n.getInnerText=n.getScroll=n.createIconFromfontCN=n.destroyAllDialog=n.enableBodyScroll=n.disableBodyScroll=n.elementScrollIntoView=n.removeCls=n.addCls=n.css=n.getWindow=n.focusDialog=n.getActiveElement=n.delElementFrom=n.addElementTo=n.delElementFromBody=n.addElementToBody=n.getAbsoluteLeft=n.getAbsoluteTop=n.addDomEventListenerToFirstChild=n.removeClsFromFirstChild=n.addClsToFirstChild=n.getFirstChildDomInfo=n.scrollToPosition=n.scrollToElement=n.scrollTo=n.backTop=n.log=n.blur=n.hasFocus=n.focus=n.copy=n.matchMedia=n.addDomEventListener=n.getFirstChildBoundingClientRect=n.getBoundingClientRect=n.triggerEvent=n.uploadFile=n.getObjectURL=n.getFileInfo=n.clearFile=n.fileClickEvent=n.removeFileClickEventListener=n.addFileClickEventListener=n.getDomInfo=n.getDom=void 0,n.getSize=n.getBoundingClientRects=n.observer=n.preventDefaultOnArrowUpDown=n.getImageDimensions=n.insertAdjacentHTML=n.removePreventEnterOnOverlayVisible=n.addPreventEnterOnOverlayVisible=n.removePreventKeys=n.addPreventKeys=n.unbindTableHeaderAndBodyScroll=n.bindTableHeaderAndBodyScroll=n.resetModalPosition=n.enableDraggable=n.disableDraggable=n.getCursorXY=n.disposeObj=n.resizeTextArea=n.disposeResizeTextArea=void 0,n.getDom=l,n.getDomInfo=s,n.addFileClickEventListener=function(e){e.addEventListener&&e.addEventListener("click",r)},n.removeFileClickEventListener=function(e){e.removeEventListener("click",r)},n.fileClickEvent=r,n.clearFile=function(e){e.setAttribute("type","input"),e.value="",e.setAttribute("type","file")},n.getFileInfo=function(e){if(e.files&&0<e.files.length){for(var t=[],n=0;n<e.files.length;n++){var o=e.files[n],i=d(o);t.push({fileName:o.name,size:o.size,objectURL:i,type:o.type})}return t}},n.getObjectURL=d,n.uploadFile=function(e,t,n,o,i,r,l,s,d,a,c){var u=new FormData,f=(t=e.files[t]).size;if(u.append(l,t),null!=n)for(var p in n)u.append(p,n[p]);var m=new XMLHttpRequest;if(m.onreadystatechange=function(){4===m.readyState&&(200==m.status?s.invokeMethodAsync(a,i,m.responseText):s.invokeMethodAsync(c,i,'{"status": '+m.status+"}"))},m.upload.onprogress=function(e){e=Math.floor(e.loaded/f*100);s.invokeMethodAsync(d,i,e)},m.onerror=function(e){s.invokeMethodAsync(c,i,"error")},m.open("post",r,!0),null!=o)for(var h in o)m.setRequestHeader(h,o[h]);m.send(u)},n.triggerEvent=function(e,t,n){return(t=document.createEvent(t)).initEvent(n),e.dispatchEvent(t)},n.getBoundingClientRect=a,n.getFirstChildBoundingClientRect=function(e){var t=1<arguments.length&&void 0!==arguments[1]?arguments[1]:"body",n=l(e);if(n.firstElementChild){if("none"!==n.firstElementChild.style.display)return a(n.firstElementChild);e=n.firstElementChild.cloneNode(!0);e.style.display="inline-block",e.style["z-index"]=-1e3,document.querySelector(t).appendChild(e);n=a(e);return document.querySelector(t).removeChild(e),n}return null},n.addDomEventListener=c,n.matchMedia=function(e){return window.matchMedia(e).matches},n.copy=function(e){navigator.clipboard?navigator.clipboard.writeText(e).then(function(){console.log("Async: Copying to clipboard was successful!")},function(e){console.error("Async: Could not copy text: ",e)}):function(e){var t=document.createElement("textarea");t.value=e,t.style.top="0",t.style.left="0",t.style.position="fixed",document.body.appendChild(t),t.focus(),t.select();try{var n=document.execCommand("copy")?"successful":"unsuccessful";console.log("Fallback: Copying text command was "+n)}catch(e){console.error("Fallback: Oops, unable to copy",e)}document.body.removeChild(t)}(e)},n.focus=function(e){var t=1<arguments.length&&void 0!==arguments[1]&&arguments[1],e=l(e);if(!(e instanceof HTMLElement))throw new Error("Unable to focus an invalid element.");e.focus({preventScroll:t})},n.hasFocus=function(e){return e=l(e),document.activeElement===e},n.blur=function(e){l(e).blur()},n.log=function(e){console.log(e)},n.backTop=function(e){u((e=l(e))?e.scrollTop:0)},n.scrollTo=function(e){(e=l(e))instanceof HTMLElement&&e.scrollIntoView({behavior:"smooth",block:"start",inline:"nearest"})},n.scrollToElement=function(e,t){e=l(e),t=s(t),e.scrollTop=t.offsetTop-e.offsetHeight/2+t.offsetHeight/2},n.scrollToPosition=function(e,t){l(e).scrollTop=t},n.getFirstChildDomInfo=function(e){var t=1<arguments.length&&void 0!==arguments[1]?arguments[1]:"body",e=l(e);return e.firstElementChild?s(e.firstElementChild,t):s(e,t)},n.addClsToFirstChild=function(e,t){(e=l(e)).firstElementChild&&e.firstElementChild.classList.add(t)},n.removeClsFromFirstChild=function(e,t){(e=l(e)).firstElementChild&&e.firstElementChild.classList.remove(t)},n.addDomEventListenerToFirstChild=function(e,t,n,o){(e=l(e)).firstElementChild&&c(e.firstElementChild,t,n,o)},n.getAbsoluteTop=function e(t){var n=t.offsetTop;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},n.getAbsoluteLeft=function e(t){var n=t.offsetLeft;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},n.addElementToBody=function(e){document.body.appendChild(e)},n.delElementFromBody=function(e){document.body.removeChild(e)},n.addElementTo=function(e,t){(t=l(t))&&e&&t.appendChild(e)},n.delElementFrom=function(e,t){(t=l(t))&&e&&t.removeChild(e)},n.getActiveElement=f,n.focusDialog=function e(t){var n=1<arguments.length&&void 0!==arguments[1]?arguments[1]:0,o=document.querySelector(t);o&&!o.hasAttribute("disabled")&&setTimeout(function(){o.focus(),"#"+f()!==t&&n<10&&e(t,n+1)},10)},n.getWindow=function(){return{innerWidth:window.innerWidth,innerHeight:window.innerHeight,isTop:0==window.scrollY,isBottom:window.scrollY+window.innerHeight==document.body.clientHeight}},n.css=m,n.addCls=h,n.removeCls=g,n.elementScrollIntoView=function(e){(e=l(e))&&e.scrollIntoView({behavior:"smooth",block:"nearest",inline:"start"})};var v=[];function y(e){var t={},n=l(e);return t.scrollHeight=n.scrollHeight||0,e.currentStyle?(t.lineHeight=parseFloat(e.currentStyle["line-height"]),t.paddingTop=parseFloat(e.currentStyle["padding-top"]),t.paddingBottom=parseFloat(e.currentStyle["padding-bottom"]),t.borderBottom=parseFloat(e.currentStyle["border-bottom"]),t.borderTop=parseFloat(e.currentStyle["border-top"])):window.getComputedStyle&&(t.lineHeight=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("line-height")),t.paddingTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-top")),t.paddingBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-bottom")),t.borderBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-bottom")),t.borderTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-top"))),Object.is(NaN,t.borderTop)&&(t.borderTop=1),Object.is(NaN,t.borderBottom)&&(t.borderBottom=1),t}n.disableBodyScroll=function(){var e,t=document.body,n={};["position","width","overflow"].forEach(function(e){n[e]=t.style[e]}),v.push(n),m(t,{position:"relative",width:(!(e=document.body.style.overflow)||"hidden"!==e)&&document.body.scrollHeight>(window.innerHeight||document.documentElement.clientHeight)?"calc(100% - 17px)":null,overflow:"hidden"}),h(document.body,"ant-scrolling-effect")},n.enableBodyScroll=function(){var e,t=0<v.length?v.pop():{};m(document.body,{position:null!==(e=t.position)&&void 0!==e?e:null,width:null!==(e=t.width)&&void 0!==e?e:null,overflow:null!==(t=t.overflow)&&void 0!==t?t:null}),g(document.body,"ant-scrolling-effect")},n.destroyAllDialog=function(){document.querySelectorAll(".ant-modal-root").forEach(function(e){return document.body.removeChild(e.parentNode)})},n.createIconFromfontCN=function(e){var t;document.querySelector('[data-namespace="'+e+'"]')||((t=document.createElement("script")).setAttribute("src",e),t.setAttribute("data-namespace",e),document.body.appendChild(t))},n.getScroll=function(){return{x:window.pageXOffset,y:window.pageYOffset}},n.getInnerText=function(e){return l(e).innerText},n.getMaxZIndex=function(){return[].concat(o(document.all)).reduce(function(e,t){return Math.max(e,+window.getComputedStyle(t).zIndex||0)},0)},n.getStyle=function(e,t){return e.currentStyle?e.currentStyle[t]:window.getComputedStyle?document.defaultView.getComputedStyle(e,null).getPropertyValue(t):void 0},n.getTextAreaInfo=y;var b={};function w(e){e.removeEventListener("input",b[e.id+"input"]),C[e.id]=null,b[e.id+"input"]=null}function E(e,t,n){var o=y(e),i=o.lineHeight,r=o.paddingTop+o.paddingBottom+o.borderTop+o.borderBottom,l=parseFloat(e.style.height);e.style.height="auto";var s=Math.trunc(e.scrollHeight/i),o=0;n<(s=Math.max(t,s))?(e.style.height=(o=(s=n)*i+r)+"px",e.style.overflowY="visible"):(e.style.height=(o=s*i+r)+"px",e.style.overflowY="hidden"),l!==o&&C[e.id].invokeMethodAsync("ChangeSizeAsyncJs",parseFloat(e.scrollWidth),o)}n.registerResizeTextArea=function(e,t,n,o){if(o)return C[e.id]=o,b[e.id+"input"]=function(){E(e,t,n)},e.addEventListener("input",b[e.id+"input"]),y(e);w(e)},n.disposeResizeTextArea=w,n.resizeTextArea=E;var C={};n.disposeObj=function(e){delete C[e]};var T=e("./modules/Caret");function L(e){var t=C.mentions;t?t.invokeMethodAsync("CloseMentionsDropDown"):window.removeEventListener("click",L)}n.getCursorXY=function(e,t){return C.mentions=t,window.addEventListener("click",L),[(e=T.default(e)).left,e.top+e.height+14]};var S=e("./modules/dragHelper");Object.defineProperty(n,"disableDraggable",{enumerable:!0,get:function(){return S.disableDraggable}}),Object.defineProperty(n,"enableDraggable",{enumerable:!0,get:function(){return S.enableDraggable}}),Object.defineProperty(n,"resetModalPosition",{enumerable:!0,get:function(){return S.resetModalPosition}}),n.bindTableHeaderAndBodyScroll=function(e,t){e.bindScrollLeftToHeader=function(){t.scrollLeft=e.scrollLeft},e.addEventListener("scroll",e.bindScrollLeftToHeader)},n.unbindTableHeaderAndBodyScroll=function(e){e&&e.removeEventListener("scroll",e.bindScrollLeftToHeader)},n.addPreventKeys=function(e,t){var n;e&&(n=l(e),t=t.map(function(e){return e.toUpperCase()}),b[e.id+"keydown"]=function(e){return function(e,t){if(-1!==t.indexOf(e.key.toUpperCase()))return e.preventDefault(),!1}(e,t)},n.addEventListener("keydown",b[e.id+"keydown"],!1))},n.removePreventKeys=function(e){e&&(l(e).removeEventListener("keydown",b[e.id+"keydown"]),b[e.id+"keydown"]=null)},n.addPreventEnterOnOverlayVisible=function(e,t){var n;e&&t&&(n=l(e),b[e.id+"keydown:Enter"]=function(e){return function(e,t,n){if(e.key.toUpperCase()===t.toUpperCase()&&n())return e.preventDefault(),!1}(e,"enter",function(){return null!==t.offsetParent})},n.addEventListener("keydown",b[e.id+"keydown:Enter"],!1))},n.removePreventEnterOnOverlayVisible=function(e){e&&(l(e).removeEventListener("keydown",b[e.id+"keydown:Enter"]),b[e.id+"keydown:Enter"]=null)},n.insertAdjacentHTML=function(e,t){document.head.insertAdjacentHTML(e,t)},n.getImageDimensions=function(o){return new Promise(function(e,t){var n=new Image;n.src=o,n.onload=function(){e({width:n.width,height:n.height,hasError:!1})},n.onerror=function(){e({width:0,height:0,hasError:!0})}})},n.preventDefaultOnArrowUpDown=function(e){l(e).addEventListener("keydown",function(e){"ArrowUp"!=e.code&&"ArrowDown"!=e.code&&"Enter"!=e.code||e.preventDefault()})},n.observer=function(e,d){new ResizeObserver(function(e){var t=[],n=!0,o=!1,i=void 0;try{for(var r,l=e[Symbol.iterator]();!(n=(r=l.next()).done);n=!0){var s=r.value.contentRect;t.push(s)}}catch(e){o=!0,i=e}finally{try{!n&&l.return&&l.return()}finally{if(o)throw i}}d.invokeMethodAsync("Invoke",t)}).observe(l(e))},n.getBoundingClientRects=function(e){for(var t=document.querySelectorAll(e),n=[],o=0;o<t.length;o++){var i=t[o],i={id:i.id,rect:i.getBoundingClientRect()};n.push(i)}return n},n.getSize=function(e,t){var n=e.style.display,o=e.style.overflow;return e.style.display="",e.style.overflow="hidden",t=e["offset"+t]||0,e.style.display=n,e.style.overflow=o,t}},{"./modules/Caret":3,"./modules/dragHelper":4}],3:[function(e,t,n){"use strict";function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}Object.defineProperty(n,"__esModule",{value:!0});function i(e){o(this,i),this.getPos=function(){return this.domInputor.selectionStart},this.getPosition=function(e){var t=this.domInputor,n=function(e){return e=e.replace(/<|>|`|"|&/g,"?").replace(/\r\n|\r|\n/g,"<br/>"),e=/firefox/i.test(navigator.userAgent)?e.replace(/\s/g,"&nbsp;"):e};e=e||this.getPos();var o=t.value,i=o.slice(0,e),e=o.slice(e),i="<span style='position: relative; display: inline;'>"+n(i)+"</span>";return i+="<span id='caret' style='position: relative; display: inline;'>|</span>",i+="<span style='position: relative; display: inline;'>"+n(e)+"</span>",new r(t).create(i).rect()},this.getOffset=function(){var e=0<arguments.length&&void 0!==arguments[0]?arguments[0]:null,t=this.domInputor,n=t.getBoundingClientRect(),n={left:n.left,top:n.top},e=this.getPosition(e);return{left:n.left+e.left-t.scrollLeft,top:n.top+e.top-t.scrollTop,height:e.height}},this.domInputor=e}var r=function e(t){o(this,e),this.create=function(e){return this.$mirror=document.createElement("div"),window.AntDesign.interop.css(this.$mirror,this.mirrorCss()),this.$mirror.innerHTML=e,this.domInputor.parentElement.append(this.$mirror),this},this.mirrorCss=function(){var t=this,n={position:"absolute",left:-9999,top:0,zIndex:-2e4};return this.css_attr.push("width"),this.css_attr.forEach(function(e){return n[e]=t.domInputor.style[e]}),n},this.rect=function(){var e=this.$mirror.querySelector("#caret"),t=e.getBoundingClientRect(),e={left:e.offsetLeft,top:e.offsetTop},t={left:e.left,top:e.top,height:t.height};return this.$mirror.parentElement.removeChild(this.$mirror),t},this.domInputor=t,this.css_attr=[]};n.default=function(e){return new i(e).getOffset()}},{}],4:[function(e,t,n){"use strict";var o=function(e,t,n){return t&&i(e.prototype,t),n&&i(e,n),e};function i(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}Object.defineProperty(n,"__esModule",{value:!0}),n.resetModalPosition=n.disableDraggable=n.enableDraggable=void 0;function r(r){var l=1<arguments.length&&void 0!==arguments[1]?arguments[1]:160,s=void 0,d=+new Date;return function(){for(var e=this,t=arguments.length,n=Array(t),o=0;o<t;o++)n[o]=arguments[o];var i=+new Date;window.clearTimeout(s),l<=i-d?(r.apply(this,n),d=i):s=window.setTimeout(function(){r.apply(e,n)},l)}}var l=new Map,s={inViewport:!0},d=(o(a,[{key:"getContainerPos",value:function(){var e=this._container.getBoundingClientRect();return{left:e.left,top:e.top}}},{key:"bindDrag",value:function(){var e=this._triggler,t=this._options;e.addEventListener("mousedown",this.onMousedown,!1),window.addEventListener("mouseup",this.onMouseup,!1),document.addEventListener("mousemove",this.onMousemove),t.inViewport&&window.addEventListener("resize",this.onResize,!1)}},{key:"unbindDrag",value:function(){this._triggler.removeEventListener("mousedown",this.onMousedown,!1),window.removeEventListener("mouseup",this.onMouseup,!1),document.removeEventListener("mousemove",this.onMousemove),this._options.inViewport&&window.removeEventListener("resize",this.onResize,!1)}},{key:"resetContainerStyle",value:function(){null!==this._style&&(this._isFirst=!0,this._container.setAttribute("style",this._style))}}]),a);function a(e,t,n){var i=this;!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,a),this._triggler=null,this._container=null,this._options=null,this._state=null,this._isFirst=!0,this._style=null,this.onMousedown=function(e){var t=i._state;t.isInDrag=!0,t.mX=e.clientX,t.mY=e.clientY,i._container.style.position="absolute";var n=i.getContainerPos(),e=n.left,n=n.top;i._isFirst&&(t.domMaxY=document.documentElement.clientHeight-i._container.offsetHeight-1,t.domMaxX=document.documentElement.clientWidth-i._container.offsetWidth-1,i._container.style.left=e+"px",i._container.style.top=n+"px",i._style||(i._style=i._container.getAttribute("style")),i._isFirst=!1),t.domStartX=e,t.domStartY=n},this.onMouseup=function(e){var t=i._state;t.isInDrag=!1;var n=i.getContainerPos(),o=n.left,n=n.top;t.domStartX=o,t.domStartY=n},this.onMousemove=r(function(e){var t,n=i._state;n.isInDrag&&(t=e.clientX,e=e.clientY,t=t-n.mX,e=e-n.mY,t=n.domStartX+t,e=n.domStartY+e,i._options.inViewport&&(t<0?t=0:t>n.domMaxX&&(t=n.domMaxX),e<0?e=0:e>n.domMaxY&&(e=n.domMaxY)),i._container.style.position="absolute",i._container.style.margin="0",i._container.style.paddingBottom="0",i._container.style.left=t+"px",i._container.style.top=e+"px")},10).bind(this),this.onResize=r(function(e){var t=i._state;t.domMaxY=document.documentElement.clientHeight-i._container.offsetHeight-1,t.domMaxX=document.documentElement.clientWidth-i._container.offsetWidth-1,t.domStartY=parseInt(i._container.style.top),t.domStartX=parseInt(i._container.style.left),t.domStartY>t.domMaxY&&0<t.domMaxY&&(i._container.style.top=t.domMaxY+"px"),t.domStartX>t.domMaxX&&(i._container.style.left=t.domMaxX+"px")},10).bind(this),this._triggler=e,this._container=t,this._options=Object.assign({},s,{inViewport:n}),this._state={isInDrag:!1,mX:0,mY:0,domStartX:0,domStartY:0}}n.enableDraggable=function(e,t){var n=!(2<arguments.length&&void 0!==arguments[2])||arguments[2],o=l.get(e);o||(o=new d(e,t,n),l.set(e,o)),o.bindDrag()},n.disableDraggable=function(e){(e=l.get(e))&&e.unbindDrag()},n.resetModalPosition=function(e){(e=l.get(e))&&e.resetContainerStyle()}},{}]},{},[1]);
//# sourceMappingURL=blazor-component.js.map
