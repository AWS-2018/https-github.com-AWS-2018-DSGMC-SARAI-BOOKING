angular.module('app').controller('LoginController', function ($scope, LoginService) {  
      
    //initilize user data object  
    $scope.LoginData = {  
        Email: '',  
        Password:''  
    }  
    $scope.msg = "";  
    $scope.Submited = false;  
    $scope.IsLoggedIn = false;  
    $scope.IsFormValid = false;  
      
    //Check whether the form is valid or not using $watch  
    $scope.$watch("myForm.$valid", function (TrueOrFalse) {  
        $scope.IsFormValid = TrueOrFalse;   //returns true if form valid  
    });  
      
    $scope.LoginForm = function () {  
        $scope.Submited = true;  
        if ($scope.IsFormValid) {  
            LoginService.getUserDetails($scope.UserModel).then(function (d) {  
                debugger;  
                if (d.data.Email != null) {  
                    debugger;  
                    $scope.IsLoggedIn = true;  
                    $scope.msg = "You successfully Loggedin Mr/Ms " +d.data.FullName;  
                }  
                else {  
                    ShowBootBoxErrorMessage("Invalid credentials buddy! try again");
                }  
            });  
        }  
    }  
})


