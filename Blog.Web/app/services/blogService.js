var App;
(function (App) {
    var app = angular.module("blogApp");
    var BlogService = (function () {
        function BlogService($http) {
            this.$http = $http;
            this.http = $http;
        }
        BlogService.prototype.getMessages = function (count) {
            return this.$http.get("/api/blog/GetPosts?count=" + count);
        };
        BlogService.prototype.getPostById = function (id) {
            return this.$http.get("/api/blog/GetPost?id=" + id);
        };
        BlogService.prototype.postMessage = function (obj, method) {
            return this.$http.post("/api/blog/" + method, obj);
        };
        BlogService.prototype.deleteMessageWithId = function (id, method) {
            return this.$http.delete("/api/blog/" + method + "/" + id);
        };
        BlogService.prototype.getThemes = function () {
            return this.$http.get("/api/blog/GetThemes");
        };
        BlogService.prototype.getUserStatus = function () {
            return this.$http.get("/api/blog/GetUserStatus");
        };
        BlogService.id = "blogService";
        return BlogService;
    }());
    App.BlogService = BlogService;
    app.service(BlogService.id, BlogService);
})(App || (App = {}));
