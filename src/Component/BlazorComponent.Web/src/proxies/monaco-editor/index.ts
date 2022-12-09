import { editor as MonacoEditor } from "monaco-editor";

interface Monaco {
  editor: typeof MonacoEditor;
}

declare const monaco: Monaco;

function init(id: string, options: MonacoEditor.IStandaloneEditorConstructionOptions) {
  return monaco.editor.create(document.getElementById(id), options);
}

function getValue(instance: MonacoEditor.IStandaloneCodeEditor) {
  return instance.getValue();
}
function setValue(instance: MonacoEditor.IStandaloneCodeEditor, value) {
  instance.setValue(value);
}
function setTheme(theme: string) {
  monaco.editor.setTheme(theme);
}

function getModels() {
  return monaco.editor.getModels();
}

function getModel(instance: MonacoEditor.IStandaloneCodeEditor) {
  return instance.getModel();
}

function setModelLanguage(instance: MonacoEditor.IStandaloneCodeEditor, languageId: string) {
  monaco.editor.setModelLanguage(instance.getModel(), languageId);
}
function remeasureFonts() {
  monaco.editor.remeasureFonts();
}

function addKeybindingRules(rules: MonacoEditor.IKeybindingRule[]) {
  monaco.editor.addKeybindingRules(rules);
}

function addKeybindingRule(rule: MonacoEditor.IKeybindingRule) {
  monaco.editor.addKeybindingRule(rule);
}

export {
  init,
  getValue,
  setValue,
  setTheme,
  getModels,
  getModel,
  setModelLanguage,
  remeasureFonts,
  addKeybindingRules,
  addKeybindingRule
}