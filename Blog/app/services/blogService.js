blogApp.service("blogService", function ($http) {
    var blogService = new Object();
    blogService.GetMessages = function (count) {
        return $http.get("/Home/GetPosts?count=" + count);
    }
    blogService.GetPostById = function (id) {
        return $http.get("/Home/GetPost?id=" + id);
    }
    blogService.PostMessage = function (obj, method) {
        console.log(obj);
        return $http.post("/Home/" + method, obj);
    }
    blogService.PostMessageWithId = function (id, method) {
        return $http.post("/Home/" + method + "/" + id);
    }
    blogService.GetThemes = function ()
    {
        return $http.get("/Home/GetThemes");
    }

    return blogService;
});