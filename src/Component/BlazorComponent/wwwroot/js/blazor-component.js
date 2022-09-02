function e(e,t){e.style.transform=t,e.style.webkitTransform=t}function t(e,t){e.style.opacity=t.toString()}const n={show(n,o){const i=document.createElement("span"),r=document.createElement("span");i.appendChild(r),i.className="m-ripple__container";const{radius:l,scale:s,x:a,y:c,centerX:d,centerY:u}=((e,t)=>{let n=0,o=0;const i=t.getBoundingClientRect(),r=e;n=r.clientX-i.left,o=r.clientY-i.top;let l=0;return l=Math.sqrt(t.clientWidth**2+t.clientHeight**2)/2,{radius:l,scale:.3,x:n-l+"px",y:o-l+"px",centerX:(t.clientWidth-2*l)/2+"px",centerY:(t.clientHeight-2*l)/2+"px"}})(n,o),f=2*l+"px";r.className="m-ripple__animation",r.style.width=f,r.style.height=f,o.appendChild(i);const p=window.getComputedStyle(o);p&&"static"===p.position&&(o.style.position="relative",o.dataset.previousPosition="static"),r.classList.add("m-ripple__animation--enter"),r.classList.add("m-ripple__animation--visible"),e(r,`translate(${a}, ${c}) scale3d(${s},${s},${s})`),t(r,0),r.dataset.activated=String(performance.now()),setTimeout((()=>{r.classList.remove("m-ripple__animation--enter"),r.classList.add("m-ripple__animation--in"),e(r,`translate(${d}, ${u}) scale3d(1,1,1)`),t(r,.25)}),0)},hide(e){const n=e.getElementsByClassName("m-ripple__animation");if(0===n.length)return;const o=n[n.length-1];if(o.dataset.isHiding)return;o.dataset.isHiding="true";const i=performance.now()-Number(o.dataset.activated),r=Math.max(250-i,0);setTimeout((()=>{o.classList.remove("m-ripple__animation--in"),o.classList.add("m-ripple__animation--out"),t(o,0),setTimeout((()=>{1===e.getElementsByClassName("m-ripple__animation").length&&e.dataset.previousPosition&&(e.style.position=e.dataset.previousPosition,delete e.dataset.previousPosition),o.parentNode&&e.removeChild(o.parentNode)}),300)}),r)}};function o(e){n.show(e,e.currentTarget)}function i(e){n.hide(e.currentTarget)}let r=!1;try{if("undefined"!=typeof window){const e=Object.defineProperty({},"passive",{get:()=>{r=!0}});window.addEventListener("testListener",e,e),window.removeEventListener("testListener",e,e)}}catch(e){console.warn(e)}const l=Object.freeze({enter:13,tab:9,delete:46,esc:27,space:32,up:38,down:40,left:37,right:39,end:35,home:36,del:46,backspace:8,insert:45,pageup:33,pagedown:34,shift:16});function s(e){if(e instanceof Element){for(var t=[];e.nodeType===Node.ELEMENT_NODE;){var n=e.nodeName.toLowerCase();if(e.id){n+="#"+e.id,t.unshift(n);break}for(var o=e,i=1;o=o.previousElementSibling;)o.nodeName.toLowerCase()==n&&i++;1!=i&&(n+=":nth-of-type("+i+")"),t.unshift(n),e=e.parentNode}return t.join(" > ")}}function a(e){const t=[];for(let n=0;n<e.length;n++){const o=e[n];t.push({identifier:o.identifier,clientX:o.clientX,clientY:o.clientY,screenX:o.screenX,screenY:o.screenY,pageX:o.pageX,pageY:o.pageY})}return t}function c(e,t){Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t,createEventArgs:e=>d("mouse",e)})}function d(e,t){let n={target:{}};var o;if("mouse"===e?n=Object.assign(Object.assign({},n),{detail:(o=t).detail,screenX:o.screenX,screenY:o.screenY,clientX:o.clientX,clientY:o.clientY,offsetX:o.offsetX,offsetY:o.offsetY,pageX:o.pageX,pageY:o.pageY,button:o.button,buttons:o.buttons,ctrlKey:o.ctrlKey,shiftKey:o.shiftKey,altKey:o.altKey,metaKey:o.metaKey,type:o.type}):"touch"===e&&(n=Object.assign(Object.assign({},n),function(e){return{detail:e.detail,touches:a(e.touches),targetTouches:a(e.targetTouches),changedTouches:a(e.changedTouches),ctrlKey:e.ctrlKey,shiftKey:e.shiftKey,altKey:e.altKey,metaKey:e.metaKey,type:e.type}}(t))),t.target){const e=t.target,o=e.getAttributeNames().find((e=>e.startsWith("_bl_")));o?(n.target.elementReferenceId=o,n.target.selector=`[${o}]`):n.target.selector=s(e),n.target.class=e.getAttribute("class")}return n}function u(){var e,t;c("exmousedown","mousedown"),c("exmouseup","mouseup"),c("exclick","click"),c("exmouseleave","mouseleave"),c("exmouseenter","mouseenter"),c("exmousemove","mousemove"),e="extouchstart",t="touchstart",Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t,createEventArgs:e=>d("touch",e)}),function(e,t){Blazor&&Blazor.registerCustomEventType(e,{browserEventName:t})}("transitionend","transitionend")}function f(e){if(!e||e.nodeType!==Node.ELEMENT_NODE)return 0;const t=+window.getComputedStyle(e).getPropertyValue("z-index");return t||f(e.parentNode)}function p(e,t,n){t&&"height"===e.propertyName&&(n.style.height="auto")}function m(e,t){let n=0;requestAnimationFrame((()=>{n=e.scrollHeight})),e.style.height="0px",setTimeout((()=>t&&(e.style.height=n+"px"||"auto")),450)}function v(e){e.style.height=e.clientHeight+"px",setTimeout((()=>{e.style.height="0px"}),10)}function g(e){if(e){if("string"==typeof e){if("document"===e)return document.documentElement;if(e.indexOf(".")>0){let t=e.split("."),n=document.querySelector(t[0]);if(!n)return null;e=n[t[1]]}else e=document.querySelector(e)}}else e=document.body;return e}function h(e,t="body"){var n={},o=g(e);if(o)if(o.style&&"none"===o.style.display){var i=o.cloneNode(!0);i.style.display="inline-block",i.style["z-index"]=-1e3,o.parentElement.appendChild(i),n=y(i),o.parentElement.removeChild(i)}else n=y(o);return n}function y(e){var t={};t.offsetTop=e.offsetTop||0,t.offsetLeft=e.offsetLeft||0,t.scrollHeight=e.scrollHeight||0,t.scrollWidth=e.scrollWidth||0,t.scrollLeft=e.scrollLeft||0,t.scrollTop=e.scrollTop||0,t.clientTop=e.clientTop||0,t.clientLeft=e.clientLeft||0,t.clientHeight=e.clientHeight||0,t.clientWidth=e.clientWidth||0;var n=function(e){var t=new Object;if(t.x=0,t.y=0,null!==e&&e.getBoundingClientRect){var n=document.documentElement,o=e.getBoundingClientRect(),i=n.scrollLeft,r=n.scrollTop;t.offsetWidth=o.width,t.offsetHeight=o.height,t.relativeTop=o.top,t.relativeBottom=o.bottom,t.relativeLeft=o.left,t.relativeRight=o.right,t.absoluteLeft=o.left+i,t.absoluteTop=o.top+r}return t}(e);return t.offsetWidth=Math.round(n.offsetWidth)||0,t.offsetHeight=Math.round(n.offsetHeight)||0,t.relativeTop=Math.round(n.relativeTop)||0,t.relativeBottom=Math.round(n.relativeBottom)||0,t.relativeLeft=Math.round(n.relativeLeft)||0,t.relativeRight=Math.round(n.relativeRight)||0,t.absoluteLeft=Math.round(n.absoluteLeft)||0,t.absoluteTop=Math.round(n.absoluteTop)||0,t}function w(){var e=this.attributes["data-fileid"].nodeValue;document.getElementById(e).click()}function b(e){var t=null;return null!=window.URL?t=window.URL.createObjectURL(e):null!=window.webkitURL&&(t=window.webkitURL.createObjectURL(e)),t}function E(e,t="body"){var n,o;let i=g(e);var r={};if(i&&i.getBoundingClientRect)if(i.style&&"none"===i.style.display){var l=i.cloneNode(!0);l.style.display="inline-block",l.style["z-index"]=-1e3,null===(n=document.querySelector(t))||void 0===n||n.appendChild(l),r=l.getBoundingClientRect(),null===(o=document.querySelector(t))||void 0===o||o.removeChild(l)}else r=i.getBoundingClientRect();return r}var L={};var T={};function C(e){var t=setInterval((function(){var n=document.documentElement.scrollTop||document.body.scrollTop,o=e>n?e-n:n-e,i=Math.ceil(o/10);n==e?clearInterval(t):window.scrollTo(0,e>n?n+i:n-i)}),10)}function k(){return document.activeElement.getAttribute("id")||""}function x(e=[],t=[]){const n={};return e&&e.forEach((e=>n[e]=window[e])),t&&t.forEach((e=>n[e]=document.documentElement[e])),n}function S(e=[],t){const n=[f(g(t))],o=[...document.getElementsByClassName("m-menu__content--active"),...document.getElementsByClassName("m-dialog__content--active")];for(let t=0;t<o.length;t++)e.includes(o[t])||n.push(f(o[t]));return Math.max(...n)}function A(e){var t={},n=g(e);return t.scrollHeight=n.scrollHeight||0,e.currentStyle?(t.lineHeight=parseFloat(e.currentStyle["line-height"]),t.paddingTop=parseFloat(e.currentStyle["padding-top"]),t.paddingBottom=parseFloat(e.currentStyle["padding-bottom"]),t.borderBottom=parseFloat(e.currentStyle["border-bottom"]),t.borderTop=parseFloat(e.currentStyle["border-top"])):window.getComputedStyle&&(t.lineHeight=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("line-height")),t.paddingTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-top")),t.paddingBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("padding-bottom")),t.borderBottom=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-bottom")),t.borderTop=parseFloat(document.defaultView.getComputedStyle(e,null).getPropertyValue("border-top"))),Object.is(NaN,t.borderTop)&&(t.borderTop=1),Object.is(NaN,t.borderBottom)&&(t.borderBottom=1),t}const M={};function O(e){e.removeEventListener("input",M[e.id+"input"]),_[e.id]=null,M[e.id+"input"]=null}function H(e,t,n){var o=A(e),i=o.lineHeight,r=o.paddingTop+o.paddingBottom+o.borderTop+o.borderBottom,l=parseFloat(e.style.height);e.style.height="auto";var s=Math.trunc(e.scrollHeight/i),a=0;if((s=Math.max(t,s))>n?(a=(s=n)*i+r,e.style.height=a+"px",e.style.overflowY="visible"):(a=s*i+r,e.style.height=a+"px",e.style.overflowY="hidden"),l!==a){_[e.id].invokeMethodAsync("ChangeSizeAsyncJs",parseFloat(e.scrollWidth),a)}}const _={};function N(e,t,n,o,i,r){if(!i){var l=document.querySelector(r);o.nodeType&&l.appendChild(o)}var s={activator:{},content:null,relativeYOffset:0,offsetParentLeft:0};if(e){var a=document.querySelector(t);s.activator=B(a,n),s.activator.offsetLeft=a.offsetLeft,s.activator.offsetTop=null!==n?a.offsetTop:0}return function(e,t){if(!t||!t.style||"none"!==t.style.display)return void e();t.style.display="inline-block",e(),t.style.display="none"}((()=>{if(o){if(o.offsetParent){const t=I(o.offsetParent);s.relativeYOffset=window.pageYOffset+t.top,e?(s.activator.top-=s.relativeYOffset,s.activator.left-=window.pageXOffset+t.left):s.offsetParentLeft=t.left}s.content=B(o,n)}}),o),s}function B(e,t){if(!e)return null;const n=I(e);if(null!==t){const t=window.getComputedStyle(e);n.left=parseInt(t.marginLeft),n.top=parseInt(t.marginTop)}return n}function I(e){if(!e||!e.nodeType)return null;const t=e.getBoundingClientRect();return{top:Math.round(t.top),left:Math.round(t.left),bottom:Math.round(t.bottom),right:Math.round(t.right),width:Math.round(t.width),height:Math.round(t.height)}}function Y(e,t,n,o){e.preventDefault();const i=e.key;if("ArrowLeft"===i||"Backspace"===i){if("Backspace"===i){const e={type:i,index:t,value:""};o&&o.invokeMethodAsync("Invoke",JSON.stringify(e))}P(t-1,n)}else"ArrowRight"===i&&P(t+1,n)}function P(e,t){if(e<0)P(0,t);else if(e>=t.length)P(t.length-1,t);else if(document.activeElement!==t[e]){g(t[e]).focus()}}function z(e,t,n){const o=g(n[t]);o&&document.activeElement===o&&o.select()}function F(e,t,n,o){const i=e.target.value;if(i&&""!==i&&(P(t+1,n),o)){const e={type:"Input",index:t,value:i};o.invokeMethodAsync("Invoke",JSON.stringify(e))}}window.onload=function(){var e;u(),e="pastewithdata",Blazor&&Blazor.registerCustomEventType(e,{browserEventName:"paste",createEventArgs:e=>({type:e.type,pastedData:e.clipboardData.getData("text")})}),new MutationObserver((function(e){for(let l of e)if("childList"===l.type){if(!(r=l.target)._bind&&r.attributes.ripple)r.addEventListener("mousedown",o),r.addEventListener("mouseup",i),r.addEventListener("mouseleave",i),r._bind=!0;else if("BODY"==r.nodeName){var t=document.querySelectorAll("[ripple]");for(let e of t){var n=e;n._bind||(n.addEventListener("mousedown",o),n.addEventListener("mouseup",i),n.addEventListener("mouseleave",i),n._bind=!0)}}}else if("attributes"===l.type){var r;"ripple"==l.attributeName&&((r=l.target).attributes.ripple?(r.addEventListener("mousedown",o),r.addEventListener("mouseup",i),r.addEventListener("mouseleave",i)):(r.removeEventListener("mousedown",o),r.removeEventListener("mouseup",i),r.removeEventListener("mouseleave",i)))}})).observe(document,{attributes:!0,subtree:!0,childList:!0,attributeFilter:["ripple"]})};var q=Object.freeze({__proto__:null,updateCanvas:function(e,t){const n=e,o=n.getContext("2d");if(!o)return;const i=o.createLinearGradient(0,0,n.width,0);i.addColorStop(0,"hsla(0, 0%, 100%, 1)"),i.addColorStop(1,`hsla(${t}, 100%, 50%, 1)`),o.fillStyle=i,o.fillRect(0,0,n.width,n.height);const r=o.createLinearGradient(0,0,0,n.height);r.addColorStop(0,"hsla(0, 0%, 100%, 0)"),r.addColorStop(1,"hsla(0, 0%, 0%, 1)"),o.fillStyle=r,o.fillRect(0,0,n.width,n.height)},getZIndex:f,addStepperEventListener:function(e,t){(null==e?void 0:e.addEventListener)&&e.addEventListener("transitionend",(n=>p(n,t,e)),!1)},removeStepperEventListener:function(e,t){(null==e?void 0:e.removeEventListener)&&e.removeEventListener("transitionend",(n=>p(n,t,e)),!1)},initStepperWrapper:function(e){e.classList.contains("active")||(e.style.display="none"),new MutationObserver((function(e){for(let n of e)if("attributes"===n.type&&"class"==n.attributeName){var t=n.target;t.classList.contains("active")?(t.style.display="",m(t,!0)):(v(t),setTimeout((()=>{t.style.display="none"}),300))}})).observe(e,{attributes:!0,attributeFilter:["class"]})},getDom:g,getDomInfo:h,addFileClickEventListener:function(e){e.addEventListener&&e.addEventListener("click",w)},removeFileClickEventListener:function(e){e.removeEventListener("click",w)},fileClickEvent:w,clearFile:function(e){e.setAttribute("type","input"),e.value="",e.setAttribute("type","file")},getFileInfo:function(e){if(e.files&&e.files.length>0){for(var t=[],n=0;n<e.files.length;n++){var o=e.files[n],i=b(o);t.push({fileName:o.name,size:o.size,objectURL:i,type:o.type})}return t}},getObjectURL:b,uploadFile:function(e,t,n,o,i,r,l,s,a,c,d){let u=new FormData;var f=e.files[t],p=f.size;if(u.append(l,f),null!=n)for(var m in n)u.append(m,n[m]);const v=new XMLHttpRequest;if(v.onreadystatechange=function(){if(4===v.readyState){if(200!=v.status)return void s.invokeMethodAsync(d,i,`{"status": ${v.status}}`);s.invokeMethodAsync(c,i,v.responseText)}},v.upload.onprogress=function(e){var t=Math.floor(e.loaded/p*100);s.invokeMethodAsync(a,i,t)},v.onerror=function(e){s.invokeMethodAsync(d,i,"error")},v.open("post",r,!0),null!=o)for(var g in o)v.setRequestHeader(g,o[g]);v.send(u)},triggerEvent:function(e,t,n,o){var i=g(e),r=document.createEvent(t);return r.initEvent(n),o&&r.stopPropagation(),i.dispatchEvent(r)},setProperty:function(e,t,n){g(e)[t]=n},getBoundingClientRect:E,getFirstChildBoundingClientRect:function(e,t="body"){let n=g(e);if(n.firstElementChild){if("none"===n.firstElementChild.style.display){var o=n.firstElementChild.cloneNode(!0);o.style.display="inline-block",o.style["z-index"]=-1e3,document.querySelector(t).appendChild(o);var i=E(o);return document.querySelector(t).removeChild(o),i}return E(n.firstElementChild)}return null},addHtmlElementEventListener:function(e,t,n,o,i){let r;r="window"==e?window:"document"==e?document.documentElement:document.querySelector(e);var l=(null==i?void 0:i.key)||`${e}:${t}`,s={},a=e=>{var t;if((null==i?void 0:i.stopPropagation)&&e.stopPropagation(),(null==i?void 0:i.preventDefault)&&e.preventDefault(),(null==i?void 0:i.relatedTarget)&&(null===(t=document.querySelector(i.relatedTarget))||void 0===t?void 0:t.contains(e.relatedTarget)))return;const o={};for(var r in e)if("string"==typeof e[r]||"number"==typeof e[r])o[r]=e[r];else if("target"==r&&e.target.attributes){var l={attributes:{}};for(let t=0;t<e.target.attributes.length;t++){const n=e.target.attributes[t];l.attributes[n.name]=n.value}o[r]=l}else if("touches"==r||"targetTouches"==r||"changedTouches"==r){var s=[];e[r].forEach((e=>{var t={};for(var n in e)"string"!=typeof e[n]&&"number"!=typeof e[n]||(t[n]=e[n]);s.push(t)})),o[r]=s}n.invokeMethodAsync("Invoke",o)};if((null==i?void 0:i.debounce)&&i.debounce>0){let e;s.listener=function(t){clearTimeout(e),e=setTimeout((()=>a(t)),i.debounce)}}else if((null==i?void 0:i.throttle)&&i.throttle>0){let e;s.listener=function(t){var n;e||(a(t),e=!0,setTimeout((()=>{e=!1}),null!==(n=null==i?void 0:i.throttle)&&void 0!==n?n:0))}}else s.listener=a;s.options=o,L[l]?L[l].push(s):L[l]=[s],r&&r.addEventListener(t,s.listener,o)},removeHtmlElementEventListener:function(e,t,n){let o;o="window"==e?window:"document"==e?document.documentElement:document.querySelector(e);var i=L[n=n||`${e}:${t}`];i&&(i.forEach((e=>{null==o||o.removeEventListener(t,e.listener,e.options)})),L[n]=[])},addOutsideClickEventListener:function(e,t,n){if(t){t=t.filter((e=>!!e));var o=function(o){if(!t.some((e=>{var t;return null===(t=g(e))||void 0===t?void 0:t.contains(o.target)}))){var i=s(o.target);n?n.some((e=>{var t;return null===(t=g(e))||void 0===t?void 0:t.contains(o.target)}))&&e.invokeMethodAsync("Invoke",{pointerSelector:i}):e.invokeMethodAsync("Invoke",{pointerSelector:i})}};document.addEventListener("click",o,!0);var i=`(${t.join(",")})document:click`;T[i]=o}},removeOutsideClickEventListener:function(e){if(e){var t=`(${(e=e.filter((e=>!!e))).join(",")})document:click`;T[t]&&(document.removeEventListener("click",T[t],!0),T[t]=void 0)}},addMouseleaveEventListener:function(e){var t=document.querySelector(e);t&&t.addEventListener()},contains:function(e,t){const n=g(e);return!(!n||!n.contains)&&n.contains(g(t))},equalsOrContains:function(e,t){const n=g(e),o=g(t);return!!n&&n.contains&&!!o&&(n==o||n.contains(o))},matchMedia:function(e){return window.matchMedia(e).matches},copy:function(e){navigator.clipboard?navigator.clipboard.writeText(e).then((function(){console.log("Async: Copying to clipboard was successful!")}),(function(e){console.error("Async: Could not copy text: ",e)})):function(e){var t=document.createElement("textarea");t.value=e,t.style.top="0",t.style.left="0",t.style.position="fixed",document.body.appendChild(t),t.focus(),t.select();try{var n=document.execCommand("copy")?"successful":"unsuccessful";console.log("Fallback: Copying text command was "+n)}catch(e){console.error("Fallback: Oops, unable to copy",e)}document.body.removeChild(t)}(e)},focus:function(e,t=!1){let n=g(e);if(!(n instanceof HTMLElement))throw new Error("Unable to focus an invalid element.");n.focus({preventScroll:t})},select:function(e){let t=g(e);if(!(t instanceof HTMLInputElement||t instanceof HTMLTextAreaElement))throw new Error("Unable to select an invalid element");t.select()},hasFocus:function(e){let t=g(e);return document.activeElement===t},blur:function(e){g(e).blur()},log:function(e){console.log(e)},backTop:function(e){let t=g(e);C(t?t.scrollTop:0)},scrollTo:function(e){let t=g(e);t instanceof HTMLElement&&t.scrollIntoView({behavior:"smooth",block:"start",inline:"nearest"})},scrollToActiveElement:function(e,t){let n=g(e);(t=n.querySelector(".active"))&&(n.scrollTop=t.offsetTop-n.offsetHeight/2+t.offsetHeight/2)},scrollToPosition:function(e,t){g(e).scrollTo({top:t,behavior:"smooth"})},getFirstChildDomInfo:function(e,t="body"){var n=g(e);return n.firstElementChild?h(n.firstElementChild,t):h(n,t)},addClsToFirstChild:function(e,t){var n=g(e);n.firstElementChild&&n.firstElementChild.classList.add(t)},removeClsFromFirstChild:function(e,t){var n=g(e);n.firstElementChild&&n.firstElementChild.classList.remove(t)},getAbsoluteTop:function e(t){var n=t.offsetTop;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},getAbsoluteLeft:function e(t){var n=t.offsetLeft;return null!=t.offsetParent&&(n+=e(t.offsetParent)),n},addElementToBody:function(e){document.body.appendChild(e)},delElementFromBody:function(e){document.body.removeChild(e)},addElementTo:function(e,t){let n=g(t);n&&e&&n.appendChild(e)},delElementFrom:function(e,t){let n=g(t);n&&e&&n.removeChild(e)},getActiveElement:k,focusDialog:function e(t,n=0){let o=document.querySelector(t);o&&!o.hasAttribute("disabled")&&setTimeout((()=>{o.focus(),"#"+k()!==t&&n<10&&e(t,n+1)}),10)},getWindow:function(){return{innerWidth:window.innerWidth,innerHeight:window.innerHeight,pageXOffset:window.pageXOffset,pageYOffset:window.pageYOffset,isTop:0==window.scrollY,isBottom:window.scrollY+window.innerHeight==document.body.clientHeight}},getWindowAndDocumentProps:x,css:function(e,t,n=null){var o=g(e);if("string"==typeof t)o.style[t]=n;else for(let e in t)t.hasOwnProperty(e)&&(o.style[e]=t[e])},addCls:function(e,t){let n=g(e);"string"==typeof t?n.classList.add(t):n.classList.add(...t)},removeCls:function(e,t){let n=g(e);"string"==typeof t?n.classList.remove(t):n.classList.remove(...t)},elementScrollIntoView:function(e){let t=g(e);t&&t.scrollIntoView({behavior:"smooth",block:"nearest",inline:"start"})},createIconFromfontCN:function(e){if(document.querySelector(`[data-namespace="${e}"]`))return;const t=document.createElement("script");t.setAttribute("src",e),t.setAttribute("data-namespace",e),document.body.appendChild(t)},getScroll:function(){return{x:window.pageXOffset,y:window.pageYOffset}},getInnerText:function(e){return g(e).innerText},getMenuOrDialogMaxZIndex:S,getMaxZIndex:function(){return[...document.all].reduce(((e,t)=>Math.max(e,+window.getComputedStyle(t).zIndex||0)),0)},getStyle:function(e,t){return(e=g(e)).currentStyle?e.currentStyle[t]:window.getComputedStyle?document.defaultView.getComputedStyle(e,null).getPropertyValue(t):void 0},getTextAreaInfo:A,registerResizeTextArea:function(e,t,n,o){if(o)return _[e.id]=o,M[e.id+"input"]=function(){H(e,t,n)},e.addEventListener("input",M[e.id+"input"]),A(e);O(e)},disposeResizeTextArea:O,resizeTextArea:H,disposeObj:function(e){delete _[e]},bindTableHeaderAndBodyScroll:function(e,t){e.bindScrollLeftToHeader=()=>{t.scrollLeft=e.scrollLeft},e.addEventListener("scroll",e.bindScrollLeftToHeader)},unbindTableHeaderAndBodyScroll:function(e){e&&e.removeEventListener("scroll",e.bindScrollLeftToHeader)},addPreventKeys:function(e,t){if(e){let n=g(e);t=t.map((function(e){return e.toUpperCase()})),M[e.id+"keydown"]=e=>function(e,t){if(-1!==t.indexOf(e.key.toUpperCase()))return e.preventDefault(),!1}(e,t),n.addEventListener("keydown",M[e.id+"keydown"],!1)}},removePreventKeys:function(e){if(e){g(e).removeEventListener("keydown",M[e.id+"keydown"]),M[e.id+"keydown"]=null}},addPreventEnterOnOverlayVisible:function(e,t){if(e&&t){let n=g(e);M[e.id+"keydown:Enter"]=e=>function(e,t,n){if(e.key.toUpperCase()===t.toUpperCase()&&n())return e.preventDefault(),!1}(e,"enter",(()=>null!==t.offsetParent)),n.addEventListener("keydown",M[e.id+"keydown:Enter"],!1)}},removePreventEnterOnOverlayVisible:function(e){if(e){g(e).removeEventListener("keydown",M[e.id+"keydown:Enter"]),M[e.id+"keydown:Enter"]=null}},insertAdjacentHTML:function(e,t){document.head.insertAdjacentHTML(e,t)},getImageDimensions:function(e){return new Promise((function(t,n){var o=new Image;o.src=e,o.onload=function(){t({width:o.width,height:o.height,hasError:!1})},o.onerror=function(){t({width:0,height:0,hasError:!0})}}))},enablePreventDefaultForEvent:function(e,t,n){const o=g(e);o&&("keydown"===t?o.addEventListener(t,(e=>{Array.isArray(n)?n.includes(e.code)&&e.preventDefault():e.preventDefault()})):o.addEventListener(t,(e=>{e.preventDefault&&e.preventDefault()})))},resizeObserver:function(e,t){var n=document.querySelector(e);if(!n)return;new ResizeObserver((e=>{const n=[];for(var o of e){const e=o.contentRect;n.push(e)}t.invokeMethodAsync("Invoke",n)})).observe(n)},intersectionObserver:function(e,t){var n=document.querySelector(e);if(!n)return;new IntersectionObserver(((e=[],n)=>{e.some((e=>e.isIntersecting))&&t.forEach((e=>{e.invokeMethodAsync("Invoke")}))})).observe(n)},getBoundingClientRects:function(e){for(var t=document.querySelectorAll(e),n=[],o=0;o<t.length;o++){var i=t[o],r={id:i.id,rect:i.getBoundingClientRect()};n.push(r)}return n},getSize:function(e,t){var n=g(e),o=n.style.display,i=n.style.overflow;n.style.display="",n.style.overflow="hidden";var r=n["offset"+t.charAt(0).toUpperCase()+t.slice(1)]||0;return n.style.display=o,n.style.overflow=i,r},getProp:function(e,t){var n=g(e);return n?n[t]:null},updateWindowTransition:function(e,t,n){var o=g(e),i=o.querySelector(".m-window__container");if(n){var r=g(n);i.style.height=r.clientHeight+"px"}else t?(i.classList.add("m-window__container--is-active"),i.style.height=o.clientHeight+"px"):(i.style.height="",i.classList.remove("m-window__container--is-active"))},getScrollHeightWithoutHeight:function(e){var t=g(e);if(!t)return 0;var n=t.style.height;t.style.height="0";var o=t.scrollHeight;return t.style.height=n,o},registerTextFieldOnMouseDown:function(e,t,n){console.log("registerTextFieldOnMouseDown element",e,t),e&&t&&e.addEventListener("mousedown",(e=>{if(e.target!==g(t)&&(e.preventDefault(),e.stopPropagation()),n){const t={Detail:e.detail,ScreenX:e.screenX,ScreenY:e.screenY,ClientX:e.clientX,ClientY:e.clientY,OffsetX:e.offsetX,OffsetY:e.offsetY,PageX:e.pageX,PageY:e.pageY,Button:e.button,Buttons:e.buttons,CtrlKey:e.ctrlKey,ShiftKey:e.shiftKey,AltKey:e.altKey,MetaKey:e.metaKey,Type:e.type};n.invokeMethodAsync("Invoke",t)}}))},isMobile:function(){return/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)||/1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0,4))},containsActiveElement:function(e){var t=g(e);return t&&t.contains?t.contains(document.activeElement):null},copyChild:function(e){"string"==typeof e&&(e=document.querySelector(e)),e&&(e.setAttribute("contenteditable","true"),e.focus(),document.execCommand("selectAll",!1,null),document.execCommand("copy"),document.execCommand("unselect"),e.removeAttribute("contenteditable"))},copyText:function(e){if(navigator.clipboard)navigator.clipboard.writeText(e).then((function(){console.log("Async: Copying to clipboard was successful!")}),(function(e){console.error("Async: Could not copy text: ",e)}));else{var t=document.createElement("textarea");t.value=e,t.style.top="0",t.style.left="0",t.style.position="fixed",document.body.appendChild(t),t.focus(),t.select();try{var n=document.execCommand("copy")?"successful":"unsuccessful";console.log("Fallback: Copying text command was "+n)}catch(e){console.error("Fallback: Oops, unable to copy",e)}document.body.removeChild(t)}},getMenuableDimensions:N,invokeMultipleMethod:function(e,t,n,o,i,r,l,s,a){var c={windowAndDocument:null,dimensions:null,zIndex:0};return c.windowAndDocument=x(e,t),c.dimensions=N(n,o,i,r,l,s),c.zIndex=S([r],a),c},registerOTPInputOnInputEvent:function(e,t){for(let n=0;n<e.length;n++)e[n].addEventListener("input",(o=>F(o,n,e,t))),e[n].addEventListener("focus",(t=>z(t,n,e))),e[n].addEventListener("keyup",(o=>Y(o,n,e,t)))},otpInputKeyupEvent:Y,otpInputFocus:P,otpInputFocusEvent:z,otpInputOnInputEvent:F,getListIndexWhereAttributeExists:function(e,t,n){const o=document.querySelectorAll(e);if(!o)return-1;let i=-1;for(let e=0;e<o.length;e++)if(o[e].getAttribute(t)===n){i=e;break}return i},scrollToTile:function(e,t,n,o){var i=document.querySelectorAll(t);if(!i)return;let r=i[n];if(!r)return;const l=document.querySelector(e);if(!l)return;const s=l.scrollTop,a=l.clientHeight;s>r.offsetTop-8?l.scrollTo({top:r.offsetTop-r.clientHeight,behavior:"smooth"}):s+a<r.offsetTop+r.clientHeight+8&&l.scrollTo({top:r.offsetTop-a+2*r.clientHeight,behavior:"smooth"})},getElementTranslateY:function(e){const t=window.getComputedStyle(e),n=t.transform||t.webkitTransform,o=n.slice(7,n.length-1).split(", ")[5];return Number(o)},checkIfThresholdIsExceededWhenScrolling:function(e,t,n){if(!e||!t)return;const o=e.getBoundingClientRect().top;return(t===window?window.innerHeight:t.getBoundingClientRect().bottom)>=o-n}}),R={};function X(e,t){if(e.hasAttribute("data-app"))return!1;const n=t.shiftKey||t.deltaX?"x":"y",o="y"===n?t.deltaY:t.deltaX||t.deltaY;let i,r;"y"===n?(i=0===e.scrollTop,r=e.scrollTop+e.clientHeight===e.scrollHeight):(i=0===e.scrollLeft,r=e.scrollLeft+e.clientWidth===e.scrollWidth);return!(i||!(o<0))||(!(r||!(o>0))||!(!i&&!r)&&X(e.parentNode,t))}function D(e,t){return e===t||null!==e&&e!==document.body&&D(e.parentNode,t)}function j(e){if(!e||e.nodeType!==Node.ELEMENT_NODE)return!1;const t=window.getComputedStyle(e);return(["auto","scroll"].includes(t.overflowY)||"SELECT"===e.tagName)&&e.scrollHeight>e.clientHeight||["auto","scroll"].includes(t.overflowX)&&e.scrollWidth>e.clientWidth}var K=Object.freeze({__proto__:null,hideScroll:function(e,t,n,o){if(e)document.documentElement.classList.add("overflow-y-hidden");else{if(!t)return;const e=function(e){if(e){if("string"==typeof e){if("document"===e)return document.documentElement;if(e.indexOf(".")>0){let t=e.split("."),n=document.querySelector(t[0]);if(!n)return null;e=n[t[1]]}else e=document.querySelector(e)}}else e=document.body;return e}(t),d=t=>{!function(e,t,n,o){if("keydown"===e.type){if(["INPUT","TEXTAREA","SELECT"].includes(e.target.tagName)||e.target.isContentEditable)return;const t=[l.up,l.pageup],n=[l.down,l.pagedown];if(t.includes(e.keyCode))e.deltaY=-1;else{if(!n.includes(e.keyCode))return;e.deltaY=1}}(e.target===t||"keydown"!==e.type&&e.target===document.body||function(e,t,n){const o=function(e){if(e.composedPath)return e.composedPath();const t=[];let n=e.target;for(;n;){if(t.push(n),"HTML"===n.tagName)return t.push(document),t.push(window),t;n=n.parentElement}return t}(e);if("keydown"===e.type&&o[0]===document.body){const t=window.getSelection().anchorNode;return!(n&&j(n)&&D(t,n))||!X(n,e)}for(let n=0;n<o.length;n++){const i=o[n];if(i===document)return!0;if(i===document.documentElement)return!0;if(i===t)return!0;if(j(i))return!X(i,e)}return!0}(e,n,o))&&e.preventDefault()}(t,e,n,o)};R[`window_${t}`]=d,i=window,s="wheel",a=d,c={passive:!1},i.addEventListener(s,a,!!r&&c),window.addEventListener("keydown",d)}var i,s,a,c},showScroll:function(e){document.documentElement.classList.remove("overflow-y-hidden");var t=R[`window_${e}`];t&&(window.removeEventListener("wheel",t),window.removeEventListener("keydown",t))}});window.BlazorComponent={interop:Object.assign(Object.assign({},q),K)};
//# sourceMappingURL=blazor-component.js.map
