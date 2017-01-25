module App {
    'use strict';
    var app = angular.module("blogApp");
    

    export interface IContentScope extends ng.IScope {
        ctrl: ContentController;
    }

    export class ContentController {
        static id = "contentController";
        themes: string[];
        selectedTheme: string;
        posts: domain.PostReview[];
        filterText: string;
        roles: domain.Roles;      

        constructor($scope: IContentScope, private $location: ng.ILocationService, private blogService: BlogService)
        {
            $scope.ctrl = this;
            this.blogService.getMessages(0).then((response) => this.posts = response.data as domain.PostReview[], () => alert('Error'));
            this.blogService.getThemes().then((response) => this.themes = response.data as string[], () => alert('Error'));
            this.blogService.getUserStatus().then((response) => this.roles = response.data as domain.Roles, () => alert('Error'));
        }

        showMore() {
            this.blogService.getMessages(this.posts.length).then((response) => Array.prototype.push.apply(this.posts, response.data), () => alert('Error'));
        }

        getTextPreview(text: string) {
            if (text.length > 30) {
                return text.slice(0, 27) + "...";
            }
            else {
                return text;
            }
        }

        clearFilter () {
            this.filterText = "";
            this.selectedTheme = "";
        };

        createNewPost() {
            this.$location.path('/newpost');
        };
        postDetails(id: string) {
            this.$location.path('/post/' + id);
        };

        deletePost(postId: number) {
            this.blogService.deleteMessageWithId(postId, "DeletePost").then((response) => {                
                for (var i = 0; i < this.posts.length; i++)
                {
                    if (this.posts[i].Id == postId)
                        this.posts.splice(i, 1);
                }
            }, function () {
                alert('Error');
            });
        };
             
    }

    ContentController.$inject = ["$scope", "$location", "blogService"];

    app.controller(ContentController.id, ContentController);
}