import MarkdownIt from "markdown-it";
import markdownItClass from "markdown-it-class";
import markdownItHeaderSections from "markdown-it-header-sections";

declare const Prism: Prism;
declare const hljs: hljs;

let md: MarkdownIt = undefined;

const mdDict = {};

export function create(
  options: MarkdownIt.Options = {},
  tagClassMap: { [prop: string]: string[] } = {},
  enableHeaderSections: boolean = false,
  key: string = "default"
) {
  key ??= "default";

  options = { ...options, highlight };

  let md = mdDict[key];
  md = new MarkdownIt(options).use(markdownItClass, tagClassMap);

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  mdDict[key] = md;
}

export function render(src: string, key: string = "default") {
  key ??= "default";
  return mdDict[key].render(src);
}

function getHighlighter() {
  try {
    if (Prism) {
      return {
        getLanguage(lang: string) {
          return Prism.languages[lang];
        },
        highlight(str: string, lang: string) {
          return Prism.highlight(str, Prism.languages[lang], lang);
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
        highlight(str: string, lang: string) {
          return hljs.highlight(str, { language: lang }).value;
        },
      };
    }
  } catch (error) {}

  return undefined;
}

function highlight(str: string, lang: string) {
  const highlighter = getHighlighter();

  if (!highlighter) {
    console.warn(
      `[markdown-it-proxy] Highlighter(Prismjs or Highlight.js) is required!`
    );
    return str;
  }

  if (!lang) {
    return str;
  }

  lang = getLangCodeFromExtension(lang.toLowerCase());

  if (highlighter.getLanguage(lang)) {
    try {
      return highlighter.highlight(str, lang);
    } catch (error) {
      console.error(
        `[markdown-it-proxy] Syntax highlight for language ${lang} failed.`
      );
      return str;
    }
  } else {
    console.warn(
      `[markdown-it-proxy] Syntax highlight for language "${lang}" is not supported.`
    );
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
