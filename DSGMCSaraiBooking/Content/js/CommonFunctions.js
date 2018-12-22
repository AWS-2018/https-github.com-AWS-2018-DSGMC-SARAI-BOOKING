function PositiveNumsInt(e) {
    e.value = e.value.replace(/[^0-9]/g, '');
    e.value = e.value.replace(/(\\..*)\\./g, '$1');
    return e.value;
}

function NegativeNumsInt(e) {
    e.value = e.value.replace(/[^0-9-]/g, '');
    e.value = e.value.replace(/(\\..*)\\./g, '$1');

    if (e.value.indexOf('-') != e.value.lastIndexOf('-')) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('-')) + e.value.substr(e.value.lastIndexOf('-') + 1));
    }

    if (e.value.indexOf('-') > 0) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('-')) + e.value.substr(e.value.lastIndexOf('-') + 1));
    }

    return e.value;
}

function DatePickerData(e) {
    e.value = '';
    return e.value;
}

function FormatDecimal(e, digitCount) {
    var data = 0.0;

    if (e != "")
        data = parseFloat(e);

    return data.toFixed(digitCount);
}

function FormatAmount(e, digitCount) {
    if (e.value == "")
        e.value = 0;

    e.value = parseFloat(e.value).toFixed(digitCount);
    return e.value;
}

function FormatNumber(e) {
    if (e.value == "")
        e.value = 0;

    e.value = parseFloat(e.value).toFixed(0);
    return e.value;
}

function ValidateDecimalNumber(e) {
    e.value = e.value.replace(/[^0-9.]/g, '');
    e.value = e.value.replace(/(\\..*)\\./g, '$1');

    if (e.value.indexOf('.') != e.value.lastIndexOf('.')) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('.')) + e.value.substr(e.value.lastIndexOf('.') + 1));
    }

    return e.value;
}

function ValidateNegativeDecimalNumber(e) {
    e.value = e.value.replace(/[^0-9-.]/g, '');
    e.value = e.value.replace(/(\\..*)\\./g, '$1');

    if (e.value.indexOf('.') != e.value.lastIndexOf('.')) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('.')) + e.value.substr(e.value.lastIndexOf('.') + 1));
    }

    if (e.value.indexOf('-') != e.value.lastIndexOf('-')) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('-')) + e.value.substr(e.value.lastIndexOf('-') + 1));
    }

    if (e.value.indexOf('-') > 0) {
        e.value = (e.value.substr(0, e.value.lastIndexOf('-')) + e.value.substr(e.value.lastIndexOf('-') + 1));
    }

    return e.value;
}

function ConfirmStatusChange() {
    return confirm("Are you sure you want to change the Status?");
}

function ConfirmDelete() {
    return confirm("Are you sure you want to Remove this Data?");
}

window.setTimeout(function () {
    $(".status-message").fadeTo(2000, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 1000);

$('.hasTooltip').hover(function () {
    var offset = $(this).offset();
    $(this).next('span').fadeIn(200).addClass('showTooltip');
    $(this).next('span');
}, function () {
    $(this).next('span').fadeOut(200);
});

function RoundNumber(number, digitCount) {
    if (number == "")
        number = 0;

    number = parseFloat(number).toFixed(digitCount);
    return number;
}

function GetJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

function FormatDate(value) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    var day = dt.getDate();
    var monthIndex = dt.getMonth();
    var year = dt.getFullYear();

    return monthNames[monthIndex] + ' ' + day + ', ' + year;
}

function FormatDateTime(value) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var date = new Date(parseFloat(results[1]));

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    var hours = date.getHours() > 12 ? date.getHours() - 12 : date.getHours();
    var am_pm = date.getHours() >= 12 ? "PM" : "AM";
    hours = hours < 10 ? "0" + hours : hours;
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    var time = hours + ":" + minutes + ":" + seconds + " " + am_pm;

    return monthNames[monthIndex] + ' ' + day + ', ' + year + ' ' + time;
}

function FormatJavaScriptDate(date) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return monthNames[monthIndex] + ' ' + day + ', ' + year;
}

function FormatJavaScriptDateTime(date) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    var hours = date.getHours() > 12 ? date.getHours() - 12 : date.getHours();
    var am_pm = date.getHours() >= 12 ? "PM" : "AM";
    hours = hours < 10 ? "0" + hours : hours;
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    var time = hours + ":" + minutes + ":" + seconds + " " + am_pm;

    return monthNames[monthIndex] + ' ' + day + ', ' + year + ' ' + time;
}


function ConvertCSharpDateToJavaScriptDate(date) {
    return new Date(Date.parse(date));
}

function ShowLoaderOnFormSubmit(formId) {
    $(formId).bind('submit', function () {
        var isvalidate = $(formId)[0].checkValidity();
        if (isvalidate) {
            $('#loader').css("display", "block");
            $("body").css("overflow-y", "hidden");
            return true;
        }
        else {
            return false;
        }
    });
}

function ShowSuccessMessage(message) {
    var returnHtml = "";
    returnHtml = returnHtml + "<div class='alert alert-success alert-dismissible status-message' role='alert'>";
    returnHtml = returnHtml + "     <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
    returnHtml = returnHtml + "     <span class='fa fa-success fa-lg'></span>";
    returnHtml = returnHtml + "     <strong>Success!</strong> " + message.replace(/(?:\r\n|\r|\n)/g, '<br />') + "";
    returnHtml = returnHtml + "</div>";

    return returnHtml;
}

function ShowErrorMessage(message) {
    var returnHtml = "";
    returnHtml = returnHtml + "<div class='alert alert-danger alert-dismissible' role='alert'>";
    returnHtml = returnHtml + "     <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
    returnHtml = returnHtml + "     <span class='fa fa-warning fa-lg'></span>";
    returnHtml = returnHtml + "     <strong>Error!</strong><br />" + message.replace(/(?:\r\n|\r|\n)/g, '<br />') + "";
    returnHtml = returnHtml + "</div>";
    return returnHtml;
}

function ShowModalErrorMessage(message) {
    var returnHtml = "";
    returnHtml = returnHtml + "<div class='alert alert-danger alert-dismissible' role='alert'>";
    returnHtml = returnHtml + "     <span class='fa fa-warning fa-lg'></span>";
    returnHtml = returnHtml + "     <strong>Error!</strong><br />" + message.replace(/(?:\r\n|\r|\n)/g, '<br />') + "";
    returnHtml = returnHtml + "</div>";
    return returnHtml;
}

function ShowModalLoadErrorMessage(message) {
    var returnHtml = "";
    returnHtml = returnHtml + " <div class='modal-header' style='padding-top:5px !important; padding-bottom: 0 !important;'>";
    returnHtml = returnHtml + "     <ul class='nav navbar-right panel_toolbox'>";
    returnHtml = returnHtml + "         <li>";
    returnHtml = returnHtml + "             <button type='button' class='btn btn-default btn-sm' data-dismiss='modal'>Close</button>";
    returnHtml = returnHtml + "         </li>";
    returnHtml = returnHtml + "     </ul>";
    returnHtml = returnHtml + " </div>";
    returnHtml = returnHtml + " <div class='modal-body' style='padding-top:2px !important;padding-bottom:0 !important;'>";
    returnHtml = returnHtml + "    " + ShowErrorMessage(message);
    returnHtml = returnHtml + " </div>";

    return returnHtml
}

function EnableDisableRecord(recordStatus, token, targetUrl) {
    var a = confirm("Are you sure to " + recordStatus + " this record?");
    if (a) {
        $.get(targetUrl, { "Token": token }, function (data) {
            if (data.Success == true) {
                var pageNumber = data.PageNumber;
                var menuId = $("#hdnMenuId").val();
                LoadDataInTable(pageNumber, menuId);
                $('#alert').html(ShowSuccessMessage(data.ResponseText));

                $(".status-message").fadeTo(2000, 0).slideUp(500, function () {
                    $(this).remove();
                });

            }
            else {
                $('#alert').html(ShowErrorMessage(data.ResponseText));
            }
        });
    }
}

function IsDecimal(str) {
    if (isNaN(str) || str.indexOf(".") < 0) {
        return false;
    }
    else {
        return true;
    }
}

function ShowAjaxErrorMessage(xhr, errorType, exception) {
    var returnHtml = "";

    returnHtml = returnHtml + "<div class='alert alert-danger alert-dismissible' role='alert'>";
    returnHtml = returnHtml + "     <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
    returnHtml = returnHtml + "     <span class='fa fa-warning fa-lg'></span>";
    returnHtml = returnHtml + "     <strong>Error!</strong><br />";

    returnHtml = returnHtml + "<div class='clearfix'>&nbsp;</div>";
    returnHtml = returnHtml + "<div style='max-height:400px;max-width:auto;overflow:auto;font-size: 10pt! important; font-weight: normal !important;>";

    var responseText;

    try {
        responseText = jQuery.parseJSON(xhr.responseText);
        //responseText = JSON.parse(xhr.responseText);
        //responseText = xhr.responseJson;
        returnHtml = returnHtml + "<div><b>" + errorType + " " + exception + "</b></div>";
        returnHtml = returnHtml + "<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>";
        returnHtml = returnHtml + "<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>";
        returnHtml = returnHtml + "<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>";
    } catch (e) {
        responseText = xhr.responseText;
        returnHtml = returnHtml + responseText;
    }

    returnHtml = returnHtml + "</div>"
    returnHtml = returnHtml + "</div>";

    return returnHtml;
}

function CheckEnglishOnly(field) {
    var sNewVal = "";
    var sFieldVal = field.value;

    for (var i = 0; i < sFieldVal.length; i++) {

        var ch = sFieldVal.charAt(i);

        var c = ch.charCodeAt(0);

        if (c < 0 || c > 255) {
            // Discard
        }
        else {
            sNewVal += ch;
        }
    }

    field.value = sNewVal;
}

function GetIntegerValue(value) {
    var Amount = 0;

    if (value == "")
        Amount = 0;
    else
        Amount = parseInt(value);

    return Amount;
}

function GetDecimalValue(value) {
    var Amount = 0.00;

    if (value == "")
        Amount = 0.00;
    else
        Amount = parseFloat(value);

    return Amount;
}

function ExecuteParentPageFunction() {
    if (parent.jQuery("#hdnParentPageName").length > 0) {
        if (parent.jQuery("#hdnParentPageName").val() == "FeeSlip") {
            parent.GetStudentDetailsForFeeSlipPage();
        }
    }
}

function ShowBootBoxErrorMessage(msg, boxSize) {
    bootbox.alert({
        title: "<span class='error'><span class='fa fa-warning fa-sm'></span>&nbsp;<strong>Error</strong></span>",
        message: msg,
        size: boxSize != undefined ? (
            (boxSize.toUpperCase() == 'S' ? 'small' : 'large')
            ) : (
                'large'
            )
    });
}

function ShowBootBoxDeleteConfirmation(deleteFunction) {
    bootbox.confirm({
        title: '<i class="fa fa-warning fa-lg"></i> Confirmation',
        message: "This will delete the record. Are you sure to continue?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> No'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Yes'
            }
        },
        callback: function (result) {
            if (result == true) {
                deleteFunction();
            }
        }
    });
    return hasConfirmed;
}

function ShowBootBoxApproveConfirmation(approveFunction) {
    bootbox.confirm({
        title: '<i class="fa fa-warning fa-lg"></i> Confirmation',
        message: "This will approve the record. Are you sure to continue?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> No'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Yes'
            }
        },
        callback: function (result) {
            if (result == true) {
                approveFunction();
            }
        }
    });
    return hasConfirmed;
}

function ShowBootBoxLockConfirmation(lockFunction) {
    bootbox.confirm({
        title: '<i class="fa fa-warning fa-lg"></i> Confirmation',
        message: "This will lock the record. Are you sure to continue?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> No'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Yes'
            }
        },
        callback: function (result) {
            if (result == true) {
                lockFunction();
            }
        }
    });
}

function ShowBootBoxUnlockConfirmation(unlockFunction) {
    bootbox.confirm({
        title: '<i class="fa fa-warning fa-lg"></i> Confirmation',
        message: "This will unlock the record. Are you sure to continue?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> No'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Yes'
            }
        },
        callback: function (result) {
            if (result == true) {
                unlockFunction();
            }
        }
    });
}




function AccountMasterList() {

    if (sessionStorage.getItem('WsmAccountMasterList') == null) {
        $.ajax({
            type: "GET",
            url: "/Base/AccountMasterAutoCompleteList",
            async: false,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (response) {
                sessionStorage.setItem('WsmAccountMasterList', JSON.stringify(response.ListData));
                let accountList = JSON.parse(sessionStorage.WsmAccountMasterList);
                console.log(accountList);
                return accountList;
            },
            error: function (response) {
                alert("Sorry! Some error has occured.");
            }
        });
    }
    else {
        return JSON.parse(sessionStorage.WsmAccountMasterList);
    }
}

function Balance(amount) {
    var balance = "0";

    if (parseFloat(amount) > 0) {
        balance = amount + ' Dr.';
    }
    else if (parseFloat(amount) < 0) {
        balance = Math.abs(amount) + ' Cr.';
    }
    else if (parseFloat(amount) == 0) {
        balance = 'NIL';
    }

    return balance;
}

function checkDuplicateInObject(propertyName, inputArray) {
    var seenDuplicate = false,
        testObject = {};

    inputArray.map(function (item) {
        var itemPropertyName = item[propertyName];
        if (itemPropertyName in testObject) {
            testObject[itemPropertyName].duplicate = true;
            item.duplicate = true;
            seenDuplicate = true;
        }
        else {
            testObject[itemPropertyName] = item;
            delete item.duplicate;
        }
    });

    return seenDuplicate;
}

function GetMondayOfCurrentWeek(d) {
    var day = d.getDay();
    return new Date(d.getFullYear(), d.getMonth(), d.getDate() + (day == 0 ? -6 : 1) - day);
}

function GetSundayOfCurrentWeek(d) {
    var day = d.getDay();
    return new Date(d.getFullYear(), d.getMonth(), d.getDate() + (day == 0 ? 0 : 7) - day);
}

function GetFirstDayOfCurrentMonth(d) {
    return new Date(d.getFullYear(), d.getMonth(), 1);
}

function GetLastDayOfCurrentMonth(d) {
    return new Date(d.getFullYear(), d.getMonth() + 1, 0);
}

function GetCurrentQuarterStartEndDates(currentDate, financialYearStartDate, financialYearEndDate) {
    var currentQuarterStartEndDates = [];

    var startYear = financialYearStartDate.getFullYear();
    var endYear = financialYearEndDate.getFullYear();

    firstQuarterStartDate = new Date(startYear, 3, 1);
    firstQuarterEndDate = new Date(startYear, 5, 30);

    secondQuarterStartDate = new Date(startYear, 6, 1);
    secondQuarterEndDate = new Date(startYear, 8, 30);

    thirdQuarterStartDate = new Date(startYear, 9, 1);
    thirdQuarterEndDate = new Date(startYear, 11, 31);

    fourthQuarterStartDate = new Date(endYear, 0, 1);
    fourthQuarterEndDate = new Date(endYear, 2, 31);

    //if (new Date(currentJavaScriptDate.getFullYear(), currentJavaScriptDate.getMonth(), 1) >= new Date(startYear, 3, 1) && new Date(currentJavaScriptDate.getFullYear(), currentJavaScriptDate.getMonth(), 1) <= new Date(startYear, 5, 30))
    if (currentDate.setHours(0, 0, 0, 0) >= firstQuarterStartDate.setHours(0, 0, 0, 0) && currentDate.setHours(0, 0, 0, 0) <= firstQuarterEndDate.setHours(0, 0, 0, 0)) {
        currentQuarterStartEndDates.push(firstQuarterStartDate);
        currentQuarterStartEndDates.push(firstQuarterEndDate);

        return currentQuarterStartEndDates;
    }
    if (currentDate.setHours(0, 0, 0, 0) >= secondQuarterStartDate.setHours(0, 0, 0, 0) && currentDate.setHours(0, 0, 0, 0) <= secondQuarterEndDate.setHours(0, 0, 0, 0)) {
        currentQuarterStartEndDates.push(secondQuarterStartDate);
        currentQuarterStartEndDates.push(secondQuarterEndDate);

        return currentQuarterStartEndDates;
    }
    if (currentDate.setHours(0, 0, 0, 0) >= thirdQuarterStartDate.setHours(0, 0, 0, 0) && currentDate.setHours(0, 0, 0, 0) <= thirdQuarterEndDate.setHours(0, 0, 0, 0)) {
        currentQuarterStartEndDates.push(thirdQuarterStartDate);
        currentQuarterStartEndDates.push(thirdQuarterEndDate);

        return currentQuarterStartEndDates;
    }
    if (currentDate.setHours(0, 0, 0, 0) >= fourthQuarterStartDate.setHours(0, 0, 0, 0) && currentDate.setHours(0, 0, 0, 0) <= fourthQuarterEndDate.setHours(0, 0, 0, 0)) {
        currentQuarterStartEndDates.push(fourthQuarterStartDate);
        currentQuarterStartEndDates.push(fourthQuarterEndDate);

        return currentQuarterStartEndDates;
    }
}

//This function will bind DateRangePicker with Textbox when textbox value gets changed.
function BindDateRangePicker(textBox) {
    $(textBox).daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        format: applicationDateFormat,
        calender_style: "picker_1"
    }, function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
    });
}

//This function will update TextBox value and also will trigger textbox change() event so that 
//DateRangePicker should display current textbox value.
function ResetDatePickerValue(textBoxId, dateValue) {
    $(textBoxId).attr('value', dateValue).trigger("change");
}


function TrimWordSpaces(e) {
    e.value = e.value.replace(/\s/g, '');
    return e.value;
}

//#
function ValidatePANNumber(inputId, placerId) {
    var regExp = /[a-zA-z]{5}\d{4}[a-zA-Z]{1}/;
    var txtpan = $('#' + inputId).val();
    if (txtpan.length > 0) {

        if (txtpan.length == 10) {
            if (txtpan.match(regExp)) {
                //alert('PAN match found');
                HideCustomError(inputId, placerId);
            }
            else {
                //alert('Not a valid PAN number');
                ShowCustomError(inputId, placerId, 'Invalid PAN Number.');
                //event.preventDefault();
            }
        }
        else {
            //alert('Please enter 10 digits for a valid PAN number');
            ShowCustomError(inputId, placerId, 'Please enter 10 digits for a valid PAN number.');
            //event.preventDefault();
        }
    }
    else {
        HideCustomError(inputId, placerId);
    }
}

function ValidateAadhaarNumber(inputId, placerId) {
    var regExp = "^[0-9]{10,12}$";
    var txtaadhaar = $('#' + inputId).val();
    if (txtaadhaar.length > 0) {
        if (txtaadhaar.length == 12) {
            if (txtaadhaar.match(regExp)) {
                HideCustomError(inputId, placerId);
            }
            else {
                ShowCustomError(inputId, placerId, 'Invalid Aadhaar Number.');
            }
        }
        else {
            ShowCustomError(inputId, placerId, 'Please enter 12 digits for a valid Aadhaar number.');
        }
    }
    else {
        HideCustomError(inputId, placerId);
    }
}

function ShowCustomError(inputId, placerId, errorMessage) {
    if ($('#' + placerId).find("ul").length > 0) {
        $('#' + placerId).find("ul").remove();
    }

    var ulTag = "";
    ulTag = ulTag + ' <ul class=\'parsley-errors-list filled\'>';
    ulTag = ulTag + '   <li class=\'parsley-required\'>';
    ulTag = ulTag + '       ' + errorMessage;
    ulTag = ulTag + '   </li>';
    ulTag = ulTag + ' </ul>';

    $('#' + placerId).append(ulTag);

    $('#' + inputId).addClass("parsley-error");
    $('#' + inputId).removeClass("parsley-success");
}

function HideCustomError(inputId, placerId) {
    if ($('#' + placerId).find("ul").length > 0) {
        $('#' + placerId).find("ul").remove();
    }

    $('#' + inputId).addClass("parsley-success");
    $('#' + inputId).removeClass("parsley-error");
}

function GetProductAutoCompleteList(txtProductName) {
    $(txtProductName).autocomplete({
        minLength: 1,
        source: function (request, response) {
            $.ajax({
                url: "/Base/ProductMasterAutoCompleteList",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }))
                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
}