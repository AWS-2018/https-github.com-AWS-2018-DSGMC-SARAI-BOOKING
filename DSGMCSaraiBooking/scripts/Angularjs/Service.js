angular.module('app').service("LoginService", function ($http) {
    //initilize factory object.  
    var fact = {};  
    fact.getUserDetails = function (d) {  
        debugger;  
        return $http({  
            url: '/Home/Index',  
            method: 'POST',  
            data:JSON.stringify(d),  
            headers: { 'content-type': 'application/json' }  
        });  
    };  
    return fact;  
});

