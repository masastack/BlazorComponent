import MarkdownIt from "markdown-it";
import markdownItClass from "markdown-it-class";
import markdownItFrontMatter from "markdown-it-front-matter";
import markdownItHeaderSections from "markdown-it-header-sections";

import { highlight } from "./highlighter";

type MarkdownItMore = {
  md: MarkdownIt;
  frontMatter: {
    meta?: string;
    cb: (s: string) => void;
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
    } as MarkdownItMore);

  more.frontMatter.meta = undefined;
  more.frontMatter.cb = (s) => {
    more.frontMatter.meta = s;
  };

  const md = new MarkdownIt(options)
    .use(markdownItClass, tagClassMap)
    .use(markdownItFrontMatter, more.frontMatter.cb);

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  more.md = md;

  mdDict[key] = more;
}

function parse(src: string, key: string = "default") {
  const [, markup] = parseAll(src, key);
  return markup;
}

function parseAll(src: string, key: string = "default") {
  key ??= "default";

  const more = mdDict[key];
  more.frontMatter.meta = undefined;

  const markup = more.md.render(src);

  return [more.frontMatter.meta, markup];
}

export { create, parse, parseAll, highlight };
