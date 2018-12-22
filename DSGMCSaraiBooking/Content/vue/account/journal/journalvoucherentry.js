

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
        el: "#account-journal-entry",
        data: {
            title: "Journal Voucher",
            row: [],
            Journal: model,
            DebitVouchers: debitVouchers,
            CreditVouchers: creditVouchers,
            DebitTotal: debitTotal,
            CreditTotal: creditTotal,
            IsEnable: !((debitTotal == 0 ? 1 : debitTotal) == (creditTotal == 0 ? 2 : creditTotal)),
            Errors: [],
            IsProgress: false
        },
        computed: {
            //Get Account List
            accountList: function () {
                //In CommonFunction.js
                return AccountMasterList();
            }
        },
        methods: {
            //For saving record
            save: function (e) {

                vm.IsEnable = true;


                var vouchers = _.filter(vm.DebitVouchers.concat(vm.CreditVouchers), function (obj) {
                    return obj.AccountName != "" && obj.AccountName != null;
                });
                console.log(vouchers);
                var isDuplicate = checkDuplicateInObject('AccountName', vouchers);
                console.log(isDuplicate);
                if (isDuplicate) {
                    $('#divAccountJournalEntryAlert').html(ShowErrorMessage('Duplicate account cannot be acceptable.'));
                    vm.IsEnable = false;
                    return;
                }
                if (vm.IsEnable) {

                    var journalObj = vm.Journal;

                    journalObj.EntryDate = moment(journalObj.EntryDate).format(applicationDateFormat);
                    journalObj.DebitVoucher = vm.DebitVouchers;
                    journalObj.CreditVoucher = vm.CreditVouchers;


                    $.post("/Account/Journal/Create", { "obj": journalObj }, function (response) {
                        if (response.Succes) {
                            vm.Journal = response.data;
                            vm.DebitVouchers = response.data.DebitVoucher;
                            vm.CreditVouchers = response.data.CreditVoucher;
                            vm.CreditTotal = 0;
                            vm.DebitTotal = 0;
                            $('#divAccountJournalEntryAlert').html(ShowSuccessMessage(response.ResponseText));
                            $(".status-message").fadeTo(applicationMessageBoxFadeToTime, 0).slideUp(applicationMessageBoxSlideUpTime, function () {
                                ShowBootBoxPrintConfirmationMessage("Print Voucher");
                            });

                        }
                        else {
                            $('#divAccountJournalEntryAlert').html(ShowErrorMessage(response.ResponseText));
                        }
                        vm.IsEnable = false;
                    }).fail(function () {
                        alert("error");
                    }).always(function () {
                        vm.IsEnable = false;
                    })
                }
                else {
                    alert('Please check voucher amount.');
                }
            },
            //For Print
            print: function () {
                ShowBootBoxPrintConfirmationMessage('Hello');
            },
            //Add new row in Debit grid
            addDebitRecord: function () {
                var length = vm.DebitVouchers.length;
                if (vm.DebitVouchers[length - 1].Amount != 0 && vm.DebitVouchers[length - 1].AccountName != "") {
                    vm.DebitVouchers.push({
                        AccountMasterId: 0,
                        AccountName: null,
                        CRCValue: "",
                        CheckSumValue: 0,
                        CreateDate: "",
                        CreatedByUserId: 0,
                        CreatedByUserName: "",
                        DatabaseTableName: "",
                        EntryDate: "",
                        EntryType: null,
                        GUID: "fb897490-544a-4687-a6a1-e7ee189d0fbf",
                        HasPrinted: false,
                        Id: 0,
                        IsActive: true,
                        IsSelected: true,
                        KeyId: null,
                        ManualVoucherNo: null,
                        ModifiedByUserId: 0,
                        ModifiedByUserName: "",
                        ModifyDate: "",
                        RecordCount: 0,
                        Remarks: "",
                        RowVersion: 1,
                        SerialNumber: 0,
                        SourceModule: null,
                        SuperAdminName: "",
                        Token: "",
                        Amount: 0,
                        VoucherDescription: null,
                        VoucherNo: null
                    });

                    vm.$nextTick(function () {

                        let index = length;
                        let input = vm.$refs.debitIsSelected[index];
                        input.focus();
                    });

                }

            },
            //Remove row from debit grid
            removeDebitRecord: function () {
                var length = vm.DebitVouchers.length;
                if (length > 5) {

                    if (vm.DebitVouchers[length - 1].Amount == 0) {
                        vm.DebitVouchers.pop();
                        vm.debitAmountChange();
                    }
                    else {
                        if (confirm("Are you sure want to delete this record.")) {
                            vm.DebitVouchers.pop();
                            vm.debitAmountChange();
                        }
                    }
                }
            },
            //Add new row in credit grid
            addCreditRecord: function () {
                var length = vm.CreditVouchers.length;
                if (vm.CreditVouchers[length - 1].Amount != 0 && vm.CreditVouchers[length - 1].AccountName != "") {
                    vm.CreditVouchers.push({
                        AccountMasterId: 0,
                        AccountName: null,
                        CRCValue: "",
                        CheckSumValue: 0,
                        CreateDate: "",
                        CreatedByUserId: 0,
                        CreatedByUserName: "",
                        DatabaseTableName: "",
                        EntryDate: "",
                        EntryType: null,
                        GUID: "fb897490-544a-4687-a6a1-e7ee189d0fbf",
                        HasPrinted: false,
                        Id: 0,
                        IsActive: true,
                        IsSelected: true,
                        KeyId: null,
                        ManualVoucherNo: null,
                        ModifiedByUserId: 0,
                        ModifiedByUserName: "",
                        ModifyDate: "",
                        RecordCount: 0,
                        Remarks: "",
                        RowVersion: 1,
                        SerialNumber: 0,
                        SourceModule: null,
                        SuperAdminName: "",
                        Token: "",
                        Amount: 0,
                        VoucherDescription: null,
                        VoucherNo: null
                    });

                    vm.$nextTick(function () {
                        let index = length;
                        let input = vm.$refs.creditIsSelected[index];
                        input.focus();
                    });
                }
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
            },
            //Get Closing Balance By Account Name
            AccountBalance: function (obj, rowNo, type) {

                if (obj.AccountName != "") {
                    //Start Updating Description 2 in Debit and Credit
                    if ((vm.CreditVouchers[rowNo].VoucherDescription2 == null
                        || vm.CreditVouchers[rowNo].VoucherDescription2 == "")
                        && vm.DebitVouchers[rowNo].AccountName != ""
                        && vm.CreditVouchers[rowNo].AccountName != "") {
                        vm.CreditVouchers[rowNo].VoucherDescription2 = vm.DebitVouchers[rowNo].AccountName;
                    }

                    if ((vm.DebitVouchers[rowNo].VoucherDescription2 == null
                        || vm.DebitVouchers[rowNo].VoucherDescription2 == "")
                        && vm.CreditVouchers[rowNo].AccountName != ""
                        && vm.DebitVouchers[rowNo].AccountName != "") {
                        vm.DebitVouchers[rowNo].VoucherDescription2 = vm.CreditVouchers[rowNo].AccountName;
                    }
                    //End Updating Description 2 in Debit and Credit

                    // Start fetching Closing Balance from database
                    if (obj.AccountName != "" && obj.AccountName != null) {
                        var accountNames = [];
                        accountNames.push(obj.AccountName);
                        GetAccountClosingBalance(accountNames);
                    }
                    // End fetching Closing Balance from database



                }
            },
            //Updating Closing on Date change in Debit and Credit
            UpdateAccountBalance: function () {
                var accountNames = [];
                $.each(vm.CreditVouchers, function (index, Credit) {
                    if (Credit.AccountName != '') {
                        //console.log(obj.AccountName);
                        accountNames.push(Credit.AccountName);
                    }
                });
                $.each(vm.DebitVouchers, function (index, Debit) {
                    if (Debit.AccountName != '') {
                        accountNames.push(Debit.AccountName);
                        //vm.AccountBalance(Debit);
                    }
                });

                GetAccountClosingBalance(accountNames);
            }
        }
    });

    function GetAccountClosingBalance(accountNames) {
        var entryDate = moment(vm.Journal.EntryDate).format(applicationDateFormat);

        $.ajax({
            url: '/Base/GetAccountClosingBalance',
            type: 'POST',
            data: { 'date': entryDate, 'accountList': accountNames },// { data: ["value 1", "value 2", "value 3"] },
            dataType: "json",
            success: function (response) {

                $.each(vm.CreditVouchers, function (index, Credit) {
                    if (Credit.AccountName != '') {
                        let voucher = _.findWhere(response, { Name: Credit.AccountName })
                        if (voucher) {
                            vm.$set(Credit, 'AccountBalance', voucher.ClosingBalance);
                        }
                    }
                });
                $.each(vm.DebitVouchers, function (index, Debit) {
                    if (Debit.AccountName != '') {
                        let voucher = _.findWhere(response, { Name: Debit.AccountName })
                        if (voucher) {
                            vm.$set(Debit, 'AccountBalance', voucher.ClosingBalance);
                        }
                        //vm.AccountBalance(Debit);
                    }
                });
            },
            error: function (xhr) {
                alert(xhr);

            }
        });
    };
    //Close Confirmation Handler
    function printConfirm(obj) {

        if (obj === true) {
            alert('Printing............');
            return true;
        }
        else {
            $('#divAccountJournalEntryModalPopUp').modal('show');
            return false;
        }
    }
    //Close Confirmation Handler
    function confirm(obj) {

        if (obj === true) {
            $('#divAccountJournalEntryModalPopUp').modal('hide');
            return true;
        }
        else {
            $('#divAccountJournalEntryModalPopUp').modal('show');
            return false;
        }
    }
    $(document).ready(function () {
        vm.$refs.date.innerText = "Date";
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
            let dTotal = $('#debit-total').text();
            let cTotal = $('#credit-total').text();
            if (dTotal != oldDebitTotal || cTotal != oldCreditTotal) {
                ShowBootBoxConfirmationMessage(alertMessage);
            }
            else {
                $('#divAccountJournalEntryModalPopUp').modal('hide');
            }
        });
    });

  
}