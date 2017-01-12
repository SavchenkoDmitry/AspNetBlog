blogApp.controller('newPostController', function ($scope, blogService, $location) {
    
    var t = blogService.GetThemes().then(function (response) {
        $scope.themes = response.data;
    }, function () {
        alert('Error');
    });


    $scope.btnPostPost = function () {
        var obj = { "Topic": $scope.themes.selected, "Text": $scope.newMessage }
        blogService.PostMessage(obj, "AddPost").then(function (response) {
            if (response.data.success) {
                $location.path('/');
            }
            else {
                alert('Failed');
            }
        }, function () {
            alert('Error4');
        });
    };
})