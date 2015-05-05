define(["sitecore", "jquery", "knockout"], function (Sitecore, jQuery, ko) {
    var AjaxPlaceholderDialog = Sitecore.Definitions.App.extend({

        initialized: function () {
            var app = this;
            window.setTimeout(this.selectSelectedRow, 100, app); //TODO remove horrible setTimeout hack
        },

        selectSelectedRow: function(app) {
            //TODO figure out how to ensure that the relevant row gets selected automatically
            $.each(app.JsonList.viewModel.$el.find('td.ventilate'), function () {
                var ctx = ko.contextFor(this);
                if (ctx.$data.selected()) {
                    $(this.closest('tr')).addClass('active');
                }
            });
        },

        submitDialog: function () {
            var app = this;

            var selRenderingId = app.JsonList.get('selectedItemId');
            $.ajax({
                url: "/mvcdemo/ajaxplaceholder/SetSelectedRendering?renderingReferenceId=" + selRenderingId,
                type: "POST",
                success: function (data) {
                    window.top.location.reload(true);
                }
            });
        }
 
    });
    return AjaxPlaceholderDialog;
});