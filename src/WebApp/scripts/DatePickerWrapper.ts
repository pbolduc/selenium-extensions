/// <reference path="jquery.d.ts" />
/// <reference path="kendo.all.d.ts" />
export class _kendo_widget_class {

    private widgets: { [uuid: string]: { [eventName: string]: number } }

    public when(uuid: string, eventName: string): number {
        if (this.widgets && this.widgets[uuid]) {
            return this.widgets[uuid][eventName];
        }
        return undefined;
    }

    public bind(uuid: string, widget: kendo.ui.Widget, eventName: string): void {
        widget.bind(eventName, (e) => {
            this.widgets[uuid][eventName] = Date.now();
        });
    }
}

