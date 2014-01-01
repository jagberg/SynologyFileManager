var app = angular.module('moviefileApp');

app.controller('MovieFileController', function ($scope, movieFileService) {
    init();

    function init() {
        $scope.movieGroups = movieFileService.getMovieGroups();
    };


});