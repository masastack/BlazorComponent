!function(){"use strict";var e=function(e,t,n){var o=null,i=null,r=function(){o&&(clearTimeout(o),i=null,o=null)},l=function(){if(!t)return e.apply(this,arguments);var l=this,s=arguments,a=n&&!o;return r(),i=function(){e.apply(l,s)},o=setTimeout((function(){if(o=null,!a){var e=i;return i=null,e()}}),t),a?i():void 0};return l.cancel=r,l.flush=function(){var e=i;r(),e&&e()},l};var t=function(e,t,n){var o=null,i=null,r=n&&n.leading,l=n&&n.trailing;null==r&&(r=!0);null==l&&(l=!r);1==r&&(l=!1);var s=function(){o&&(clearTimeout(o),o=null)},a=function(){var n=r&&!o,s=this,a=arguments;if(i=function(){return e.apply(s,a)},o||(o=setTimeout((function(){if(o=null,l)return i()}),t)),n)return n=!1,i()};return a.cancel=s,a.flush=function(){var e=i;s(),e&&e()},a};function n(e,t){e.style.transform=t,e.style.webkitTransform=t}function o(e,t){e.style.opacity=t.toString()}const i={show(e,t){const i=document.createElement("span"),r=document.createElement("span");i.appendChild(r),i.className="m-ripple__container";const{radius:l,scale:s,x:a,y:c,centerX:u,centerY:d}=((e,t)=>{let n=0,o=0;const i=t.getBoundingClientRect(),r=e;n=r.clientX-i.left,o=r.clientY-i.top;let l=0;return l=Math.sqrt(t.clientWidth**2+t.clientHeight**2)/2,{radius:l,scale:.3,x:n-l+"px",y:o-l+"px",centerX:(t.clientWidth-2*l)/2+"px",centerY:(t.clientHeight-2*l)/2+"px"}})(e,t),f=2*l+"px";r.className="m-ripple__animation",r.style.width=f,r.style.height=f,t.appendChild(i);const p=window.getComputedStyle(t);p&&"static"===p.position&&(t.style.position="relative",t.dataset.previousPosition="static"),r.classList.add("m-ripple__animation--enter"),r.classList.add("m-ripple__animation--visible"),n(r,`translate(${a}, ${c}) scale3d(${s},${s},${s})`),o(r,0),r.dataset.activated=String(performance.now()),setTimeout((()=>{r.classList.remove("m-ripple__animation--enter"),r.classList.add("m-ripple__animation--in"),n(r,`translate(${u}, ${d}) scale3d(1,1,1)`),o(r,.25)}),0)},hide(e){const t=e.getElementsByClassName("m-ripple__animation");if(0===t.length)return;const n=t[t.length-1];if(n.dataset.isHiding)return;n.dataset.isHiding="true";const i=performance.now()-Number(n.dataset.activated),r=Math.max(250-i,0);setTimeout((()=>{n.classList.remove("m-ripple__animation--in"),n.classList.add("m-ripple__animation--out"),o(n,0),setTimeout((()=>{1===e.getElementsByClassName("m-ripple__animation").length&&e.dataset.previousPosition&&(e.style.position=e.dataset.previousPosition,delete e.dataset.previousPosition),n.parentNode&&e&&e.removeChild(n.parentNode)}),300)}),r)}};function r(e){i.show(e,e.currentTarget)}function l(e){i.hide(e.currentTarget)}const s=["touchcancel","touchend","touchmove","touchenter","touchleave","touchstart"];function a(e){return{detail:e.detail,screenX:e.screenX,screenY:e.screenY,clientX:e.clientX,clientY:e.clientY,offsetX:e.offsetX,offsetY:e.offsetY,pageX:e.pageX,pageY:e.pageY,button:e.button,buttons:e.buttons,ctrlKey:e.ctrlKey,shiftKey:e.shiftKey,altKey:e.altKey,metaKey:e.metaKey,type:e.type}}function c(e){return{detail:e.detail,touches:u(e.touches),targetTouches:u(e.targetTouches),changedTouches:u(e.changedTouches),ctrlKey:e.ctrlKey,shiftKey:e.shiftKey,altKey:e.altKey,metaKey:e.metaKey,type:e.type}}function u(e){const t=[];for(let n=0;n<e.length;n++){const o=e[n];t.push({identifier:o.identifier,clientX:o.clientX,clientY:o.clientY,screenX:o.screenX,screenY:o.screenY,pageX:o.pageX,pageY:o.pageY})}return t}let d=!1;try{if("undefined"!=typeof window){const e=Object.defineProperty({},"passive",{get:()=>{d=!0}});window.addEventListener("testListener",e,e),window.removeEventListener("testListener",e,e)}}catch(e){console.warn(e)}const f=Object.freeze({enter:13,tab:9,delete:46,esc:27,space:32,up:38,down:40,left:37,right:39,end:35,home:36,del:46,backspace:8,insert:45,pageup:33,pagedown:34,shift:16});function p(e){if(!e)return null;let t=e.getAttributeNames().find((e=>e.startsWith("_bl_")));return t&&(t=t.substring(4)),t}function m(e){if(e instanceof Element){for(var t=[];e.nodeType===Node.ELEMENT_NODE;){var n=e.nodeName.toLowerCase();if(e.id){n+="#"+e.id,t.unshift(n);break}for(var o=e,i=1;o=o.previousElementSibling;)o.nodeName.toLowerCase()==n&&i++;1!=i&&(n+=":nth-of-type("+i+")"),t.unshift(n),e=e.parentNode}return t.join(" > ")}}function g(e){let t;try{if(e)if("string"==typeof e)if("document"===e)t=document.documentElement;else if(e.indexOf("__.__")>0){let n=e.split("__.__"),o=0,i=document.querySelector(n[o++]);if(i)for(;n[o];)i=i[n[o]],o++;i instanceof HTMLElement&&(t=i)}else t=document.querySelector(e);else t=e;else t=document.body}catch(e){console.error(e)}return t}const h=!("undefined"==typeof window||"undefined"==typeof document||!window.document||!window.document.createElement);function v(e,t){Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t,createEventArgs:e=>w("mouse",e)})}function y(e,t){Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t,createEventArgs:e=>{const t=(n=e,Object.assign(Object.assign({},a(n)),{dataTransfer:n.dataTransfer?{dropEffect:n.dataTransfer.dropEffect,effectAllowed:n.dataTransfer.effectAllowed,files:Array.from(n.dataTransfer.files).map((e=>e.name)),items:Array.from(n.dataTransfer.items).map((e=>({kind:e.kind,type:e.type}))),types:n.dataTransfer.types}:null}));var n;const o=e.dataTransfer.getData("data-value"),i=e.dataTransfer.getData("offsetX"),r=e.dataTransfer.getData("offsetY");return t.dataTransfer.data={value:o,offsetX:Number(i),offsetY:Number(r)},t}})}function w(e,t){let n={target:{}};if("mouse"===e?n=Object.assign(Object.assign({},n),a(t)):"touch"===e&&(n=Object.assign(Object.assign({},n),c(t))),t.target){const e=t.target,o=e.getAttributeNames().find((e=>e.startsWith("_bl_")));o?(n.target.elementReferenceId=o,n.target.selector=`[${o}]`):n.target.selector=m(e),n.target.class=e.getAttribute("class")}return n}function b(){var e,t;v("exmousedown","mousedown"),v("exmouseup","mouseup"),v("exclick","click"),v("exmouseleave","mouseleave"),v("exmouseenter","mouseenter"),v("exmousemove","mousemove"),e="extouchstart",t="touchstart",Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t,createEventArgs:e=>w("touch",e)}),function(e,t){Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t})}("transitionend","transitionend"),y("exdrop","drop")}function E(e){if(!e||e.nodeType!==Node.ELEMENT_NODE)return 0;const t=+window.getComputedStyle(e).getPropertyValue("z-index");return t||E(e.parentNode)}function T(e){var t={};t.offsetTop=e.offsetTop||0,t.offsetLeft=e.offsetLeft||0,t.scrollHeight=e.scrollHeight||0,t.scrollWidth=e.scrollWidth||0,t.scrollLeft=e.scrollLeft||0,t.scrollTop=e.scrollTop||0,t.clientTop=e.clientTop||0,t.clientLeft=e.clientLeft||0,t.clientHeight=e.clientHeight||0,t.clientWidth=e.clientWidth||0;var n=function(e){var t=new Object;if(t.x=0,t.y=0,null!==e&&e.getBoundingClientRect){var n=document.documentElement,o=e.getBoundingClientRect(),i=n.scrollLeft,r=n.scrollTop;t.offsetWidth=o.width,t.offsetHeight=o.height,t.relativeTop=o.top,t.relativeBottom=o.bottom,t.relativeLeft=o.left,t.relativeRight=o.right,t.absoluteLeft=o.left+i,t.absoluteTop=o.top+r}return t}(e);return t.offsetWidth=Math.round(n.offsetWidth)||0,t.offsetHeight=Math.round(n.offsetHeight)||0,t.relativeTop=Math.round(n.relativeTop)||0,t.relativeBottom=Math.round(n.relativeBottom)||0,t.relativeLeft=Math.round(n.relativeLeft)||0,t.relativeRight=Math.round(n.relativeRight)||0,t.absoluteLeft=Math.round(n.absoluteLeft)||0,t.absoluteTop=Math.round(n.absoluteTop)||0,t}var L={};function C(){return document.activeElement.getAttribute("id")||""}function x(e=[],t=[]){const n={};return e&&e.forEach((e=>n[e]=window[e])),t&&t.forEach((e=>n[e]=document.documentElement[e])),n}function S(e){return"HTML"!==e.tagName&&"BODY"!==e.tagName&&1==e.nodeType}function _(e=[],t){const n=[E(g(t))],o=[...document.getElementsByClassName("m-menu__content--active"),...document.getElementsByClassName("m-dialog__content--active")];for(let t=0;t<o.length;t++)e.includes(o[t])||n.push(E(o[t]));return Math.max(...n)}function N(e,t,n,o,i,r){if(!i){var l=document.querySelector(r);o.nodeType&&l.appendChild(o)}var s={activator:{},content:{},relativeYOffset:0,offsetParentLeft:0};if(e){var a=document.querySelector(t);s.activator=k(a,n),s.activator.offsetLeft=a.offsetLeft,s.activator.offsetTop=n?0:a.offsetTop}return function(e,t){if(!t||!t.style||"none"!==t.style.display)return void e();t.style.display="inline-block",e(),t.style.display="none"}((()=>{if(o){if(o.offsetParent){const t=H(o.offsetParent);s.relativeYOffset=window.pageYOffset+t.top,e?(s.activator.top-=s.relativeYOffset,s.activator.left-=window.pageXOffset+t.left):s.offsetParentLeft=t.left}s.content=k(o,n)}}),o),s}function k(e,t){if(!e)return{};const n=H(e);if(!t){const t=window.getComputedStyle(e);n.left=parseInt(t.marginLeft),n.top=parseInt(t.marginTop)}return n}function H(e){if(!e||!e.nodeType)return null;const t=e.getBoundingClientRect();return{top:Math.round(t.top),left:Math.round(t.left),bottom:Math.round(t.bottom),right:Math.round(t.right),width:Math.round(t.width),height:Math.round(t.height)}}function O(e,t,n,o){e.preventDefault();const i=e.key;if("ArrowLeft"===i||"Backspace"===i){if("Backspace"===i){const e={type:i,index:t,value:""};o&&o.invokeMethodAsync("Invoke",JSON.stringify(e))}A(t-1,n)}else"ArrowRight"===i&&A(t+1,n)}function A(e,t){if(e<0)A(0,t);else if(e>=t.length)A(t.length-1,t);else if(document.activeElement!==t[e]){g(t[e]).focus()}}function M(e,t,n){const o=g(n[t]);o&&document.activeElement===o&&o.select()}function B(e,t,n,o){const i=e.target.value;if(i&&""!==i&&(A(t+1,n),o)){const e={type:"Input",index:t,value:i};o.invokeMethodAsync("Invoke",JSON.stringify(e))}}function Y(){var e,t,n="weird_get_top_level_domain=cookie",o=document.location.hostname.split(".");for(e=o.length-1;e>=0;e--)if(t=o.slice(e).join("."),document.cookie=n+";domain=."+t+";",document.cookie.indexOf(n)>-1)return document.cookie=n.split("=")[0]+"=;domain=."+t+";expires=Thu, 01 Jan 1970 00:00:01 GMT;",t}window.onload=function(){var e;b(),e="pastewithdata",Blazor&&Blazor.registerCustomEventType(e,{browserEventName:"paste",createEventArgs:e=>({type:e.type,pastedData:e.clipboardData.getData("text")})}),new MutationObserver((function(e){for(let i of e)if("childList"===i.type){if(!(o=i.target)._bind&&o.attributes&&o.attributes.ripple)o.addEventListener("mousedown",r),o.addEventListener("mouseup",l),o.addEventListener("mouseleave",l),o._bind=!0;else if("BODY"==o.nodeName){var t=document.querySelectorAll("[ripple]");for(let e of t){var n=e;n._bind||(n.addEventListener("mousedown",r),n.addEventListener("mouseup",l),n.addEventListener("mouseleave",l),n._bind=!0)}}}else if("attributes"===i.type){var o;"ripple"==i.attributeName&&((o=i.target).attributes.ripple?(o.addEventListener("mousedown",r),o.addEventListener("mouseup",l),o.addEventListener("mouseleave",l)):(o.removeEventListener("mousedown",r),o.removeEventListener("mouseup",l),o.removeEventListener("mouseleave",l)))}})).observe(document,{attributes:!0,subtree:!0,childList:!0,attributeFilter:["ripple"]})};var I=Object.freeze({__proto__:null,getZIndex:E,getDomInfo:function(e,t="body"){var n={},o=g(e);if(o)if(o.style&&"none"===o.style.display){var i=o.cloneNode(!0);i.style.display="inline-block",i.style["z-index"]=-1e3,o.parentElement.appendChild(i),n=T(i),o.parentElement.removeChild(i)}else n=T(o);return n},triggerEvent:function(e,t,n,o){var i=g(e),r=document.createEvent(t);return r.initEvent(n),o&&r.stopPropagation(),i.dispatchEvent(r)},setProperty:function(e,t,n){g(e)[t]=n},getBoundingClientRect:function(e,t="body"){var n,o;let i=g(e);var r={};if(i&&i.getBoundingClientRect)if(i.style&&"none"===i.style.display){var l=i.cloneNode(!0);l.style.display="inline-block",l.style["z-index"]=-1e3,null===(n=document.querySelector(t))||void 0===n||n.appendChild(l),r=l.getBoundingClientRect(),null===(o=document.querySelector(t))||void 0===o||o.removeChild(l)}else r=i.getBoundingClientRect();return r},addHtmlElementEventListener:function(n,o,i,r,l){let a;a="window"==n?window:"document"==n?document.documentElement:document.querySelector(n);var u=(null==l?void 0:l.key)||`${n}:${o}`;const d={};var f=e=>{var t;if((null==l?void 0:l.stopPropagation)&&e.stopPropagation(),("boolean"!=typeof e.cancelable||e.cancelable)&&(null==l?void 0:l.preventDefault)&&e.preventDefault(),(null==l?void 0:l.relatedTarget)&&(null===(t=document.querySelector(l.relatedTarget))||void 0===t?void 0:t.contains(e.relatedTarget)))return;let n={};if(s.includes(e.type))n=c(e);else for(var o in e)"string"!=typeof e[o]&&"number"!=typeof e[o]||(n[o]=e[o]);if(e.target&&e.target!==window&&e.target!==document){n.target={};const t=e.target,o=t.getAttributeNames().find((e=>e.startsWith("_bl_")));o?(n.target.elementReferenceId=o,n.target.selector=`[${o}]`):n.target.selector=m(t),n.target.class=t.getAttribute("class")}i.invokeMethodAsync("Invoke",n)};(null==l?void 0:l.debounce)&&l.debounce>0?d.listener=e(f,l.debounce):(null==l?void 0:l.throttle)&&l.throttle>0?d.listener=t(f,l.throttle,{trailing:!0}):d.listener=f,d.options=r,d.handle=i,L[u]?L[u].push(d):L[u]=[d],a&&a.addEventListener(o,d.listener,d.options)},removeHtmlElementEventListener:function(e,t,n){let o;o="window"==e?window:"document"==e?document.documentElement:document.querySelector(e);var i=L[n=n||`${e}:${t}`];i&&(i.forEach((e=>{e.handle.dispose(),null==o||o.removeEventListener(t,e.listener,e.options)})),L[n]=[])},addMouseleaveEventListener:function(e){var t=document.querySelector(e);t&&t.addEventListener()},contains:function(e,t){const n=g(e);return!(!n||!n.contains)&&n.contains(g(t))},equalsOrContains:function(e,t){const n=g(e),o=g(t);return!!n&&n.contains&&!!o&&(n==o||n.contains(o))},copy:function(e){navigator.clipboard?navigator.clipboard.writeText(e).then((function(){console.log("Async: Copying to clipboard was successful!")}),(function(e){console.error("Async: Could not copy text: ",e)})):function(e){var t=document.createElement("textarea");t.value=e,t.style.top="0",t.style.left="0",t.style.position="fixed",document.body.appendChild(t),t.focus(),t.select();try{var n=document.execCommand("copy")?"successful":"unsuccessful";console.log("Fallback: Copying text command was "+n)}catch(e){console.error("Fallback: Oops, unable to copy",e)}document.body.removeChild(t)}(e)},focus:function(e,t=!1){let n=g(e);if(!(n instanceof HTMLElement))throw new Error("Unable to focus an invalid element.");n.focus({preventScroll:t})},select:function(e){let t=g(e);if(!(t instanceof HTMLInputElement||t instanceof HTMLTextAreaElement))throw new Error("Unable to select an invalid element");t.select()},hasFocus:function(e){let t=g(e);return document.activeElement===t},blur:function(e){g(e).blur()},log:function(e){console.log(e)},scrollIntoView:function(e,t){let n=g(e);n instanceof HTMLElement&&(null===t||null==t?n.scrollIntoView():"boolean"==typeof t?n.scrollIntoView(t):n.scrollIntoView({block:null==t.block?void 0:t.block,inline:null==t.inline?void 0:t.inline,behavior:t.behavior}))},scrollIntoParentView:function(e,t=!1,n=!1,o=1,i="smooth"){const r=g(e);if(r instanceof HTMLElement){let e=r;for(;o>0;)if(e=e.parentElement,o--,!e)return;const l={behavior:i};if(t)if(n)l.left=r.offsetLeft;else{const t=r.offsetLeft-e.offsetLeft;t-e.scrollLeft<0?l.left=t:t+r.offsetWidth-e.scrollLeft>e.offsetWidth&&(l.left=t+r.offsetWidth-e.offsetWidth)}else if(n)l.top=r.offsetTop;else{const t=r.offsetTop-e.offsetTop;t-e.scrollTop<0?l.top=t:t+r.offsetHeight-e.scrollTop>e.offsetHeight&&(l.top=t+r.offsetHeight-e.offsetHeight)}(l.left||l.top)&&e.scrollTo(l)}},scrollTo:function(e,t){let n=g(e);if(n instanceof HTMLElement){const e={left:null===t.left?void 0:t.left,top:null===t.top?void 0:t.top,behavior:t.behavior};n.scrollTo(e)}},scrollToElement:function(e,t,n){const o=g(e);if(!o)return;const i=o.getBoundingClientRect().top+window.pageYOffset-t;window.scrollTo({top:i,behavior:n})},scrollToActiveElement:function(e,t=".active",n="center"){let o,i=g(e);"string"==typeof t&&(o=e.querySelector(t)),i&&o&&(i.scrollTop="center"===n?o.offsetTop-i.offsetHeight/2+o.offsetHeight/2:o.offsetTop-n)},addClsToFirstChild:function(e,t){var n=g(e);n.firstElementChild&&n.firstElementChild.classList.add(t)},removeClsFromFirstChild:function(e,t){var n=g(e);n.firstElementChild&&n.firstElementChild.classList.remove(t)},getAbsoluteTop:function e(t){var n=t.offsetTop;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},getAbsoluteLeft:function e(t){var n=t.offsetLeft;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},addElementToBody:function(e){document.body.appendChild(e)},delElementFromBody:function(e){document.body.removeChild(e)},addElementTo:function(e,t){let n=g(t);n&&e&&n.appendChild(e)},delElementFrom:function(e,t){let n=g(t);n&&e&&n.removeChild(e)},getActiveElement:C,focusDialog:function e(t,n=0){let o=document.querySelector(t);o&&!o.hasAttribute("disabled")&&setTimeout((()=>{o.focus(),"#"+C()!==t&&n<10&&e(t,n+1)}),10)},getWindow:function(){return{innerWidth:window.innerWidth,innerHeight:window.innerHeight,pageXOffset:window.pageXOffset,pageYOffset:window.pageYOffset,isTop:0==window.scrollY,isBottom:window.scrollY+window.innerHeight==document.body.clientHeight}},getWindowAndDocumentProps:x,css:function(e,t,n=null){var o=g(e);if("string"==typeof t)o.style[t]=n;else for(let e in t)t.hasOwnProperty(e)&&(o.style[e]=t[e])},addCls:function(e,t){let n=g(e);"string"==typeof t?n.classList.add(t):n.classList.add(...t)},removeCls:function(e,t){let n=g(e);"string"==typeof t?n.classList.remove(t):n.classList.remove(...t)},elementScrollIntoView:function(e){let t=g(e);t&&t.scrollIntoView({behavior:"smooth",block:"nearest",inline:"start"})},getScroll:function(){return{x:window.pageXOffset,y:window.pageYOffset}},getScrollParent:function(e,t=undefined){null!=t||(t=h?window:void 0);let n=e;for(;n&&n!==t&&S(n);){const{overflowY:e}=window.getComputedStyle(n);if(/scroll|auto|overlay/i.test(e))return n;n=n.parentNode}return t},getScrollTop:function(e){const t="scrollTop"in e?e.scrollTop:e.pageYOffset;return Math.max(t,0)},getInnerText:function(e){return g(e).innerText},getMenuOrDialogMaxZIndex:_,getMaxZIndex:function(){return[...document.all].reduce(((e,t)=>Math.max(e,+window.getComputedStyle(t).zIndex||0)),0)},getStyle:function(e,t){return(e=g(e)).currentStyle?e.currentStyle[t]:window.getComputedStyle?document.defaultView.getComputedStyle(e,null).getPropertyValue(t):void 0},getTextAreaInfo:function(e){var t={},n=g(e);return t.scrollHeight=n.scrollHeight||0,e.currentStyle?(t.lineHeight=parseFloat(e.currentStyle["line-height"]),t.paddingTop=parseFloat(e.currentStyle["padding-top"]),t.paddingBottom=parseFloat(e.currentStyle["padding-bottom"]),t.borderBottom=parseFloat(e.currentStyle["border-bottom"]),t.borderTop=parseFloat(e.currentStyle["border-top"])):window.getComputedStyle&&(t.lineHeight=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("line-height")),t.paddingTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-top")),t.paddingBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-bottom")),t.borderBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-bottom")),t.borderTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-top"))),Object.is(NaN,t.borderTop)&&(t.borderTop=1),Object.is(NaN,t.borderBottom)&&(t.borderBottom=1),t},disposeObj:function(e){},upsertThemeStyle:function(e,t){const n=document.getElementById(e);n&&document.head.removeChild(n);const o=document.createElement("style");o.id=e,o.type="text/css",o.innerHTML=t,document.head.insertAdjacentElement("beforeend",o)},getImageDimensions:function(e){return new Promise((function(t,n){var o=new Image;o.src=e,o.onload=function(){t({width:o.width,height:o.height,hasError:!1})},o.onerror=function(){t({width:0,height:0,hasError:!0})}}))},enablePreventDefaultForEvent:function(e,t,n){const o=g(e);o&&("keydown"===t?o.addEventListener(t,(e=>{Array.isArray(n)?n.includes(e.code)&&e.preventDefault():e.preventDefault()})):o.addEventListener(t,(e=>{e.preventDefault&&e.preventDefault()})))},getBoundingClientRects:function(e){for(var t=document.querySelectorAll(e),n=[],o=0;o<t.length;o++){var i=t[o],r={id:i.id,rect:i.getBoundingClientRect()};n.push(r)}return n},getSize:function(e,t){var n=g(e),o=n.style.display,i=n.style.overflow;n.style.display="",n.style.overflow="hidden";var r=n["offset"+t.charAt(0).toUpperCase()+t.slice(1)]||0;return n.style.display=o,n.style.overflow=i,r},getProp:function(e,t){var n=g(e);return n?n[t]:null},updateWindowTransition:function(e,t,n){var o=g(e),i=o.querySelector(".m-window__container");if(n){var r=g(n);i.style.height=r.clientHeight+"px"}else t?(i.classList.add("m-window__container--is-active"),i.style.height=o.clientHeight+"px"):(i.style.height="",i.classList.remove("m-window__container--is-active"))},getScrollHeightWithoutHeight:function(e){var t=g(e);if(!t)return 0;var n=t.style.height;t.style.height="0";var o=t.scrollHeight;return t.style.height=n,o},registerTextFieldOnMouseDown:function(e,t,n){if(!e||!t)return;const o=e=>{if(e.target!==g(t)&&(e.preventDefault(),e.stopPropagation()),n){const t={Detail:e.detail,ScreenX:e.screenX,ScreenY:e.screenY,ClientX:e.clientX,ClientY:e.clientY,OffsetX:e.offsetX,OffsetY:e.offsetY,PageX:e.pageX,PageY:e.pageY,Button:e.button,Buttons:e.buttons,CtrlKey:e.ctrlKey,ShiftKey:e.shiftKey,AltKey:e.altKey,MetaKey:e.metaKey,Type:e.type};n.invokeMethodAsync("Invoke",t)}};e.addEventListener("mousedown",o);const i={listener:o,handle:n},r=`registerTextFieldOnMouseDown_${p(e)}`;L[r]=[i]},unregisterTextFieldOnMouseDown:function(e){const t=`registerTextFieldOnMouseDown_${p(e)}`,n=L[t];n&&n.length&&n.forEach((t=>{t.handle.dispose(),e&&e.removeEventListener("mousedown",t.listener)}))},containsActiveElement:function(e){var t=g(e);return t&&t.contains?t.contains(document.activeElement):null},copyChild:function(e){"string"==typeof e&&(e=document.querySelector(e)),e&&(e.setAttribute("contenteditable","true"),e.focus(),document.execCommand("selectAll",!1,null),document.execCommand("copy"),document.execCommand("unselect"),e.removeAttribute("contenteditable"))},copyText:function(e){if(navigator.clipboard)navigator.clipboard.writeText(e).then((function(){console.log("Async: Copying to clipboard was successful!")}),(function(e){console.error("Async: Could not copy text: ",e)}));else{var t=document.createElement("textarea");t.value=e,t.style.top="0",t.style.left="0",t.style.position="fixed",document.body.appendChild(t),t.focus(),t.select();try{var n=document.execCommand("copy")?"successful":"unsuccessful";console.log("Fallback: Copying text command was "+n)}catch(e){console.error("Fallback: Oops, unable to copy",e)}document.body.removeChild(t)}},getMenuableDimensions:N,invokeMultipleMethod:function(e,t,n,o,i,r,l,s,a){var c={windowAndDocument:null,dimensions:null,zIndex:0};return c.windowAndDocument=x(e,t),c.dimensions=N(n,o,i,r,l,s),c.zIndex=_([r],a),c},registerOTPInputOnInputEvent:function(e,t){for(let n=0;n<e.length;n++){const o=o=>B(o,n,e,t),i=t=>M(t,n,e),r=o=>O(o,n,e,t);e[n].addEventListener("input",o),e[n].addEventListener("focus",i),e[n].addEventListener("keyup",r),e[n]._optInput={inputListener:o,focusListener:i,keyupListener:r}}},unregisterOTPInputOnInputEvent:function(e){for(let t=0;t<e.length;t++){const n=e[t];n&&n._optInput&&(n.removeEventListener("input",n._optInput.inputListener),n.removeEventListener("focus",n._optInput.focusListener),n.removeEventListener("keyup",n._optInput.keyupListener))}},getListIndexWhereAttributeExists:function(e,t,n){const o=document.querySelectorAll(e);if(!o)return-1;let i=-1;for(let e=0;e<o.length;e++)if(o[e].getAttribute(t)===n){i=e;break}return i},scrollToTile:function(e,t,n,o){var i=document.querySelectorAll(t);if(!i)return;let r=i[n];if(!r)return;const l=document.querySelector(e);if(!l)return;const s=l.scrollTop,a=l.clientHeight;s>r.offsetTop-8?l.scrollTo({top:r.offsetTop-r.clientHeight,behavior:"smooth"}):s+a<r.offsetTop+r.clientHeight+8&&l.scrollTo({top:r.offsetTop-a+2*r.clientHeight,behavior:"smooth"})},getElementTranslateY:function(e){const t=window.getComputedStyle(e),n=t.transform||t.webkitTransform,o=n.slice(7,n.length-1).split(", ")[5];return Number(o)},checkIfThresholdIsExceededWhenScrolling:function(e,t,n){if(!e||!t)return;let o;o="window"==t?window:"document"==t?document.documentElement:document.querySelector(t);const i=e.getBoundingClientRect().top;return(o===window?window.innerHeight:o.getBoundingClientRect().bottom)>=i-n},get_top_domain:Y,setCookie:function(e,t){var n=Y();n?isNaN(n[0])&&(n=`.${n}`):n="";var o=new Date;o.setTime(o.getTime()+2592e6),document.cookie=`${e}=${escape(t.toString())};path=/;expires=${o.toUTCString()};domain=${n}`},getCookie:function(e){const t=new RegExp(`(^| )${e}=([^;]*)(;|$)`),n=document.cookie.match(t);return n?unescape(n[2]):null},registerDragEvent:function(e,t){if(e){const n=p(e),o=e=>{if(t){const n=e.target.getAttribute(t);e.dataTransfer.setData(t,n),e.dataTransfer.setData("offsetX",e.offsetX.toString()),e.dataTransfer.setData("offsetY",e.offsetY.toString())}};L[`${n}:dragstart`]=[{listener:o}],e.addEventListener("dragstart",o)}},unregisterDragEvent:function(e){const t=p(e);if(t){const n=`${t}:dragstart`;L[n]&&L[n].forEach((t=>{e.removeEventListener("dragstart",t.listener)}))}},resizableDataTable:function(e){const t=e.querySelector("table"),n=t.querySelector(".m-data-table-header").getElementsByTagName("tr")[0],o=n?n.children:[];if(!o)return;t.style.overflow="hidden";const i=t.offsetHeight;for(var r=0;r<o.length;r++){const e=o[r],t=e.querySelector(".m-data-table-header__col-resize");if(!t)continue;t.style.height=i+"px";let n=e.firstElementChild.offsetWidth;n=n+32+18+1+1,e.style.minWidth||(e.minWidth=n,e.style.minWidth=n+"px"),l(t)}function l(n){let i,r,l,a,c,u;n.addEventListener("click",(e=>e.stopPropagation())),n.addEventListener("mousedown",(function(e){r=e.target.parentElement,l=r.nextElementSibling,i=e.pageX,u=t.offsetWidth;var n=function(e){if("border-box"==s(e,"box-sizing"))return 0;var t=s(e,"padding-left"),n=s(e,"padding-right");return parseInt(t)+parseInt(n)}(r);a=r.offsetWidth-n,l&&(c=l.offsetWidth-n)})),document.addEventListener("mousemove",(function(n){if(r){let o=n.pageX-i;e.classList.contains("m-data-table--rtl")&&(o=0-o);let s=a+o;r.style.width=s+"px";if(e.classList.contains("m-data-table--resizable-overflow"))return void(t.style.width=u+o+"px");if(e.classList.contains("m-data-table--resizable-independent")){let e=c-o;const t=a+c;o>0?l&&e<l.minWidth&&(e=l.minWidth,s=t-e):s<r.minWidth&&(s=r.minWidth,e=t-s),r.style.width=s+"px",l&&(l.style.width=e+"px")}}})),document.addEventListener("mouseup",(function(e){if(r)for(let e=0;e<o.length;e++){const t=o[e];t.style.width=t.offsetWidth+"px"}r=void 0,l=void 0,i=void 0,c=void 0,a=void 0,u=void 0}))}function s(e,t){return window.getComputedStyle(e,null).getPropertyValue(t)}},updateDataTableResizeHeight:function(e){const t=e.querySelector("table"),n=t.querySelector(".m-data-table-header").getElementsByTagName("tr")[0],o=n?n.children:[];if(!o)return;const i=t.offsetHeight;for(var r=0;r<o.length;r++){o[r].querySelector(".m-data-table-header__col-resize").style.height=i+"px"}}}),D={};function P(e,t){if(e.hasAttribute("data-app"))return!1;const n=t.shiftKey||t.deltaX?"x":"y",o="y"===n?t.deltaY:t.deltaX||t.deltaY;let i,r;"y"===n?(i=0===e.scrollTop,r=e.scrollTop+e.clientHeight===e.scrollHeight):(i=0===e.scrollLeft,r=e.scrollLeft+e.clientWidth===e.scrollWidth);return!(i||!(o<0))||(!(r||!(o>0))||!(!i&&!r)&&P(e.parentNode,t))}function W(e,t){return e===t||null!==e&&e!==document.body&&W(e.parentNode,t)}function X(e){if(!e||e.nodeType!==Node.ELEMENT_NODE)return!1;const t=window.getComputedStyle(e);return(["auto","scroll"].includes(t.overflowY)||"SELECT"===e.tagName)&&e.scrollHeight>e.clientHeight||["auto","scroll"].includes(t.overflowX)&&e.scrollWidth>e.clientWidth}var z=Object.freeze({__proto__:null,hideScroll:function(e,t,n,o){if(e)document.documentElement.classList.add("overflow-y-hidden");else{if(!t)return;const e=g(t),a=t=>{!function(e,t,n,o){if("keydown"===e.type){if(["INPUT","TEXTAREA","SELECT"].includes(e.target.tagName)||e.target.isContentEditable)return;const t=[f.up,f.pageup],n=[f.down,f.pagedown];if(t.includes(e.keyCode))e.deltaY=-1;else{if(!n.includes(e.keyCode))return;e.deltaY=1}}(e.target===t||"keydown"!==e.type&&e.target===document.body||function(e,t,n){const o=function(e){if(e.composedPath)return e.composedPath();const t=[];let n=e.target;for(;n;){if(t.push(n),"HTML"===n.tagName)return t.push(document),t.push(window),t;n=n.parentElement}return t}(e);if("keydown"===e.type&&o[0]===document.body){const t=window.getSelection().anchorNode;return!(n&&X(n)&&W(t,n))||!P(n,e)}for(let n=0;n<o.length;n++){const i=o[n];if(i===document)return!0;if(i===document.documentElement)return!0;if(i===t)return!0;if(X(i))return!P(i,e)}return!0}(e,n,o))&&e.preventDefault()}(t,e,n,o)};D[`window_${t}`]=a,i=window,r="wheel",l=a,s={passive:!1},i.addEventListener(r,l,!!d&&s),window.addEventListener("keydown",a)}var i,r,l,s},showScroll:function(e){document.documentElement.classList.remove("overflow-y-hidden");var t=D[`window_${e}`];t&&(window.removeEventListener("wheel",t),window.removeEventListener("keydown",t))}});window.BlazorComponent={interop:Object.assign(Object.assign({},I),z)},window.MasaBlazor={}}();
//# sourceMappingURL=blazor-component.js.map
