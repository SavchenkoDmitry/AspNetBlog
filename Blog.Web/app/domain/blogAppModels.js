var App;
(function (App) {
    var domain;
    (function (domain) {
        'use strict';
        var Comment = (function () {
            function Comment() {
            }
            return Comment;
        }());
        domain.Comment = Comment;
        var Post = (function () {
            function Post() {
            }
            return Post;
        }());
        domain.Post = Post;
        var PostReview = (function () {
            function PostReview() {
            }
            return PostReview;
        }());
        domain.PostReview = PostReview;
        var Roles = (function () {
            function Roles() {
            }
            return Roles;
        }());
        domain.Roles = Roles;
    })(domain = App.domain || (App.domain = {}));
})(App || (App = {}));
