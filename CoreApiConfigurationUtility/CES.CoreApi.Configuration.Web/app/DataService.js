
serviceConfigApp.factory('DataService',
    ["$http",
    function ($http) {

        var getServices = function () {
            return $http.get("api/ServiceList/Get");
        };

        var getSettings = function (id) {
            return $http.get("api/Setting/Get",
            {
                params: {
                    'serviceId': id
                }
            });
        };

        var updateSetting = function(setting) {
            return $http.post("api/Setting/Post", setting);
        };

        //var getEmployee = function (id) {
        //    if (id == 123) {
        //        return {
        //            id: 123,
        //            fullName: "Milton Waddams",
        //            notes: "The ideal employee.  Just don't touch his red stapler.",
        //            department: "Administration",
        //            dateHired: "July 11 2014",
        //            breakTime: "July 11 2014 3:00 PM",
        //            perkCar: true,
        //            perkStock: false,
        //            perkSixWeeks: true,
        //            payrollType: "none"
        //        };
        //    }
        //    return undefined;
        //};

        //var insertEmployee = function (newEmployee) {

        //    return $http.post("api/EmployeeWebApi/Post", newEmployee);
        //};

        //var updateEmployee = function (employee) {
        //    return $http.post("Employee/Update", employee);
        //};

        return {
            //insertEmployee: insertEmployee,
            updateSetting: updateSetting,
            getSettings: getSettings,
            getServices: getServices
        };
    }]);