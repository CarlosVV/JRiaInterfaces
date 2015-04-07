serviceConfigApp.controller("ListController",
    ["$scope", "$location", "DataService",
    function ($scope, $location, dataService) {

        var initializeSelectedService = function() {
            return { Id: 0, Name: '' }
        };

        $scope.selectedService = initializeSelectedService();

        var getServices = function() {
            dataService.getServices()
                .then(
                    function(response) {
                        $scope.services = response.data;
                    }, function(response) {
                        $scope.hasFormError = true;
                        $scope.formErrors = response.statusText;
                    });
        };

        $scope.showNavigation = function() {
            return $scope.selectedService != null &&
                $scope.selectedService.Id != null &&
                $scope.selectedService.Id != "0";
        }

        $scope.resetForm = function () {
            $scope.$broadcast('hide-errors-event');
            $scope.selectedService = initializeSelectedService();
        }

        $scope.showPermisisonsForm = function () {
            $location.path('/permissionsForm/' + $scope.selectedService.Id);
        };

        $scope.showSettingsForm = function () {
            $location.path('/settingsForm/' + $scope.selectedService.Id + '/' + $scope.selectedService.Name);
        };

        getServices();
    }]);
