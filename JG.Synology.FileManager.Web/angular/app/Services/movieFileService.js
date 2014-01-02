var app = angular.module('moviefileApp');

app.service('movieFileService', function () {
    this.getMovieGroups = function () {
        return movieGroups;
    };

    this.getMovieFiles = function (movieGroupName) {
        for (var i = 0; i < movieGroups.length; i++) {
            if (movieGroups[i].movieGroupName === movieGroupName) {
                return movieGroups[i];
            }
        }
        return movieGroups[0];
    };

    this.reassignMovies = function (reassignedMovieFiles, oldMovieGroupName, newMovieGroupName) {
        var newMovieFileArray = [];
        var currentMovieGroupName = reassignedMovieFiles.movieGroupName;

        // Copy movie files to new array which dont need to be moved
        for (var i = 0; i < reassignedMovieFiles.length; i++) {

            if (reassignedMovieFiles[i].requiresMove === false) {
                newMovieFileArray.push(reassignedMovieFiles[i]);
            }
            else {
                // Add movie files that need to be reassigned to the new movie group
                this.addMovieFile(newMovieGroupName, reassignedMovieFiles[i]);
            }
        }

        this.replaceMovieGroupFiles(newMovieFileArray, oldMovieGroupName);
    };

    // Replace movie files in group with a new set of movie files.
    this.replaceMovieGroupFiles = function (newMovieFileArray, movieGroupName) {
        for (var i = 0; i < movieGroups.length; i++) {
            if (movieGroups[i].movieGroupName === movieGroupName) {
                movieGroups[i].movieFiles = newMovieFileArray;
            }
        }
    };

    // Add movie file to new group
    this.addMovieFile = function (newMovieGroupName, reassignedMovieFile) {
        for (var i = 0; i < movieGroups.length; i++) {
            if (movieGroups[i].movieGroupName === newMovieGroupName) {
                movieGroups[i].movieFiles.push(reassignedMovieFile);
            }
        }
    };

    this.removeMovie = function (movieGroupName, reassignedMovieFiles) {
        for (var i = 0; i < movieGroups.length; i++) {
            // Check if the movie group name matches the one that the file is assigned to.
            if (movieGroups[i].movieGroupName === movieGroupName) {
                for (var j = 0; j < movieGroups[i].movieFiles.length; j++) {
                    // Go through each movie file in the group and remove the ones the user wants
                    if (movieGroups[i].movieFiles[j].fileName === reassignedMovieFiles.fileName) {
                        movieGroups[i].movieFiles.splice(j, 1);
                    }
                }
            }
        }
    };

    this.addMovieGroup = function (newMovieGroup) {
        movieGroups.push(newMovieGroup);
    }

    var movieGroups = [
        {
            movieGroupName: 'Dexter', isMovieGroup: true, location: '\\video\Dexter',
            movieFiles: [
                { fileName: 'Dexter S08E01', location: '\\video\Dexter', requiresMove: false },
                { fileName: 'Dexter S08E02', location: '\\\video\Dexter', requiresMove: true },
                { fileName: 'Dexter S08E03', location: '\\video\Dexter', requiresMove: false }
            ]
        },
        {
            movieGroupName: 'The Middle', isMovieGroup: true, location: '\\video\The Middle',
            movieFiles: [
                { fileName: 'The Middle S08E01 - SRX', location: '\\video\The Middle', requiresMove: false },
                { fileName: 'The Middle S08E02 - SRX', location: '\\video\The Middle', requiresMove: true }
            ]
        },
        {
            movieGroupName: 'The Middle S02E11', isMovieGroup: false, location: '\\video\The Middle S02E11',
            movieFiles: [
                { fileName: 'The Middle S02E11', location: '\\video\The Middle S02E11', requiresMove: false }
            ]
        },
        {
            movieGroupName: 'Unmatched', isMovieGroup: false, location: '\\',
            movieFiles: [
                { fileName: 'Awake S02E11', location: '\\video\Awake S02E11', requiresMove: false },
                { fileName: 'Awake S02E17', location: '\\video\Awake S02E17', requiresMove: false }
            ]
        }
    ];
});