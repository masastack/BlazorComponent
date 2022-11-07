import { Buffer } from "buffer";
import kebabCase from "lodash.kebabcase";
import MarkdownIt from "markdown-it";
import markdownItClass from "markdown-it-class";
import markdownItFrontMatter from "markdown-it-front-matter";
import markdownItHeaderSections from "markdown-it-header-sections";

import { highlight } from "./highlighter";
import markdownItTocDesc, { Heading, MarkdownTocDescOption } from "./markdown-it-toc-desc";

type MarkdownItMore = {
  md: MarkdownIt;
  frontMatter: {
    meta?: string;
    cb: (s: string) => void;
  };
  toc: {
    headings: Heading[];
    cb: (tree: Heading[]) => void;
  };
};

type MarkdownItMoreDict = {
  [prop: string]: MarkdownItMore;
};

let md: MarkdownIt = undefined;

const mdDict: MarkdownItMoreDict = {};

function create(
  options: MarkdownIt.Options = {},
  tagClassMap: { [prop: string]: string[] } = {},
  enableHeaderSections: boolean = false,
  key: string = "default"
) {
  key ??= "default";

  options = { ...options, highlight };

  const more =
    mdDict[key] ??
    ({
      frontMatter: {},
      toc: {},
    } as MarkdownItMore);

  more.frontMatter.meta = undefined;
  more.frontMatter.cb = (s) => {
    more.frontMatter.meta = s;
  };

  more.toc.headings = [];
  more.toc.cb = (tree) => {
    more.toc.headings = tree;
  };

  const tocOptions: MarkdownTocDescOption = {
    includeLevel: [2, 3, 4],
    slugify: (s) => hashString(kebabCase(s)),
    getTocs: more.toc.cb,
  };

  const md = new MarkdownIt(options)
    .use(markdownItFrontMatter, more.frontMatter.cb)
    .use(markdownItTocDesc, tocOptions);

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  md.use(markdownItClass, tagClassMap);

  more.md = md;

  mdDict[key] = more;
}

function parse(src: string, key: string = "default") {
  const { markup } = parseAll(src, key);
  return markup;
}

function parseAll(src: string, key: string = "default") {
  key ??= "default";

  const more = mdDict[key];
  more.frontMatter.meta = undefined;
  more.toc.headings = [];

  const markup = more.md.render(src);

  return {
    frontMatter: more.frontMatter.meta,
    markup: markup,
    toc: more.toc.headings,
  };
}

export function hashString(str: string) {
  const encodedStr = encodeURIComponent(str);
  if (encodedStr === str) {
    return str;
  }

  let hash = window.btoa(unescape(encodedStr));
  hash = hash.substring(hash.length - 5);
  return hash;
}

export { create, parse, parseAll, highlight };
