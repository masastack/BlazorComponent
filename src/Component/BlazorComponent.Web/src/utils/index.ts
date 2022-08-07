export function getBlazorId(el) {
  let _bl_ = el.getAttributeNames().find(a => a.startsWith('_bl_'))
  if (_bl_) {
    _bl_ = _bl_.substring(4);
  }

  return _bl_;
}

export function getElementSelector(el) {
  if (!(el instanceof Element))
    return;
  var path = [];
  while (el.nodeType === Node.ELEMENT_NODE) {
    var selector = el.nodeName.toLowerCase();
    if (el.id) {
      selector += '#' + el.id;
      path.unshift(selector);
      break;
    } else {
      var sib = el, nth = 1;
      while (sib = sib.previousElementSibling) {
        if (sib.nodeName.toLowerCase() == selector)
          nth++;
      }
      if (nth != 1)
        selector += ":nth-of-type(" + nth + ")";
    }
    path.unshift(selector);
    el = el.parentNode;
  }
  return path.join(" > ");
}

export function getDom(element) {
  if (!element) {
    element = document.body;
  } else if (typeof element === 'string') {
    if (element === 'document') {
      return document.documentElement;
    } else if (element.indexOf('.') > 0) {
      let array = element.split('.');
      let el = document.querySelector(array[0]);
      if (!el) {
        return null;
      }

      element = el[array[1]];
    } else {
      element = document.querySelector(element);
    }
  }
  return element;
}