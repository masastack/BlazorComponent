import { editor } from "monaco-editor";

function init(id: string, options: editor.IStandaloneEditorConstructionOptions) {
  return editor.create(document.getElementById(id), options);
}

function defineTheme(name, value: editor.IStandaloneThemeData) {
  editor.defineTheme(name, value)
}

function getValue(instance: editor.IStandaloneCodeEditor) {
  return instance.getValue();
}

function setValue(instance: editor.IStandaloneCodeEditor, value) {
  instance.setValue(value);
}

function setTheme(theme: string) {
  editor.setTheme(theme);
}

function colorizeElement(id: string, options: any) {
  editor.colorizeElement(document.getElementById(id), options);
}

function updateOptions(instance: editor.IStandaloneCodeEditor, options: any) {
  instance.updateOptions(options);
}

function getModels() {
  return editor.getModels();
}

function getModel(instance: editor.IStandaloneCodeEditor) {
  return instance.getModel();
}

function setModelLanguage(instance: editor.IStandaloneCodeEditor, languageId: string) {
  editor.setModelLanguage(instance.getModel(), languageId);
}
function remeasureFonts() {
  editor.remeasureFonts();
}

function addKeybindingRules(rules: editor.IKeybindingRule[]) {
  editor.addKeybindingRules(rules);
}

function addKeybindingRule(rule: editor.IKeybindingRule) {
  editor.addKeybindingRule(rule);
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
  colorizeElement,
  defineTheme,
  updateOptions,
  addKeybindingRule
}