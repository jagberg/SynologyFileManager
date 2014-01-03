var fileManagerControllers = angular.module('moviefileApp');

fileManagerControllers.controller('MovieGroupController', function ($scope, $routeParams, $location, movieFileService) {
    init();

    function init() {
        $scope.movieGroupFile = movieFileService.getMovieFiles($routeParams.movieGroupName);
        $scope.movieGroups = movieFileService.getMovieGroups();
        $scope.movieGroupDisplay = { showEdit: false };
        setDefaultMovieGroup($routeParams.movieGroupName);
    };

    function setDefaultMovieGroup(defaultMovieGroupName) {
        /* Option 1: Set the default to the first movie group object so its no longer undefined and then set the default value to the passed in parameter.
           You need to define movGroupName with an object of MovieGroup type. Once this is set you 
           can then set the default value of the select list.
        
        //$scope.movGroupName = { selected: $scope.movieGroups[0] };
        // Now that movGroupName isnt undefined you can set the default value.
        //$scope.movGroupName.selected.movieGroupName = $routeParams.movieGroupName;
        */

        /* Option 2: Loop through movie groups and set the default to the one passed into the controller */
        for (var i = 0; i < $scope.movieGroups.length; i++) {
            if ($scope.movieGroups[i].movieGroupName === defaultMovieGroupName) {
                $scope.movGroupName = { selected: $scope.movieGroups[i] };
            }
        }
    };

    $scope.addMovieGroup = function (newMovieGroupName) {
        var movieGroup = [];
        var movieFiles = [];

        movieGroup.movieGroupName = newMovieGroupName;
        movieGroup.isMovieGroup = true;
        movieGroup.location = "\\video\\" & newMovieGroupName;
        movieGroup.movieFiles = movieFiles;

        movieFileService.addMovieGroup(movieGroup);

        $scope.showMovieGroupEditor(false);

        setDefaultMovieGroup(newMovieGroupName);

        //changeView('/');
    }

    $scope.setGroupSelect = function () {
        $scope.movGroupName.selected.movieGroupName = $routeParams.movieGroupName;
    };

    $scope.filterMovieGroups = function (movieGroup) {
        var showMovieGroupsOnly = $scope.showMovieGroupsOnly;

        if (!showMovieGroupsOnly) {
            return true; // Display everything if this wasnt ticked.
        }

        if (movieGroup.isMovieGroup === showMovieGroupsOnly) {
            return true; // this will be listed in the results
        }

        return false; // otherwise it won't be within the results
    };

    function changeView(newLocation) {
        $location.path(newLocation);
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


    $scope.showMovieGroupEditor = function (showEdit) {
        $scope.movieGroupDisplay.showEdit = showEdit;
    };

});