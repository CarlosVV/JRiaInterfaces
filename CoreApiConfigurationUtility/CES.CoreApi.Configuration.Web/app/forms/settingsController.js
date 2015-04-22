serviceConfigApp.controller("SettingsController",
    ["$scope", "$window", "$location", "$routeParams", "DataService",
    function ($scope, $window, $location, $routeParams, dataService) {

        var initializeselectedSetting = function () {
            return { Id: 0, Name: '', Value: '', Description: '' };
        };

        $scope.selectedSetting = initializeselectedSetting();

        $scope.serviceName = $routeParams.name;

        var getSettings = function () {
            dataService.getSettings($routeParams.id)
                .then(function(response) {
                    $scope.settings = response.data;
                    $scope.editableSettings = angular.copy($scope.settings);
                }, function(response) {
                    $scope.hasFormError = true;
                    $scope.formErrors = response.statusText;
                });
        };

        getSettings();

        $scope.cancelForm = function () {
            $window.history.back();
        };

        $scope.resetForm = function () {
            $scope.$broadcast('hide-errors-event');
            $scope.selectedSetting = initializeselectedSetting();
            $scope.settings = angular.copy($scope.editableSettings);
        }

        $scope.saveForm = function () {

            $scope.$broadcast('show-errors-event');

            if ($scope.settingForm.$invalid)
                return;

            dataService.updateSetting($scope.selectedSetting)
                .then(
                    function () {
                        $window.history.back();
                    },
                    function (results) {
                        $scope.hasFormError = true;
                        $scope.formErrors = results.statusText;
                    });
        };

        $scope.settingSelected = function () {
            return $scope.selectedSetting != null &&
                $scope.selectedSetting.Id != null &&
                $scope.selectedSetting.Id != "0";
        }

    }]);
