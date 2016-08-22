/// <reference path="jquery.d.ts" />
/// <reference path="kendo.all.d.ts" />
var _kendo_widget_class = (function () {
    function _kendo_widget_class() {
    }
    /**
     * Gets the last time the specified control raised the event.
     * @param uuid
     * @param eventName
     */
    _kendo_widget_class.prototype.lastEvent = function (uuid, eventName) {
        if (this.widgets && this.widgets[uuid]) {
            return this.widgets[uuid][eventName];
        }
        return undefined;
    };
    /**
     * Tracks when the widget last raised the event.
     * @param uuid
     * @param widget
     * @param eventName
     */
    _kendo_widget_class.prototype.trackEvent = function (uuid, widget, eventName) {
        var _this = this;
        widget.bind(eventName, function (e) { _this.widgets[uuid][eventName] = Date.now(); });
    };
    return _kendo_widget_class;
}());
