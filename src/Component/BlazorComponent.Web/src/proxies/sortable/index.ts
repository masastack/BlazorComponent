import Sortable, { GroupOptions, SortableOptions } from "sortablejs";

type Options = Omit<SortableOptions, "store" | "group"> & {
  group: {
    name: string;
    groupPulls?: string[] | undefined;
    groupPuts?: string[] | undefined;
    cloneable: boolean;
  };
};

class SortableProxy {
  el: HTMLElement;
  handle: DotNet.DotNetObject;
  sortable: Sortable;

  constructor(
    el: HTMLElement,
    options: Options,
    order: string[],
    handle: DotNet.DotNetObject
  ) {
    this.el = el;
    this.handle = handle;

    console.log("[Sortable] init", options);
    const { group, ...rest } = options;

    this.sortable = new Sortable(el, {
      ...options,
      group: {
        name: group.name,
        pull: group.groupPulls ?? true,
        put: (to, from, drag) => {
          if (to.toArray().includes(drag.getAttribute("data-id"))) {
            console.warn(
              `[MSortable] Group "${
                group.name
              }" already has an item with the [data-id] "${drag.getAttribute(
                "data-id"
              )}", so it can't be added.`
            );
            return false;
          }

          if (!group.groupPulls) {
            return true;
          }

          const toGroup =
            typeof to.options.group === "string"
              ? to.options.group
              : to.options.group.name;
          return group.groupPulls.includes(toGroup);
        },
      },
      store: {
        get: (sortable) => {
          console.log("[Sortable] get");
          return order;
        },
        set: (sortable) => {
          const order = sortable.toArray();
          console.log("[Sortable] set", order);
          this.handle.invokeMethodAsync("UpdateOrder", order);
        },
      },
      onMove(evt, originalEvent) {
        evt.dragged.getAttribute("data-id");
      },
    });
  }

  getOrder() {
    return this.sortable.toArray();
  }

  invokeVoid(prop: string, args: any[]) {
    if (this.sortable[prop] && typeof this.sortable[prop] === "function") {
      this.sortable[prop](...args);
    }
  }
}

function init(
  el: HTMLElement,
  options: Options,
  order: string[],
  handle: DotNet.DotNetObject
) {
  return new SortableProxy(el, options, order, handle);
}

export { init };
