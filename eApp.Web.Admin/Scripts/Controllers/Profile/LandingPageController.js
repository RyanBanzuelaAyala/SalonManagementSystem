var LandingPageController = function ($scope, $templateCache, $rootScope) {
    
    $scope.models = {
        helloAngular: 'Bouthaina : Salon Management System'
    };
    
    $scope.logout = function () {
        console.log("log loaded")
        window.location = '/Account/LogOff';
    };

    $scope.iform = {     
        isViewLoading: false
    };

    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        //$templateCache.removeAll();
        if (typeof (current) !== "undefined") {
            console.log(current);
            $templateCache.remove(current.templateUrl);
        }
        //if (current.$$route && current.$$route.resolve) {
        //    // Show a loading message until promises aren't resolved
        //    $scope.iform.isViewLoading = true;
        //}
        console.log("loading");
        $scope.iform.isViewLoading = true;
    });
    $rootScope.$on('$routeChangeSuccess', function () {
        console.log("unloading");
        $scope.iform.isViewLoading = false;
    });
    $rootScope.$on('$routeChangeError', function () {
        console.log("error loading");
        $scope.iform.isViewLoading = false;
    });
}

LandingPageController.$inject = ['$scope', '$templateCache', '$rootScope'];