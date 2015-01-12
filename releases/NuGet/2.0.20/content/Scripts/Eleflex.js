$(document).ready(function () {

    //LOAD ALL DEFAULT DATA TABLES (Jquery.Datatables)
    $('.eleflexdatatable').DataTable();

    //LOAD ALL DEFAULT SELECT INPUTS (Chosen)
    var config = {
        '.eleflexselect': { allow_single_deselect: true, disable_search_threshold: 10 },
        '.eleflexselectrequired': { allow_single_deselect: false, disable_search_threshold: 10 },
        '.eleflexselectmulti': { allow_single_deselect: true, disable_search_threshold: 10, no_results_text: 'No Results Found!' },
        '.eleflexselectmultirequired': { allow_single_deselect: false, disable_search_threshold: 10, no_results_text: 'No Results Found!' },
    };
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    //LOAD ALL DEFAULT DATEPICKER AND DATETIMEPICKER INPUTS (Bootstrap.datetimepicker)
    $('.eleflexdate').datetimepicker({ pickTime: false });
    $('.eleflexdaterequired').datetimepicker({ pickTime: false });
    $('.eleflexdatetime').datetimepicker({ pickTime: true });
    $('.eleflexdatetimerequired').datetimepicker({ pickTime: true });

});

//REGISTER JQUERY EXTENSION FOR BOOTSTAP VALIDATOR INTEGRATION
(function ($) {
    var defaultOptions = {
        errorClass: 'has-error',
        validClass: 'has-success',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .addClass(errorClass)
                .removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
            .removeClass(errorClass)
            //.addClass(validClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        //validClass: defaultOptions.validClass,
    };
})(jQuery);

//CLEAR ALL INPUT CONTROLS BEGINNING AT THE SPECIFIED ROOT ELEMENT
function eleflexClearInput(rootElement) {
    $(rootElement).find(':input').each(function () {
        switch (this.type) {
            case 'hidden':
            case 'password':
            case 'select':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':            
                $(this).val('');
                if ($(this).hasClass('eleflexselect') || $(this).hasClass('eleflexselectrequired') || $(this).hasClass('eleflexselectmulti') || $(this).hasClass('eleflexselectmultirequired')) {
                    $(this).find('option:selected').removeAttr("selected");
                    $(this).trigger('chosen:updated');
                }
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });
}

