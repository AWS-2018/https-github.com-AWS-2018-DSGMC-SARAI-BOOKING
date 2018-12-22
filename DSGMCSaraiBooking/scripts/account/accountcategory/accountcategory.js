// Onload Table Bind
var vm = new Vue({
    el: "#app",
    data: {
        Title: "Hello Vue",
        dataList: null,
        pager: null,
    },
    methods: {
        search: function () {
            var name = $("#Name").val();
            var saraiMasterId = $("#SaraiMasterId").val();
            $.get('/roommaster/GetroomList', { 'name': name, 'saraiMasterId': saraiMasterId, 'pageNumber': 1, 'pageSize': 10 }, function (data) {
                vm.dataList = data;
            });
        },
        reset: function () {
            $('#Name').val('');
            $('#CategoryType').val('N');
            vm.loadData();
        },
        loadData: function () {
            $.get('/RoomMaster/GetRoomList', { 'pageNumber': 1, 'pageSize': 10 }, function (data) {
                vm.dataList = data;
            });
        }
    }
    //,
    //components: {
    //    'my-pagination': Child
    //}
});

function LoadDataInTable(pageNumber, menuId) {
    $('#alert').html('');
    var name = $('#Name').val();
    var categoryType = $('#CategoryType').val();
    
    $.get('/AccountCategory/GetAccountCategoryList', { 'name': name, 'categoryType': categoryType, 'pageNumber': parseInt(pageNumber), 'MenuId': parseInt(menuId) }, function (data) {
    
        vm.dataList = data;
        //$('#table').html(templateResult);
        if (!data.success) {
            $('#empty-row').text(data.message);
        }
    });
}

// Onload Table Bind
$(document).ready(function () {
    var pageNumber = $('#pageNumber').val();
    var menuId = $("#hdnMenuId").val();   
    alert(pageNumber);
    LoadDataInTable(pageNumber, menuId);
});



$(document).on('click', 'table > tbody > tr > td > .disable', function () {
    var token = $(this).attr('data-token');
    EnableDisableRecord("disable", token, "/AccountCategory/DisableRecord")
});

$(document).on('click', 'table > tbody > tr > td > .enable', function () {
    var token = $(this).attr('data-token');
    EnableDisableRecord("enable", token, "/AccountCategory/EnableRecord")
});


// Paging Click Table Bind
$(document).on('click', 'li > a', function () {
    var pageNumber = $(this).attr('data-page');
    var menuId = $("#hdnMenuId").val();

    LoadDataInTable(pageNumber, menuId);
});

// Search
$(document).on('click', '#btn-search', function () {
    var pageNumber = 1;
    var menuId = $("#hdnMenuId").val();

    LoadDataInTable(pageNumber, menuId);
});

$(document).on('click', '#btn-reset', function () {
    var pageNumber = 1;
    var menuId = $("#hdnMenuId").val();
    $('#Name').val('');
    $('#CategoryType').val('N');

    LoadDataInTable(pageNumber, menuId);
});
