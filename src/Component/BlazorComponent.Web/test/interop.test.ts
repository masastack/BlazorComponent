import { getDom, log } from '../src/interop';

test('log', () => {
    expect(log('hello')).toBe(undefined);
})

test('get-dom', () => {
    expect(getDom(false)).toBe(document.body);
})

test('get-dom-document', () => {
    expect(getDom('document')).toBe(document.documentElement);
})

test('get-dom-not-exist-element', () => {
    expect(getDom('#not-exist-element')).toBeNull();
})

test('get-dom-container', () => {
    document.body.innerHTML = '<div id=\'container\'></div>';
    expect(getDom('#container')).not.toBeNull();
})

