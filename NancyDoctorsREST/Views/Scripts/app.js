var doctorsApp = angular.module('DoctorsApp', ['ngRoute']);

doctorsApp.config(function ($routeProvider) {
    $routeProvider
        .when('/Doctors',
        {
            controller: 'DoctorsController',
            templateUrl: 'Partials/List.html'
        })
        .when('/Doctor/:doctorId', 
        {
            controller: 'DoctorDetailsController',
            templateUrl: 'Partials/DoctorDetails.html'
        })
        .when('/CreateDoctor',
        {
            controller: 'DoctorsController',
            templateUrl: 'Partials/CreateDoctor.html'
        })
        .when('/TimeSlot/:timeslotId',
        {
            controller: 'TimeSlotController',
            templateUrl: 'Partials/TimeSlot.html'
        })
        .otherwise({ redirectTo: '/Doctors' });
});

doctorsApp
    .controller('DoctorsController', DoctorsController)
    .controller('DoctorDetailsController', DoctorDetailsController)
    .controller('TimeSlotController', TimeSlotController);

function DoctorsController($scope, $http) {
    $scope.doctors = [];

    $http.get("/DoctorsJSON").success(function (response) {

        for (var i = 0; i < response.length; i++) {
            $scope.doctors.push(
            {
                Id: response[i].Id,
                FirstName: response[i].FirstName,
                LastName: response[i].LastName,
                City: response[i].City,
                Specialization: response[i].Specialization
            });
        }
    });

    //$scope.addDoctor = function () {
    //    $scope.doctors.push(
    //        {
    //            FirstName: $scope.newDoctor.FirstName,
    //            LastName: $scope.newDoctor.LastName,
    //            City: $scope.newDoctor.City,
    //            Specialization: $scope.newDoctor.Specialization
    //        });
    //};
    
    $scope.predicate = 'City';
    $scope.reverse = false;
    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };
}

function DoctorDetailsController($scope, $http, $routeParams, $route) {

    $http.get("/DoctorJSON/" + $routeParams.doctorId)
    .success(function (response) {
        $scope.doctor = response;
    });

    $scope.addComment = function () {
        var req = {
            method: 'POST',
            url: "/DoctorJSON/" + $scope.doctor.Id,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: $.param(
                {
                    Author: $scope.newComment.Author,
                    Content: $scope.newComment.Content,
                })
        }
        $http(req).then(function (response) {
            $http.get("/DoctorJSON/" + $routeParams.doctorId)
            .success(function (response) {
                $scope.doctor = response;
                $scope.newComment.Author = '';
                $scope.newComment.Content = '';
            });
        },
        function (response) {
            //error
        });
    }
}

function TimeSlotController($scope, $http, $routeParams, $location) {

    $scope.bookTimesSlot = function () {
        var req = {
            method: 'POST',
            url: "/TimeSlotJSON/" + $scope.timeslot.Id,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: $.param({Visitor: $scope.timeslot.Visitor})
        }
        $http(req).then(function (response) {
            //success
            $location.path("/Doctor/" + $scope.timeslot.DoctorId)
        }, 
        function (response) {
            //error
        });
    };

    $http.get("/TimeSlotJSON/" + $routeParams.timeslotId)
    .success(function (response) {
        $scope.timeslot = response;
    });


}