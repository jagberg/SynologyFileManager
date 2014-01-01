var app = angular.module('moviefileApp');

app.service('movieFileService', function () {
    this.getMovieGroups = function () {
        return movieGroups;
    };

    var movieGroups = [
        {
            movieGroupName: 'Dexter', isMovieGroup: true, location: '\\video\Dexter',
            movieFiles: [
                { fileName: 'Dexter S08E01', location: '\\video\Dexter', requiresMove: false },
                { fileName: 'Dexter S08E02', location: '\\\video\Dexter', requiresMove: false },
                { fileName: 'Dexter S08E03', location: '\\video\Dexter', requiresMove: false }
            ]
        },
        {
            movieGroupName: 'The Middle', isMovieGroup: true, location: '\\video\The Middle',
            movieFiles: [
                { fileName: 'The Middle S08E01 - SRX', location: '\\video\The Middle', requiresMove: false },
                { fileName: 'The Middle S08E02 - SRX', location: '\\video\The Middle', requiresMove: false }
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