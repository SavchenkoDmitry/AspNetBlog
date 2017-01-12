blogApp.controller('contentController', function ($scope, blogService, $location) {

    $scope.Posts = null;

    var result = blogService.GetMessages("0").then(function (response) {
        $scope.Posts = response.data.Previews;        
        $scope.IsAdmin = response.data.IsAdmin;
        $scope.IsUser = response.data.IsUser;
    }, function () {
        alert('Error');
    });

     var t = blogService.GetThemes().then(function (response) {
         $scope.themes = response.data;
    }, function () {
        alert('Error');
    });

    $scope.createNewPost = function () {
        $location.path('/newpost');
    };

    $scope.postDetails = function (id) {
        $location.path('/post/' + id);
    };
    
    $scope.getTextPreview = function (text) {
        if (text.length > 30)
        {
            return text.slice(0, 27) + "..."
        }
        else
        {
            return text;
        }
    }
    $scope.clearFilter = function () {
        $scope.member = "";
        $scope.themes.selected = "";
    };
    
    $scope.btnDeletePost = function (postId) {
        blogService.PostMessageWithId(postId, "DeletePost").then(function (response) {
            if (response.data.success) {
                for (var i = 0; i < $scope.Posts.length; i++) {
                    if ($scope.Posts[i].Id == postId)
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
        blogService.GetMessages($scope.Posts.length).then(function (response) {
            Array.prototype.push.apply($scope.Posts, response.data.Content);
        }, function () {
            alert('Error');
        });
    }


})