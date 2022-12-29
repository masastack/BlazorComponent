declare const Prism: Prism;
declare const hljs: hljs;

function getHighlighter() {
  try {
    if (Prism) {
      return {
        getLanguage(lang: string) {
          return Prism.languages[lang];
        },
        highlight(code: string, lang: string) {
          return Prism.highlight(code, Prism.languages[lang], lang);
        },
      };
    }
  } catch (error) {}

  try {
    if (hljs) {
      return {
        getLanguage(lang: string) {
          return hljs.getLanguage(lang);
        },
        highlight(code: string, lang: string) {
          return hljs.highlight(code, { language: lang }).value;
        },
      };
    }
  } catch (error) {}

  return undefined;
}

export function highlight(code: string, lang: string) {
  const highlighter = getHighlighter();

  if (!highlighter) {
    console.warn(
      `Highlighter(Prismjs or Highlight.js) is required!`
    );
    return code;
  }

  if (!lang) {
    return code;
  }

  lang = getLangCodeFromExtension(lang.toLowerCase());

  if (highlighter.getLanguage(lang)) {
    try {
      return highlighter.highlight(code, lang);
    } catch (error) {
      console.error(
        `Syntax highlight for language ${lang} failed.`
      );
      return code;
    }
  } else {
    console.warn(
      `[markdown-it-proxy] Syntax highlight for language "${lang}" is not supported.`
    );
    return code;
  }
}

export function highlightToArrayBuffer(str: string, lang: string): ArrayBuffer {
  var htmlCode = highlight(str, lang);
  return stringToArrayBuffer(htmlCode);
}

function stringToArrayBuffer(str: string) {
  const bytes = new Array();
  let len, c;
  len = str.length;
  for (var i = 0; i < len; i++) {
    c = str.charCodeAt(i);
    if (c >= 0x010000 && c <= 0x10ffff) {
      bytes.push(((c >> 18) & 0x07) | 0xf0);
      bytes.push(((c >> 12) & 0x3f) | 0x80);
      bytes.push(((c >> 6) & 0x3f) | 0x80);
      bytes.push((c & 0x3f) | 0x80);
    } else if (c >= 0x000800 && c <= 0x00ffff) {
      bytes.push(((c >> 12) & 0x0f) | 0xe0);
      bytes.push(((c >> 6) & 0x3f) | 0x80);
      bytes.push((c & 0x3f) | 0x80);
    } else if (c >= 0x000080 && c <= 0x0007ff) {
      bytes.push(((c >> 6) & 0x1f) | 0xc0);
      bytes.push((c & 0x3f) | 0x80);
    } else {
      bytes.push(c & 0xff);
    }
  }

  var array = new Int8Array(bytes.length);

  for (var i = 0; i <= bytes.length; i++) {
    array[i] = bytes[i];
  }

  return array.buffer;
}

function getLangCodeFromExtension(extension) {
  const extensionMap = {
    cs: "csharp",
    html: "markup",
    md: "markdown",
    ts: "typescript",
    py: "python",
    razor: "cshtml",
    sh: "bash",
    yml: "yaml",
  };

  return extensionMap[extension] || extension;
}
