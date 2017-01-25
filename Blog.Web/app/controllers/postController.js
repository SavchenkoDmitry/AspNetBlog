var App;
(function (App) {
    'use strict';
    var app = angular.module("blogApp");
    var PostController = (function () {
        function PostController($scope, $location, blogService, $routeParams) {
            var _this = this;
            this.blogService = blogService;
            $scope.ctrl = this;
            this.postId = $routeParams.id;
            if (this.postId !== 'undefined') {
                blogService.getPostById(this.postId).then(function (response) { return _this.post = response.data; }, function () { return alert('Error'); });
            }
            else {
                $location.path('/');
            }
            this.blogService.getUserStatus().then(function (response) { return _this.roles = response.data; }, function () { return alert('Error'); });
        }
        PostController.prototype.postComment = function () {
            var _this = this;
            var obj = { "Id": this.post.Id, "Text": this.newComentText };
            this.blogService.postMessage(obj, "AddComment").then(function (response) {
                var responseData = response.data;
                if (responseData.success) {
                    _this.post.Coments.push(responseData.Content);
                    _this.newComentText = "";
                }
                else {
                    alert('Failed');
                }
            }, function () { return alert('Error'); });
        };
        PostController.prototype.deleteComment = function (commentId) {
            var _this = this;
            this.blogService.deleteMessageWithId(commentId, "DeleteComment").then(function (response) {
                if (response.data) {
                    for (var i = 0; i < _this.post.Coments.length; i++) {
                        if (_this.post.Coments[i].Id == commentId)
                            _this.post.Coments.splice(i, 1);
                    }
                }
                else {
                    alert('Cant delete');
                }
            }, function () {
                alert('Error3');
            });
        };
        ;
        PostController.id = "postController";
        return PostController;
    }());
    App.PostController = PostController;
    PostController.$inject = ["$scope", "$location", "blogService", "$routeParams"];
    app.controller(PostController.id, PostController);
})(App || (App = {}));
