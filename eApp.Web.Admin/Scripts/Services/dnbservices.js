var dnbservices = function ($http) {

    this.GetDataFromUrl = function (url) {
        
        return $http.get(url);

    };

}

dnbservices.$inject = ['$http'];