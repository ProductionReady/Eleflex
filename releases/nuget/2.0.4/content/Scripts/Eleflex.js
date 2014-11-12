$(document).ready(function () {

    //LOAD ALL DEFAULT DATA TABLES
    $('.datatable').DataTable();

    //LOAD ALL DEFAULT CHOSEN SELECT INPUTS
    var config = {
        '.chosen-select': { no_results_text: 'No Results Found!' },
        '.chosen-single-select': { allow_single_deselect: true, disable_search_threshold: 10, width: "150px" },        
    };
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    //LOAD ALL DEFAULT DATEPICKER AND DATETIMEPICKER INPUTS
    $('.datepicker').datetimepicker({ pickTime: false });
    $('.datetimepicker').datetimepicker({ pickTime: true });

});

/* Should prefixing everything with eleflex be required? Need to think about standards with multi-component scripts loading in page, duplicate function names if not? prob... dagger */

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
                if ($(this).hasClass('chosen-select') || $(this).hasClass('chosen-single-select')) {
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