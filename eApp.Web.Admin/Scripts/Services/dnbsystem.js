var dnbsystem = function ($http) {

    
    //eval issuance information
    this.getevalview = function (obj, objj) {

        return $http.get('/Evaluation/NewEvalView?empid=' + obj + "&vin=" + objj);

    };
}

dnbsystem.$inject = ['$http'];