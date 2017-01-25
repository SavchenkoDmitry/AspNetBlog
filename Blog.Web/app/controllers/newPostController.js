var App;
(function (App) {
    'use strict';
    var app = angular.module("blogApp");
    var NewPostController = (function () {
        function NewPostController($scope, $location, blogService) {
            var _this = this;
            this.$location = $location;
            this.blogService = blogService;
            $scope.ctrl = this;
            this.blogService.getThemes().then(function (response) { return _this.themes = response.data; }, function () { return alert('Error'); });
        }
        NewPostController.prototype.postPost = function () {
            var _this = this;
            var obj = { "Topic": this.selectedTheme, "Text": this.newPostText };
            this.blogService.postMessage(obj, "AddPost").then(function (response) {
                if (response.data) {
                    _this.$location.path('/');
                }
                else {
                    alert('Failed');
                }
            }, function () { return alert('Error'); });
        };
        NewPostController.id = "newPostController";
        return NewPostController;
    }());
    App.NewPostController = NewPostController;
    NewPostController.$inject = ["$scope", "$location", "blogService"];
    app.controller(NewPostController.id, NewPostController);
})(App || (App = {}));
