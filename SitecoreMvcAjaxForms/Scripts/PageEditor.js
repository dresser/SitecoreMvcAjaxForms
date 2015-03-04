(function($) {

    $('.page-editor')
        .on('click', '.page-editor-tabs a', function(e) {
            e.preventDefault();
            var selIndex = $(this).data('index');
            $('.page-editor-tabs a').not($(this)).removeClass('selected');
            $(this).addClass('selected');
            $('.page-editor-tab-content.selected').removeClass('selected');
            $('.page-editor-tab-content[data-index=' + selIndex + ']').addClass('selected');
        });

})(jQuery);