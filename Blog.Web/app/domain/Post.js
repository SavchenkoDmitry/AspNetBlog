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
    })(domain = App.domain || (App.domain = {}));
})(App || (App = {}));
