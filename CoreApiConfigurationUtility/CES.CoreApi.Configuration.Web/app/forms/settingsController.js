serviceConfigApp.controller("SettingsController",
[
    "$scope", "$window", "$location", "$routeParams", "$timeout", "DataService",
    function($scope, $window, $location, $routeParams, $timeout, dataService) {

        var initializeselectedSetting = function() {
            return { Id: 0, Name: '', Value: '', Description: '' };
        };

        $scope.selectedSetting = initializeselectedSetting();

        $scope.serviceName = $routeParams.name;

        var getSettings = function() {
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

        $scope.cancelForm = function() {
            $window.history.back();
        };

        $scope.resetForm = function() {
            $scope.$broadcast('hide-errors-event');
            $scope.selectedSetting = initializeselectedSetting();
            $scope.settings = angular.copy($scope.editableSettings);
        };

        $scope.saveForm = function() {
            $scope.isSaveMode = true;
            $scope.$broadcast('show-errors-event');

            if ($scope.settingForm.$invalid) {
                $scope.isSaveMode = false;
                return;
            }

            dataService.updateSetting($scope.selectedSetting)
                .then(
                    function(results) {
                        $scope.showSuccessAlert = true;
                        $scope.successTextMessage = results.data;
                        $timeout(hideMessageAndReturn, 1000);

                    },
                    function(results) {
                        $scope.hasFormError = true;
                        $scope.formErrors = results.statusText;
                    });

            var hideMessageAndReturn = function() {
                $scope.showSuccessAlert = false;
                $scope.successTextMessage = '';
                $window.history.back();
            };
        };

        $scope.settingSelected = function() {
            return $scope.selectedSetting != null &&
                $scope.selectedSetting.Id != null &&
                $scope.selectedSetting.Id != "0";
        };
    }
]);
