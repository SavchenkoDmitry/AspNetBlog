var App;
(function (App) {
    'use strict';
    var app = angular.module("blogApp");
    var ContentController = (function () {
        function ContentController($scope, $location, blogService) {
            var _this = this;
            $scope.ctrl = this;
            this.location = $location;
            this.service = blogService;
            this.service.GetMessages(0).then(function (response) { return _this.posts = response.data; }, function () { return alert('Error'); });
            this.service.GetThemes().then(function (response) { return _this.themes = response.data; }, function () { return alert('Error'); });
            this.service.GetUserStatus().then(function (response) { return _this.roles = response.data; }, function () { return alert('Error'); });
        }
        ContentController.prototype.ShowMore = function () {
            var _this = this;
            this.service.GetMessages(this.posts.length).then(function (response) { return Array.prototype.push.apply(_this.posts, response.data); }, function () { return alert('Error'); });
        };
        ContentController.prototype.GetTextPreview = function (text) {
            if (text.length > 30) {
                return text.slice(0, 27) + "...";
            }
            else {
                return text;
            }
        };
        ContentController.prototype.ClearFilter = function () {
            this.filterText = "";
            this.selectedTheme = "";
        };
        ;
        ContentController.prototype.CreateNewPost = function () {
            this.location.path('/newpost');
        };
        ;
        ContentController.prototype.PostDetails = function (id) {
            this.location.path('/post/' + id);
        };
        ;
        ContentController.prototype.DeletePost = function (postId) {
            var _this = this;
            this.service.PostMessageWithId(postId, "DeletePost").then(function (response) {
                for (var i = 0; i < _this.posts.length; i++) {
                    if (_this.posts[i].Id == postId)
                        _this.posts.splice(i, 1);
                }
            }, function () {
                alert('Error');
            });
        };
        ;
        ContentController.id = "contentController";
        return ContentController;
    }());
    App.ContentController = ContentController;
    ContentController.$inject = ["$scope", "$location", "blogService"];
    app.controller(ContentController.id, ContentController);
})(App || (App = {}));
