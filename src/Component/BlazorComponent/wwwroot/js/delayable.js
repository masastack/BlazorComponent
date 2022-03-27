class Delayable {
    #dotNetHelper = {};

    #openDelay = 0;
    #closeDelay = 0;
    #openTimeout;
    #closeTimeout;

    constructor(dotNet, openDelay, closeDelay) {
        this.#dotNetHelper = dotNet;
        this.#openDelay = openDelay;
        this.#closeDelay = closeDelay;
    }

    #clearDelay() {
        clearTimeout(this.#openTimeout);
        clearTimeout(this.#closeTimeout);
    }

    runDelay(type, cb) {
        this.#clearDelay();

        if (type === "open") {
            const delay = parseInt(this.#openDelay, 10);

            this.#openTimeout = setTimeout(cb || (() => {
                this.#dotNetHelper.invokeMethodAsync('SetActive', true);
            }), delay)
        } else {
            const delay = parseInt(this.#closeDelay, 10);

            this.#closeTimeout = setTimeout(cb || (() => {
                this.#dotNetHelper.invokeMethodAsync('SetActive', false);
            }), delay)
        }
    }
}

let delayers = {};

function init(dotNet, openDelay = 0, closeDelay = 0) {
    const key = dotNet["_id"];
    delayers[key] = new Delayable(dotNet, openDelay, closeDelay);
}

function runDelay(dotNet, type) {
    const key = dotNet["_id"];
    const delayer = delayers[key];
    delayer.runDelay(type)
}

function remove(dotNet) {
    const key = dotNet["_id"];
    if (Object.keys(delayers).includes(key)) {
        console.log(delayers, key)
        delete delayers[key]
    }
}

export {
    init,
    runDelay,
    remove
}
