define(["sitecore", "jquery"], function (Sitecore, jQuery) {
    var AjaxPlaceholderDialog = Sitecore.Definitions.App.extend({
 
        initialized: function ()
        {
            // our work goes here
            console.log('Dialog.js:initialized()');
            console.log(Sitecore.Pipelines);
            var items = this.JsonList.get('items');
            console.log(items);
            this.JsonList.set('selectedItemId', '');
        },

        rowSelected: function () {
            var app = this;
            console.log(app.JsonList);
            console.log('row selected');
        },

        submitDialog: function () {
            var app = this;

            var selRenderingId = app.JsonList.get('selectedItemId');
            $.ajax({
                url: "/mvcdemo/ajaxplaceholder/SetSelectedRendering?renderingReferenceId=" + selRenderingId,
                type: "POST",
                success: function (data) {
                    console.log(data);
                }
            });
        }
 
    });
    return AjaxPlaceholderDialog;
});