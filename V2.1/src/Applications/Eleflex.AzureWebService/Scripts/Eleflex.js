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

    //LOAD ALL DEFAULT DATEPICKER AND DATETIMEPICKER INPUTS (smalot.bootstrap-datetimepicker)
    $('.eleflexdate').datetimepicker({ autoclose: true, forceParse: false, format: 'm/dd/yyyy', showMeridian: true, todayBtn: true, todayHighlight: true, viewSelect: 'decade' });
    $('.eleflexdate').datetimepicker('update');
    $('.eleflexdaterequired').datetimepicker({ autoclose: true, forceParse: false, format: 'm/dd/yyyy', showMeridian: true, todayBtn: true, todayHighlight: true, viewSelect: 'decade' });
    $('.eleflexdaterequired').datetimepicker('update');
    $('.eleflexdatetime').each(function () {
        var curVal = $(this).val();
        $(this).datetimepicker({ autoclose: true, forceParse: false, format: 'm/dd/yyyy HH:ii P', showMeridian: true, todayBtn: true, todayHighlight: true, viewSelect: 'decade' });
        if (curVal) {
            $(this).datetimepicker('update', moment(curVal).toDate());
        }
    });
    $('.eleflexdatetimerequired').each(function () {
        var curVal = $(this).val();
        $(this).datetimepicker({ autoclose: true, forceParse: false, format: 'm/dd/yyyy HH:ii P', showMeridian: true, todayBtn: true, todayHighlight: true, viewSelect: 'decade' });
        if (curVal) {
            $(this).datetimepicker('update', moment(curVal).toDate());
        }
    });

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
            //case 'hidden':
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

//GET VERIFICATION TOKEN USED FOR AJAX REQUESTS
function eleflexGetRequestVerificationToken() {
    return $('input[name="__RequestVerificationToken"]').val();
}

//HANGLE RETURN OBJECT FOR AJAX REQUESTS
function eleflexHandleAjaxResult(result, onsuccess) {

    //Parse result
    var ajaxResult = null;
    try {
        ajaxResult = $.parseJSON(result);
    }
    catch (error) {
        eleflexShowErrorMessage("Could not load response. " + error);
        return null;
    }

    //Show message
    if (ajaxResult.Status && ajaxResult.Message) {
        if (ajaxResult.Status == "success") {
            eleflexShowSuccessMessage(ajaxResult.Message);
        }
        if (ajaxResult.Status == "info") {
            eleflexShowInfoMessage(ajaxResult.Message);
        }
        if (ajaxResult.Status == "warning") {
            eleflexShowWarningMessage(ajaxResult.Message);
        }
        if (ajaxResult.Status == "error") {
            eleflexShowErrorMessage(ajaxResult.Message);
        }
    }

    //Check if reload or redirect
    if (ajaxResult.Redirect) {
        if (ajaxResult.Redirect == "reload") {
            setTimeout(function () { window.location = window.location; }, 1500);
        }
        else {
            setTimeout(function () { window.location.href = ajaxResult.Redirect; }, 1500);
        }
        return ajaxResult;
    }
    return ajaxResult;
}

//SHOW AND HIDE ALERT MESSAGES
function eleflexShowSuccessMessage(message) {
    var output = eleflexBuildAlertMessage("success", message);
    $("#MasterAlertMessage").html(output);
}
function eleflexShowInfoMessage(message) {
    var output = eleflexBuildAlertMessage("info", message);
    $("#MasterAlertMessage").html(output);
}
function eleflexShowWarningMessage(message) {
    var output = eleflexBuildAlertMessage("warning", message);
    $("#MasterAlertMessage").html(output);
}
function eleflexShowErrorMessage(message) {
    var output = eleflexBuildAlertMessage("error", message);
    $("#MasterAlertMessage").html(output);
}
function eleflexDeleteMessages() {
    $("#MasterAlertMessage").html('');
}
function eleflexBuildAlertMessage(severity, message) {

    //Determine type of message
    var alertCSS = "";
    switch (severity) {
        case "success":
        default:
            alertCSS = "alert-success";
            break;
        case "info":
            alertCSS = "alert-info";
            break;
        case "warning":
            alertCSS = "alert-warning";
            break;
        case "error":
            alertCSS = "alert-danger";
            break;
    }

    //Build core html
    var outputMessage = "<div class='alert alert-dismissible " + alertCSS + "' role='alert'>";
    outputMessage += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

    //Check for multiple messages
    if ($.isArray(message)) {
        outputMessage += "<ul>";
        for (var i = 0; i < message.length; i++) {
            result += "<li>" + message[i] + "</li>";
        }
        outputMessage += "</ul>";
    }
    else {
        outputMessage += message;
    }
    outputMessage += "</div>";

    //Return html
    return outputMessage;
}

