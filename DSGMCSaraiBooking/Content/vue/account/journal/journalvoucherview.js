

var model = [];

var debitVouchers = [];
var creditVouchers = [];
var debitTotal = 0;
var creditTotal = 0;
function initialization(object) {
    
    model = JSON.parse(object);
    debitVouchers = model.DebitVoucher;
    creditVouchers = model.CreditVoucher;
    debitTotal = _.reduce(debitVouchers, function (meno, obj) { return meno + obj.Amount }, 0);
    creditTotal = _.reduce(creditVouchers, function (meno, obj) { return meno + obj.Amount }, 0);
    model.DebitVoucher = null;
    model.CreditVoucher = null;

    console.log(debitTotal);

    Vue.component('v-select', VueSelect.VueSelect)

    var vm = new Vue({
        el: "#account-journal-entry-view",
        data: {
            title: "Journal Voucher",
            row: [],
            Journal: model,
            DebitVouchers: debitVouchers,
            CreditVouchers: creditVouchers,
            DebitTotal: debitTotal,
            CreditTotal: creditTotal,
            IsEnable: true,
            IsProgress: false
        },
        methods: {
            //For Print
            print: function () {
                ShowBootBoxPrintConfirmationMessage('Hello');
            },
            //Updating Debit total amount on amount change in debit grid
            debitAmountChange: function (rowNo, e) {
                var sum = 0;
                $.each(vm.DebitVouchers, function (index, value) {
                    if (rowNo != index) {
                        value.Amount = parseFloat(value.Amount).toFixed(2);
                        sum += parseFloat(value.Amount);
                    }


                    else {
                        sum += parseFloat(value.Amount);
                    }
                });
                vm.DebitTotal = sum;
                vm.IsEnable = !((vm.DebitTotal == 0 ? 1 : vm.DebitTotal) == (vm.CreditTotal == 0 ? 2 : vm.CreditTotal));
            },
            //Updating Credit total amount on amount change in credit grid
            creditAmountChange: function (rowNo, e) {
                var sum = 0;
                $.each(vm.CreditVouchers, function (index, value) {
                    if (rowNo != index) {
                        value.Amount = parseFloat(value.Amount).toFixed(2);
                        sum += parseFloat(value.Amount);
                    }


                    else {
                        sum += parseFloat(value.Amount);
                    }
                });
                vm.CreditTotal = sum;
                vm.IsEnable = !((vm.DebitTotal == 0 ? 1 : vm.DebitTotal) == (vm.CreditTotal == 0 ? 2 : vm.CreditTotal));
            }
        }
    });


    $(document).ready(function () {
        var alertMessage = applicationChangeConfirmationMessage;
        var oldDebitTotal = debitTotal;
        var oldCreditTotal = creditTotal;
        $('#EntryDate').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            maxDate: new Date(),
            format: applicationDateFormat,
            calender_style: "picker_1"
        }, function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
        });

        $('.closeModal').on('click', function () {
            $('#divAccountJournalEntryViewModalPopUp').modal('hide');
        });
    });

    //Close Confirmation Handler
    function printConfirm(obj) {

        if (obj === true) {
            alert('Printing............')
            //        $.post("/Account/Journal/Print", { "obj": journalObj }, function (response) {
            //            if (response.Succes) {

            //            });

            //    }
            //    else {
            //    }
            //    vm.IsEnable = false;
            //}).fail(function () {
            //    alert("error");
            //}).always(function () {
            //    vm.IsEnable = false;
            //}) 
            return true;
        }
        else {
            $('#divAccountJournalEntryModalPopUp').modal('show');
            return false;
        }
    }
}