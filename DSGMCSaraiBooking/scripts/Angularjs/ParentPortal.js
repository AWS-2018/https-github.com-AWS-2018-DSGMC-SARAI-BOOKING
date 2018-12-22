var app = angular.module('appParentPortal', []);
app.controller('controllerParentPortal', function ($scope, $http) {
    GetInstituteListByCompanyId();

    function GetInstituteListByCompanyId() {
        $scope.InstituteList;
        var companyId = parseInt($("#hdnCompanyId").val());
        //alert(JSON.stringify({ companyId: companyId }));
        $http({
            method: 'Post',
            url: '/Login/GetInstituteListByCompanyId',
            data: JSON.stringify({ companyId: companyId })
        }).success(function (data, status, headers, config) {
            $scope.InstituteList = data;
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
        });
    }
});