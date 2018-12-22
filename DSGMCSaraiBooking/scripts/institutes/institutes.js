
function LoadDataInTable(pageNumber, menuId) {

    $('#alert').html('');

    var templateHtml = $('#table-list').html();
    var templateCompile = Handlebars.compile(templateHtml);

    $.get('/Institute/GetInstituteList', { 'pageNumber': parseInt(pageNumber), 'MenuId': parseInt(menuId) }, function (data) {

        var templateResult = templateCompile(data);
        $('#table').html(templateResult);
    });
}

// Onload Table Bind
$(document).ready(function () {
    var pageNumber = 1;
    var menuId = $("#hdnMenuId").val();    
    
    LoadDataInTable(pageNumber, menuId);
});



$(document).on('click', 'table > tbody > tr > td > .disable', function () {
    var a = confirm("Are you sure to disable this record?");
    if (a) {
        var token = $(this).attr('data-token');
        $.get('/Institute/DisableRecord', { "Token": token }, function (data) {
            if (data.Success == true) {
                var pageNumber = data.PageNumber;
                var menuId = $("#hdnMenuId").val();                
                LoadDataInTable(pageNumber, menuId);
                $('#alert').html(ShowSuccessMessage(data.ResponseText));
            }
            else {
                $('#alert').html(ShowErrorMessage(data.ResponseText));
            }
        });
    }
});

$(document).on('click', 'table > tbody > tr > td > .enable', function () {
    var a = confirm("Are you sure to enable this record?");
    if (a) {
        var token = $(this).attr('data-token');
        $.get('/Institute/EnableRecord', { "Token": token }, function (data) {
            if (data.Success == true) {
                var pageNumber = data.PageNumber;
                var menuId = $("#hdnMenuId").val();                
                LoadDataInTable(pageNumber, menuId);
                alert(ShowSuccessMessage(data.ResponseText));
                $('#alert').html(ShowSuccessMessage(data.ResponseText));
            }
            else {
                $('#alert').html(ShowErrorMessage(data.ResponseText));
            }
        });
    }
});


// Paging Click Table Bind
$(document).on('click', 'li > a', function () {
    var pageNumber = $(this).attr('data-page');
    var menuId = $("#hdnMenuId").val();
    
    LoadDataInTable(pageNumber, menuId);
});


