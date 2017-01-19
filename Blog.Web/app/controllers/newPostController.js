var App;
(function (App) {
    'use strict';
    var app = angular.module("blogApp");
    var NewPostController = (function () {
        function NewPostController($scope, $location, blogService) {
            var _this = this;
            $scope.ctrl = this;
            this.location = $location;
            this.service = blogService;
            this.service.GetThemes().then(function (response) { return _this.themes = response.data; }, function () { return alert('Error'); });
        }
        NewPostController.prototype.PostPost = function () {
            var _this = this;
            var obj = { "Topic": this.selectedTheme, "Text": this.newPostText };
            this.service.PostMessage(obj, "AddPost").then(function (response) {
                if (response.data) {
                    _this.location.path('/');
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
