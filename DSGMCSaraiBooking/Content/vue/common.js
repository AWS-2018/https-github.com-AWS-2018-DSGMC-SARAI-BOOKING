Vue.filter('balance', function (value) {
    
    if (value) {
        //Balance() is defined in CommonFunctions.js
        return Balance(value);
    }
    else if (value == 0) {
        return 'NIL';
    }
    
});

Vue.filter('dateformat', function (value) {
    if (value) {
        return moment(String(value)).format(applicationDateFormat)
    }
});
Vue.filter('amountFormat', function (value) {
    if (value == "") {
        value = 0;
    }
    return parseFloat(value).toFixed(applicationDigitAfterDecimalAmount);
});

Vue.directive('formatdate', {
    twoWay: true, // this transformation applies back to the vm
    bind: function (el) {
        if (el.value != null) {
            el.value = moment(String(el.value)).format(applicationDateFormat);
        }
    },
    update: function (el) {
        if (el.value != null) {
            el.value = moment(String(el.value)).format(applicationDateFormat);
        }
    }
});




var Child = {
    props: ['pager'],
    template: "<div class=\"row\" v-if='pager'>" +
    "<div class=\"col-sm-5\">" +
    "<div class=\"dataTables_info\" id=\"datatable-responsive_info\" role=\"status\" aria-live=\"polite\">Showing {{ (((pager.CurrentPage - 1) * pager.PageSize) + 1)}} to {{ (((pager.CurrentPage) * pager.PageSize) > pager.TotalItems ? pager.TotalItems : ((pager.CurrentPage) * pager.PageSize)) }} of {{ pager.TotalItems }} Entries</div>" +
    "</div>" +
    "<div class=\"col-sm-7\">" +
    "<div class=\"dataTables_paginate paging_simple_numbers\" id=\"datatable-responsive_paginate\">" +
    '<template v-if="pager.TotalPages>1">' +
    '<ul class="pagination">' +
    '<li v-if="parseInt(pager.CurrentPage) > 1">' +
    '<a href="javascript:void(0)" data-page="1">First</a>' +
    '</li>' +
    '<li v-if="parseInt(pager.CurrentPage) > 1">' +
    '<a href="javascript:void(0)" v-bind:data-page="parseInt(pager.CurrentPage)-1">Previous</a>' +
    '</li>' +
    '<template v-for="n in getNumbers(pager.StartPage,pager.EndPage)">' +
    '<li v-if="n==parseInt(pager.CurrentPage)" class="active" ><a href="javascript:void(0)" v-bind:data-page="n">{{n}}<span class="sr-only">(current)</span></a></li>' +
    '<li v-if="n!=parseInt(pager.CurrentPage)" class=""><a href="javascript:void(0)" v-bind:data-page="n">{{n}}</a></li>' +
    '</template > ' +
    ' <li v-if="parseInt(pager.CurrentPage) < pager.TotalPages">' +
    '<a href="javascript:void(0)" v-bind:data-page="parseInt(pager.CurrentPage) + 1">Next</a>' +
    '</li>' +
    '<li v-if="parseInt(pager.CurrentPage) < pager.TotalPages" >' +
    '<a href="javascript:void(0)" v-bind:data-page="pager.TotalPages">Last</a>' +
    '</li>' +
    '</ul>' +
    '</template>' +
    "</div>" +
    "</div>" +
    "</div>",
    methods: {
        getNumbers: function (start, stop) {
            return new Array(stop - start).fill(start).map((n, i) => n + i);
        }
    }
}
