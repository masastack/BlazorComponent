import MarkdownIt from "markdown-it";
import markdownItAnchor from "markdown-it-anchor";
import markdownItAttrs from "markdown-it-attrs";
import markdownItFrontMatter from "markdown-it-front-matter";
import markdownItHeaderSections from "markdown-it-header-sections";

import { highlight } from "./highlighter";

type MarkdownParser = {
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

function create(
  options: MarkdownIt.Options = {},
  enableHeaderSections: boolean = false,
  anchorOptions = null,
  scope: string = null
) {
  options = { ...options, highlight };

  const parser = {
    frontMatter: {},
    toc: {},
  } as MarkdownParser;

  parser.frontMatter.meta = undefined;
  parser.frontMatter.cb = (s) => {
    parser.frontMatter.meta = s;
  };

  parser.toc.contents = [];
  parser.toc.cb = (_, array) => {
    parser.toc.contents = array;
  };

  const md = new MarkdownIt(options)
    .use(markdownItAttrs)
    .use(markdownItFrontMatter, parser.frontMatter.cb);

  if (anchorOptions) {
    let slugify = (s: string) => hashString(s);
    if (window.MasaBlazor.markdownItAnchorSlugify) {
      slugify = (s) => window.MasaBlazor.markdownItAnchorSlugify(scope, s);
    }

    md.use(markdownItAnchor, {
      level: anchorOptions.level,
      permalink: anchorOptions.permalink,
      permalinkSymbol: anchorOptions.permalinkSymbol,
      permalinkClass: anchorOptions.permalinkClass,
      slugify,
      callback: (_token, info) => {
        parser.toc.contents.push({
          content: info.title,
          anchor: info.slug,
          level: _token.markup.length,
        });
      },
    });
  }

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  if (window.MasaBlazor && window.MasaBlazor.markdownItRules) {
    window.MasaBlazor.markdownItRules(scope, md);
  }

  parser.md = md;

  return parser;
}

function parse(parser: MarkdownParser, src: string) {
  const { markup } = parseAll(parser, src);
  return markup;
}

function parseAll(parser: MarkdownParser, src: string) {
  if (parser) {
    parser.frontMatter.meta = undefined;
    parser.toc.contents = [];

    const markup = parser.md.render(src);

    return {
      frontMatter: parser.frontMatter.meta,
      markup: markup,
      toc: parser.toc.contents,
    };
  }

  return {};
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
