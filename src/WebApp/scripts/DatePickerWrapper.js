"use strict";
/// <reference path="jquery.d.ts" />
/// <reference path="kendo.all.d.ts" />
var _kendo_widget_class = (function () {
    function _kendo_widget_class() {
    }
    _kendo_widget_class.prototype.when = function (uuid, eventName) {
        if (this.widgets && this.widgets[uuid]) {
            return this.widgets[uuid][eventName];
        }
        return undefined;
    };
    _kendo_widget_class.prototype.bind = function (uuid, widget, eventName) {
        var _this = this;
        widget.bind(eventName, function (e) {
            _this.widgets[uuid][eventName] = Date.now();
        });
    };
    return _kendo_widget_class;
}());
exports._kendo_widget_class = _kendo_widget_class;
//# sourceMappingURL=DatePickerWrapper.js.map