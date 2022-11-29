declare const monaco: any;

function init(id, options) {
    return monaco.editor.create(document.getElementById(id), options);
}

function getValue(myEditor) {
    return myEditor.getValue();
}
function setValue(myEditor, value) {
    myEditor.setValue(value);
    return true;
}
function setTheme(myEditor, theme) {
    myEditor.setTheme(theme);
    return true;
}

function getModels(myEditor) {
    return myEditor.getModels();
}

function getModel(myEditor, uri) {
    return myEditor.getModel(uri);
}
function setModelLanguage(myEditor, languageId) {
    monaco.editor.setModelLanguage(myEditor.getModel(), languageId);
}
function remeasureFonts(myEditor) {
    myEditor.remeasureFonts();
}

function addKeybindingRules(myEditor, rules) {
    myEditor.addKeybindingRules(rules);
}

function addKeybindingRule(myEditor, rule) {
    myEditor.addKeybindingRule(rule);
}

export {init,getValue,setValue,setTheme,getModels,getModel,setModelLanguage,remeasureFonts,addKeybindingRules,addKeybindingRule}