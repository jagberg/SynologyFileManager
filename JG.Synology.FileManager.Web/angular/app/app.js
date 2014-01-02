var app = angular.module('moviefileApp');

//This configures the routes and associates each route with a view and a controller
app.config(function ($routeProvider) {
    $routeProvider
        .when('/',
            {
                controller: 'MovieGroupController',
                templateUrl: '/angular/app/partials/movieGroups.html'
            })
        //Define a route that has a route parameter in it (:customerID)
        .when('/movieFile/:movieGroupName',
            {
                controller: 'MovieFileController',
                templateUrl: '/angular/app/partials/movieFile.html'
            })
        //Define a route that has a route parameter in it (:customerID)
        .when('/editMovieFile',
            {
                controller: 'MovieGroupController',
                templateUrl: '/angular/app/partials/editMovieFile.html'
            })
        ////Define a route that has a route parameter in it (:customerID)
        //.when('/orders',
        //    {
        //        controller: 'OrdersController',
        //        templateUrl: '/app/partials/orders.html'
        //    })
        .otherwise({ redirectTo: '/' });
});
