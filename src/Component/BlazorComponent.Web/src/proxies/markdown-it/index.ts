import MarkdownIt from "markdown-it";
import markdownItClass from "markdown-it-class";
import markdownItHeaderSections from "markdown-it-header-sections";

import { highlight } from "./highlighter";

let md: MarkdownIt = undefined;

const mdDict = {};

function create(
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

function parse(src: string, key: string = "default") {
  key ??= "default";
  return mdDict[key].render(src);
}

export {
  create,
  parse,
  highlight,
}
