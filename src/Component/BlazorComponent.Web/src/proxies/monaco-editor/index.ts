declare const monaco: any;

function init(id, options) {
    return monaco.editor.create(document.getElementById(id), options);
}

function getValue(instance) {
    return instance.getValue();
}
function setValue(instance, value) {
    instance.setValue(value);
    return true;
}
function setTheme(instance, theme) {
    instance.setTheme(theme);
    return true;
}

function getModels(instance) {
    return instance.getModels();
}

function getModel(instance, uri) {
    return instance.getModel(uri);
}
function setModelLanguage(instance, languageId) {
    monaco.editor.setModelLanguage(instance.getModel(), languageId);
}
function remeasureFonts(instance) {
    instance.remeasureFonts();
}

function addKeybindingRules(instance, rules) {
    instance.addKeybindingRules(rules);
}

function addKeybindingRule(instance, rule) {
    instance.addKeybindingRule(rule);
}

export { init, getValue, setValue, setTheme, getModels, getModel, setModelLanguage, remeasureFonts, addKeybindingRules, addKeybindingRule }