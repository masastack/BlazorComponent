import { keyCodes } from "utils/helper";

import { removeListeners, RippleOptions, update, updateRipple } from "./ripple";

export default function registerRippleObserver() {
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
              console.log("新增", node);
              // 给新增具有 "ripple" 属性的元素添加事件
              updateRipple(node, convertRippleAttributeToOptions(node), false);
            }
          }
        }
      }

      // 处理属性变动
      if (mutation.type === "attributes") {
        const target = mutation.target as HTMLElement;
        if (target.hasAttribute("ripple") && !target._ripple) {
          if (mutation.attributeName === "ripple") {
            // 给新增具有 "ripple" 属性的元素添加事件
            console.log("属性变动", target);
            updateRipple(
              target,
              convertRippleAttributeToOptions(target),
              false
            );
          } else if (!target.hasAttribute("ripple") && target._ripple) {
            // 移除已移除 "ripple" 属性的元素的事件
            removeListeners(target);
            delete target._ripple;
          }
        }
      }

      // 处理属性值变动
      if (
        mutation.type === "attributes" &&
        mutation.attributeName === "ripple"
      ) {
        const target = mutation.target as HTMLElement;
        if (target._ripple) {
          console.log("属性值变动", target);
          update(target, convertRippleAttributeToOptions(target));
        }
      }

      // 处理元素移除
      if (mutation.type === "childList" && mutation.removedNodes.length > 0) {
        for (const node of mutation.removedNodes) {
          if (node instanceof HTMLElement) {
            console.log('删除的 node', node)
            // TODO: 子元素里可能存在 ripple的element
            if (node.nodeType === Node.ELEMENT_NODE && node._ripple) {
              // 移除被移除的元素的事件
              console.log("删除", node);
              removeListeners(node);
              delete node._ripple;
            }
          }
        }
      }
    }
  });

  // ripple="false","","true","center","circle","center:custom-css","circle:custom-css","custom-css"
  function convertRippleAttributeToOptions(
    target: HTMLElement
  ): RippleOptions | false {
    const value = target.getAttribute("ripple");
    if (value === "false") {
      return false;
    }

    const options: RippleOptions = {
      circle: true,
      center: false,
    };

    const [behavior, css] = value.split(":");

    if (behavior) {
      if (behavior === "center") {
        options.circle = false;
        options.center = true;
      } else if (behavior !== "circle") {
        options.class = behavior;
      }
    }

    if (css) {
      options.class = css;
    }

    return options;
  }

  // // 选择文档中所有具有 "ripple" 属性的元素
  // const initialElements = document.querySelectorAll("[ripple]");

  // // 对现有的具有 "ripple" 属性的元素添加事件
  // for (const element of initialElements) {
  //   updateRipple(element, formatAttributeValue(element), false);
  // }

  // 将 MutationObserver 绑定到文档上，以监听后续的 DOM 变动
  observer.observe(document, {
    childList: true,
    subtree: true,
    attributes: true,
    attributeFilter: ["ripple"],
    attributeOldValue: false,
  });
}
