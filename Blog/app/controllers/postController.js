blogApp.controller('postController', function ($scope, blogService, $location, $routeParams) {
    $scope.post = null;

    var id = $routeParams["id"];
    if (id !== 'undefined') {
        var res = blogService.GetPostById(id).then(function (response) {
            $scope.post = response.data.Content;
            $scope.IsAdmin = response.data.IsAdmin;
            $scope.IsUser = response.data.IsUser;
        }, function () {
            alert('Error');
        });
    }
    else
    {
        $location.path('/');
    }


    $scope.btnPostComment = function (Id) {
        var obj = { "Id": Id, "Text": $scope.post.newCommend };
        blogService.PostMessage(obj, "AddComment").then(function (response) {
            if (response.data.success) {
                $scope.post.Coments.push(response.data.Content);
                $scope.post.newCommend = "";
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
        blogService.PostMessageWithId(commentId, "DeleteComment").then(function (response) {
            if (response.data.success) {
                for (var i = 0; i < $scope.post.Coments.length; i++) {
                    if ($scope.post.Coments[i].Id == commentId)
                        $scope.post.Coments.splice(i, 1);
                }
            }
            else {
                alert('Cant delete');
            }
        }, function () {
            alert('Error3');
        });
    };

})