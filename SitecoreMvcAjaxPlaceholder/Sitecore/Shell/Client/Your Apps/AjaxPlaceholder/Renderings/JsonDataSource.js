define(["jquery", "sitecore"], function($, Sitecore) {
    "use strict";

    var model = Sitecore.Definitions.Models.ComponentModel.extend(
        {
            initialize: function(attributes) {
                this._super();
                var that = this;
                var query = window.location.href.substr(window.location.href.indexOf("?"));
                $.ajax({
                    url: "/mvcdemo/ajaxplaceholder/GetPlaceholderRenderings" + query,
                    type: "GET",
                    success: function(data) {
                        that.set("json", data);
                    }
                });
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
