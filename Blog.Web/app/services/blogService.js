var App;
(function (App) {
    var app = angular.module("blogApp");
    var BlogService = (function () {
        function BlogService($http) {
            this.$http = $http;
            this.http = $http;
        }
        BlogService.prototype.GetMessages = function (count) {
            return this.$http.get("/Home/GetPosts?count=" + count);
        };
        BlogService.prototype.GetPostById = function (id) {
            return this.$http.get("/Home/GetPost?id=" + id);
        };
        BlogService.prototype.PostMessage = function (obj, method) {
            return this.$http.post("/Home/" + method, obj);
        };
        BlogService.prototype.PostMessageWithId = function (id, method) {
            return this.$http.post("/Home/" + method + "/" + id, "");
        };
        BlogService.prototype.GetThemes = function () {
            return this.$http.get("/Home/GetThemes");
        };
        BlogService.prototype.GetUserStatus = function () {
            return this.$http.get("/Home/GetUserStatus");
        };
        BlogService.id = "blogService";
        return BlogService;
    }());
    App.BlogService = BlogService;
    app.service(BlogService.id, BlogService);
})(App || (App = {}));
