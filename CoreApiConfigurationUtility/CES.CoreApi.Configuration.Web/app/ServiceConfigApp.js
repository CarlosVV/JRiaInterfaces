var serviceConfigApp = angular.module('serviceConfigApp', ["ngRoute", "ui.bootstrap"]);

serviceConfigApp.config(["$routeProvider", "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider
            .when("/home", {
                templateUrl: "app/forms/listTemplate.html",
                controller: "ListController"
            })
            .when("/settingsForm/:id/:name", {
                templateUrl: "app/forms/settingsTemplate.html",
                controller: "SettingsController"
            })
            .when("/permissionsForm/:id/:name", {
                templateUrl: "app/forms/permissionsTemplate.html",
                controller: "permissionsController"
            })
            .otherwise({
                redirectTo: "/home"
            });

        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});
    }]);