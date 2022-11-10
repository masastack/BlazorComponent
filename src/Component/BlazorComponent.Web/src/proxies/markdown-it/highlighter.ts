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

function getLangCodeFromExtension(extension) {
  const extensionMap = {
    cs: "csharp",
    md: "markdown",
    ts: "typescript",
    py: "python",
    razor: "cshtml",
    sh: "bash",
    yml: "yaml",
  };

  return extensionMap[extension] || extension;
}