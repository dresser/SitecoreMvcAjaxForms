define(["sitecore", "jquery"], function (Sitecore, jQuery) {
    var AjaxPlaceholderDialog = Sitecore.Definitions.App.extend({
 
        initialized: function ()
        {
            // our work goes here
            console.log('PageCode referenced script has loaded');
        },

        doStuff: function () {
            console.log('doStuff was called');
        }
 
    });
    return AjaxPlaceholderDialog;
});