import MarkdownIt from "markdown-it";
import markdownItAnchor from "markdown-it-anchor";
import markdownItAttrs from "markdown-it-attrs";
import markdownItFrontMatter from "markdown-it-front-matter";
import markdownItHeaderSections from "markdown-it-header-sections";

import { highlight } from "./highlighter";

type MarkdownItMore = {
  md: MarkdownIt;
  frontMatter: {
    meta?: string;
    cb: (s: string) => void;
  };
  toc: {
    contents: any;
    cb: (tocMarkdown, tocArray, tocHtml) => void;
  };
};

type MarkdownItMoreDict = {
  [prop: string]: MarkdownItMore;
};

let md: MarkdownIt = undefined;

const mdDict: MarkdownItMoreDict = {};

function create(
  options: MarkdownIt.Options = {},
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

  more.toc.contents = [];
  more.toc.cb = (_, array) => {
    more.toc.contents = array;
  };

  const md = new MarkdownIt(options)
    .use(markdownItAttrs)
    .use(markdownItFrontMatter, more.frontMatter.cb);

  md.use(markdownItAnchor, {
    level: 1, // todo: support for custom config
    permalink: true,
    permalinkSymbol: '',
    permalinkClass: '',
    slugify: (s: string) => hashString(s),
    callback: (_token, info) => {
      more.toc.contents.push({ content: info.title, anchor: info.slug, level: _token.markup.length });
    }
  });

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  if (window.BlazorComponent && window.BlazorComponent.markdownItRules) {
    window.BlazorComponent.markdownItRules(key, md)
  }

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
  more.toc.contents = [];

  const markup = more.md.render(src);

  return {
    frontMatter: more.frontMatter.meta,
    markup: markup,
    toc: more.toc.contents,
  };
}

function hashString(str: string) {
  let slug = String(str)
    .trim()
    .toLowerCase()
    .replace(/[\s,.[\]{}()/]+/g, "-")
    .replace(/[^a-z0-9 -]/g, (c) => c.charCodeAt(0).toString(16))
    .replace(/-{2,}/g, "-")
    .replace(/^-*|-*$/g, "");

  if (slug.charAt(0).match(/[^a-z]/g)) {
    slug = "section-" + slug;
  }

  return encodeURIComponent(slug);
}

export { create, parse, parseAll, highlight };
