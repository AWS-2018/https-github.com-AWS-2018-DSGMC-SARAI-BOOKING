//Register Helper For Paging
Handlebars.registerHelper('list', function (context, options) {
    if (parseInt(context.TotalPages) > 1) {

        var ret = "";
        ret += "<div class=\"row\">";
        ret += "<div class=\"col-sm-5\">";
        ret += "<div class=\"dataTables_info\" id=\"datatable-responsive_info\" role=\"status\" aria-live=\"polite\">Showing " + (((context.CurrentPage - 1) * context.PageSize) + 1) + " to " + (((context.CurrentPage) * context.PageSize) > context.TotalItems ? context.TotalItems : ((context.CurrentPage) * context.PageSize)) + " of " + context.TotalItems + " Entries</div>";
        ret += "</div>";

        ret += "<div class=\"col-sm-7\">";
        ret += "<div class=\"dataTables_paginate paging_simple_numbers\" id=\"datatable-responsive_paginate\">";
        ret += "<ul class=\"pagination\">";

        if (context.CurrentPage > 1) {
            ret = ret + "  <li>";
            ret = ret + " <a href='javascript:void(0)' data-page='1'>First</a>";
            ret = ret + " </li>";
            ret = ret + " <li>";
            ret = ret + " <a href='javascript:void(0)' data-page='" + (parseInt(context.CurrentPage) - 1) + "'>Previous</a>";
            ret = ret + " </li>";
        }
        for (var i = context.StartPage; i <= context.EndPage; i++) {
            ret = ret + "<li class=" + (i == context.CurrentPage ? 'active' : '') + ">";
            ret = ret + "<a href='javascript:void(0)' data-page='" + i + "'>" + i + "</a>";
            ret = ret + "</li>";
        }
        if (context.CurrentPage < context.TotalPages) {
            ret = ret + "  <li>";
            ret = ret + " <a href='javascript:void(0)' data-page='" + (parseInt(context.CurrentPage) + 1) + "'>Next</a>";
            ret = ret + " </li>";
            ret = ret + " <li>";
            ret = ret + " <a href='javascript:void(0)' data-page='" + context.TotalPages + "'>Last</a>";
            ret = ret + " </li>";
        }

        ret += "</ul>";
        ret += "</div>";
        ret += "</div>";
        ret += "</div>";

        return ret;
    }
});


//var Child = {
//    props: ['pager'],
//    template: '<nav aria-label="Page navigation example"><div v-if="pager.TotalPages>1">' +
//    '<ul class="pagination">' +
//    '<li v-if="pager.CurrentPage > 1" class="page-item">' +
//    '<button class="page-link" v-on:click="LoadData(1)">First</button>' +
//    '</li>' +
//    '<li v-if="pager.CurrentPage > 1" class="page-item">' +
//    '<button class="page-link" v-on:click="LoadData(pager.CurrentPage-1)">Previous</button>' +
//    '</li>' +
//    '<div v-for="n in getNumbers(pager.StartPage,pager.EndPage)">' +
//    '<li v-if="n==pager.CurrentPage" class="page-item active"><span class="page-link">{{n}}<span class="sr-only">(current)</span></span></li>' +
//    '<li v-if="n!=pager.CurrentPage" class="page-item"><button class="page-link" v-on:click="LoadData(n)">{{n}}</button></li>' +
//    '</div > ' +
//    ' <li v-if="pager.CurrentPage < pager.TotalPages" class="page-item">' +
//    '<button class="page-link" v-on:click="LoadData(pager.CurrentPage + 1)">Next</button>' +
//    '</li>' +
//    '<li v-if="pager.CurrentPage < pager.TotalPages" class="page-item">' +
//    '<button class="page-link" v-on:click="LoadData(pager.TotalPages)">Last</button>' +
//    '</li>' +
//    '</ul>' +
//    '</div>' +
//    '</nav>',
//    methods: {
//        getNumbers: function (start, stop) {
//            return new Array(stop - start).fill(start).map((n, i) => n + i);
//        },
//        LoadData: function (page) {
//            vm.pageNo = page;
//            vm.LoadData(vm.Title, vm.pageNo);

//        },
//        PageSizeChange: function () {


//        }
//    }
//}