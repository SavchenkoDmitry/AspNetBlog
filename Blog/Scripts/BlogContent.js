(function () {
    var blogApp = angular.module('BlogApp', []);

    blogApp.controller('ContentController', function ($scope, BlogService) {

        $scope.Posts = null;

        var result = BlogService.GetMessages("0").then(function (response) {
            $scope.Posts = response.data.content;
            $scope.ShowDetails = true;
        }, function () {
            alert('Error');
        });

        $scope.btnPostPost = function () {
            var obj = { "topic": $scope.newTopic, "text": $scope.newMessage }
            BlogService.PostMessage(obj, "AddPost").then(function (response) {
                if (response.data.success) {
                    $scope.Posts.unshift(response.data.content);
                    $scope.newTopic = "";
                    $scope.newMessage = "";
                }
                else {
                    alert('Failed');
                }
            }, function () {
                alert('Error4');
            });
        };

        $scope.btnPostComment = function (id) {
            var res = $.grep($scope.Posts, function (p) { return p.id == id; });
            var obj = { "id": id, "text": res[0].newCommend };
            BlogService.PostMessage(obj, "AddComment").then(function (response) {
                if (response.data.success) {                    
                    res[0].coments.push(response.data.content);
                    res[0].newCommend = "";
                }
                else {
                    alert('Failed');
                }
            }, function () {
                alert('Error2');
            });
        };

        $scope.btnDeleteComment = function (commentId, postId) {
            var obj = { "commentId": commentId }
            BlogService.PostMessageWithId(commentId, "DeleteComment").then(function (response) {
                console.log(response);
                if (response.data.success) {
                    var post = $.grep($scope.Posts, function (p) { return p.id == postId; });
                    for (var i = 0; i < post[0].coments.length; i++) {
                        if (post[0].coments[i].id == commentId)
                            post[0].coments.splice(i, 1);
                    }
                }
                else {
                    alert('Cant delete');
                }
            }, function () {
                alert('Error3');
            });
        };

        $scope.btnDeletePost = function (postId) {
            BlogService.PostMessageWithId(postId, "DeletePost").then(function (response) {
                console.log(response);
                if (response.data.success) {
                    for (var i = 0; i < $scope.Posts.length; i++) {
                        if ($scope.Posts[i].id == postId)
                            $scope.Posts.splice(i, 1);
                    }
                }
                else {
                    alert('Cant delete');
                }
            }, function () {
                alert('Error3');
            });
        };

        $scope.btnShowMore = function () {
            BlogService.GetMessages($scope.Posts.length).then(function (response) {
                Array.prototype.push.apply($scope.Posts, response.data.content);
            }, function () {
                alert('Error');
            });
        }


    })





    .factory("BlogService", function ($http) {
        var blogFactory = new Object();
        blogFactory.GetMessages = function (count) {
            return $http.get("/Home/GetPosts?count=" + count);
        }
        blogFactory.PostMessage = function (obj, method) {
            console.log(obj);
            return $http.post("/Home/" + method, obj);
        }
        blogFactory.PostMessageWithId = function (id, method) {
            return $http.post("/Home/" + method + "/" + id);
        }
        return blogFactory;
    });
})();

