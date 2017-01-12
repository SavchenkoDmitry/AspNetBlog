var blogApp = angular.module('blogApp', ['ngRoute', 'ui.select']);
blogApp.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {

   

    $routeProvider.when('/',
        {
            templateUrl: 'app/views/Content.html',
            controller: 'contentController'
        });
        $routeProvider.when('/contact',
            {
                templateUrl: 'app/views/Contact.html',
                controller: 'contentController'
            });
        $routeProvider.when('/newpost',
            {
                templateUrl: 'app/views/NewPost.html',
                controller: 'newPostController'
            });
        $routeProvider.when('/post/:id',
            {
                templateUrl: '/app/views/Post.html',
                controller: 'postController'
            });
            

        
        $routeProvider.otherwise({ redirectTo: '/' });
    }]);