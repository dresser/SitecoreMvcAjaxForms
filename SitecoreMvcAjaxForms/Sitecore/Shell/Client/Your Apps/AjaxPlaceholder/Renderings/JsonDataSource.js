define(["jquery", "sitecore"], function($, Sitecore) {
    "use strict";

    var model = Sitecore.Definitions.Models.ComponentModel.extend(
        {
            initialize: function(attributes) {
                this._super();
                console.log('JsonDataSource.js:initialize()');
                var that = this;
                var pageItemId = Sitecore.Helpers.url.getQueryParameters(window.location.href)['pageItemId'];
                var deviceId = Sitecore.Helpers.url.getQueryParameters(window.location.href)['deviceId'];
                var referenceId = Sitecore.Helpers.url.getQueryParameters(window.location.href)['referenceId'];
                var renderingId = Sitecore.Helpers.url.getQueryParameters(window.location.href)['renderingId'];
                console.log(pageItemId, deviceId, referenceId, renderingId);
                var query = window.location.href.substr(window.location.href.indexOf("?"));
                console.log(query);
                $.ajax({
                    url: "/mvcdemo/ajaxplaceholder/GetPlaceholderRenderings" + query,
                    type: "GET",
                    success: function(data) {
                        console.log(data);
                        that.set("json", data);
                    }
                });
            },
            add: function(data) {
                var json = this.get("json");
                if (json === null)
                    json = new Array();

                // this is done because array.push changes the array to an object which then do no work on the SPEAK listcontrol.
                var newArray = new Array(json.length + 1);
                for (var i = json.length - 1; i >= 0; i--)
                    newArray[i + 1] = json[i];
                newArray[0] = data;
                this.set("json", newArray);
            },
            selectForEdit: function (renderingId) {
                var app = this;
                alert('rendering ' + renderingId + ' selected');
            }
        }
    );

    var view = Sitecore.Definitions.Views.ComponentView.extend(
        {
            initialize: function() {
                this._super();
                this.model.set("json", null);
            }
        }
    );

    Sitecore.Factories.createComponent("JsonDataSource", model, view, "script[type='text/x-sitecore-jsondatasource']");
});
