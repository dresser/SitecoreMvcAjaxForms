(function($) {

    var resetValidation = function () {
        $('form')
            .removeData('validator')
            .removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    };

    var initialize = function() {
        resetValidation();
    };

    var containerSelector = function() {
        return '#ajaxContainer';
    };

    var pageEditorMode = function() {
        if (typeof Sitecore === "undefined") {
            return false;
        }
        if (typeof Sitecore.PageModes === "undefined" || Sitecore.PageModes == null) {
            return false;
        }
        return Sitecore.PageModes.PageEditor != null;
    };

    $(containerSelector())
        .on('submit', 'form', function () {
            if (!pageEditorMode()) {
                $.ajax({
                    type: "POST",
                    url: $(this).attr('action'),
                    data: $(this).serialize(),
                    success: function(data) {
                        if (data.Success) {
                            var url = data.Actions[0].Value;
                            $(containerSelector()).load(url, function() {
                                initialize();
                            });
                        } else {
                            var validator = $("form").validate();
                            validator.showErrors(data.Errors);
                        }
                    }
                });
            }
            return false;
        })
        .on('click', 'a', function(e) {
            if (pageEditorMode() == false &&
                $(this).attr('href').toLowerCase().indexOf(window.location.pathname.toLowerCase()) == 0) {
                e.preventDefault();
                $(containerSelector()).load($(this).attr('href'), function() {
                    initialize();
                });
                return false;
            }
            return true;
        });

})(jQuery.noConflict());