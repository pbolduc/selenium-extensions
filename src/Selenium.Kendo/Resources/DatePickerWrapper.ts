/// <reference path="jquery.d.ts" />
/// <reference path="kendo.all.d.ts" />
class _kendo_widget_class {

    private widgets: { [uuid: string]: { [eventName: string]: number } }

    /**
     * Gets the last time the specified control raised the event.
     * @param uuid
     * @param eventName
     */
    public lastEvent(uuid: string, eventName: string): number {
        if (this.widgets && this.widgets[uuid]) {
            return this.widgets[uuid][eventName];
        }
        return undefined;
    }

    /**
     * Tracks when the widget last raised the event.
     * @param uuid
     * @param widget
     * @param eventName
     */
    public trackEvent(uuid: string, widget: kendo.ui.Widget, eventName: string): void {
        widget.bind(eventName, (e) => { this.widgets[uuid][eventName] = Date.now(); });
    }
}
