var fileManagerControllers = angular.module('moviefileApp');

fileManagerControllers.controller('MovieGroupController', function ($scope, movieFileService) {
    init();

    function init() {
        $scope.movieGroups = movieFileService.getMovieGroups();
    };


});

fileManagerControllers.controller('MovieFileController', function ($scope, $routeParams, $location, movieFileService) {
    init();

    function init() {
        $scope.movieGroupFile = movieFileService.getMovieFiles($routeParams.movieGroupName);
        $scope.movieGroups = movieFileService.getMovieGroups();
    };

    $scope.assignMovieGroup = function () {
        var newMovieGroupName = $scope.movGroupName.selected.movieGroupName;
        var oldMovieGroupName = $scope.movieGroupFile.movieGroupName;
        var movieGroupFiles = $scope.movieGroupFile.movieFiles;

        movieFileService.reassignMovies(movieGroupFiles, oldMovieGroupName, newMovieGroupName);

        changeView('/');
    };

    function changeView(newLocation) {
        $location.path(newLocation);
    };

    //$scope.assignMovieGroup = function () {
    //    var selectedMovieGroup = $scope.movGroupName.selected;

    //    for (var i = 0; i < $scope.movieGroupFile.movieFiles.length; i++) {
    //        if ($scope.movieGroupFile.movieFiles[i].requiresMove === true) {
    //            var removeMovieFile = $scope.movieGroupFile.movieFiles[i];
    //            var oldMovieGroupName = $scope.movieGroupFile.movieGroupName;

    //            movieFileService.removeMovie(oldMovieGroupName, removeMovieFile);
    //            //movieFileService.assignMovieGroup($scope.movieGroupFile, $scope.movieGroup.movieGroupName
                 
    //            //assign to group;
    //        }
    //    }
    //};

    //$scope.assignMovieGroup = function () {
    //    var selectedMovieGroup = $scope.movGroupName.selected;

    //    for (var i = 0; i < $scope.movieGroupFile.movieFiles.length; i++) {
    //        if ($scope.movieGroupFile.movieFiles[i].requiresMove === true) {
    //            var removeMovieFile = $scope.movieGroupFile.movieFiles[i];
    //            var oldMovieGroupName = $scope.movieGroupFile.movieGroupName;

    //            movieFileService.removeMovie(oldMovieGroupName, removeMovieFile);
    //            //movieFileService.assignMovieGroup($scope.movieGroupFile, $scope.movieGroup.movieGroupName

    //            //assign to group;
    //        }
    //    }
    //};

});