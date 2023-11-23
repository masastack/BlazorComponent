import { rippleHide, rippleShow } from "./ripple";

export function registerRippleObserver() {
  // 创建 MutationObserver 对象
  const observer = new MutationObserver((mutationsList, observer) => {
    for (const mutation of mutationsList) {
      // 处理新增的元素
      if (mutation.type === "childList" && mutation.addedNodes.length > 0) {
        for (const node of mutation.addedNodes) {
          if (node instanceof HTMLElement) {
            if (
              node.nodeType === Node.ELEMENT_NODE &&
              node.hasAttribute("ripple") &&
              !node._ripple
            ) {
              // 给新增具有 "ripple" 属性的元素添加事件
              node.addEventListener("click", handleClick);
              node._ripple = {}
            }
          }
        }
      }

      // 处理属性变动
      if (mutation.type === "attributes") {
        const target = mutation.target;
        if (mutation.attributeName === "ripple") {
          if (target.hasAttribute("ripple") && !target.__eventAdded) {
            // 给新增具有 "ripple" 属性的元素添加事件
            target.addEventListener("click", handleClick);
            target.__eventAdded = true;
          } else if (!target.hasAttribute("ripple") && target.__eventAdded) {
            // 移除已移除 "ripple" 属性的元素的事件
            target.removeEventListener("click", handleClick);
            target.__eventAdded = false;
          }
        }
      }

      // 处理属性值变动
      if (
        mutation.type === "attributes" &&
        mutation.attributeName === "ripple"
      ) {
        const target = mutation.target;
        if (target.__eventAdded) {
          // 处理 "ripple" 属性值的变动
          handleRippleValueChange(target);
        }
      }

      // 处理元素移除
      if (mutation.type === "childList" && mutation.removedNodes.length > 0) {
        for (const node of mutation.removedNodes) {
          if (node.nodeType === Node.ELEMENT_NODE && node.__eventAdded) {
            // 移除被移除的元素的事件
            node.removeEventListener("click", handleClick);
            node.__eventAdded = false;
          }
        }
      }
    }
  });

  // 选择文档中所有具有 "ripple" 属性的元素
  const initialElements = document.querySelectorAll("[ripple]");

  // 对现有的具有 "ripple" 属性的元素添加事件
  for (const element of initialElements) {
    element.addEventListener("click", handleClick);
    element.__eventAdded = true;
  }

  // 将 MutationObserver 绑定到文档上，以监听后续的 DOM 变动
  observer.observe(document, {
    childList: true,
    subtree: true,
    attributes: true,
    attributeFilter: ["ripple"],
    attributeOldValue: false,
  });

  // 事件处理函数
  function handleClick(event) {
    console.log("点击事件触发：", event.target);
  }

  // 处理 "ripple" 属性值变动
  function handleRippleValueChange(element) {
    console.log("ripple属性值发生变化：", element.getAttribute("ripple"));
    // 在这里处理相应的事件逻辑
    // 可以根据实际需求进行操作
  }
}

export default function registerDirective() {
  var observer = new MutationObserver(function (mutationsList) {
    for (let mutation of mutationsList) {
      if (mutation.type === "childList") {
        var target: any = mutation.target;
        // console.log('childList target', target)
        //ripple
        if (!target._bind && target.attributes && target.attributes["ripple"]) {
          target.addEventListener("mousedown", rippleShow);
          target.addEventListener("mouseup", rippleHide);
          target.addEventListener("mouseleave", rippleHide);

          target._bind = true;
        } else {
          var rippleEls = document.querySelectorAll("[ripple]");
          for (let rippleEl of rippleEls) {
            var el: any = rippleEl;
            if (!el._bind) {
              console.log("bind", el);
              el._ripple = {
                circle: true,
                enabled: true,
              };
              el.addEventListener("mousedown", rippleShow);
              el.addEventListener("mouseup", rippleHide);
              el.addEventListener("mouseleave", rippleHide);

              el._bind = true;
            }
          }
        }
      } else if (mutation.type === "attributes") {
        //ripple
        if (mutation.attributeName == "ripple") {
          var target: any = mutation.target;
          if (target.attributes["ripple"]) {
            target.addEventListener("mousedown", rippleShow);
            target.addEventListener("mouseup", rippleHide);
            target.addEventListener("mouseleave", rippleHide);
          } else {
            target.removeEventListener("mousedown", rippleShow);
            target.removeEventListener("mouseup", rippleHide);
            target.removeEventListener("mouseleave", rippleHide);
          }
        }
      }
    }
  });

  observer.observe(document, {
    attributes: true,
    subtree: true,
    childList: true,
    attributeFilter: ["ripple"],
  });
}
