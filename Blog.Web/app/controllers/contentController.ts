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
        posts: PostReview[];
        location: ng.ILocationService;
        service: BlogService;
        filterText: string;
        roles: Roles;      

        constructor($scope: IContentScope, $location: ng.ILocationService, blogService: BlogService)
        {
            $scope.ctrl = this;
            this.location = $location;
            this.service = blogService;
            this.service.GetMessages(0).then((response) => this.posts = response.data as PostReview[], () => alert('Error'));
            this.service.GetThemes().then((response) => this.themes = response.data as string[], () => alert('Error'));
            this.service.GetUserStatus().then((response) => this.roles = response.data as Roles, () => alert('Error'));
        }

        ShowMore() {
            this.service.GetMessages(this.posts.length).then((response) => Array.prototype.push.apply(this.posts, response.data), () => alert('Error'));
        }

        GetTextPreview(text: string) {
            if (text.length > 30) {
                return text.slice(0, 27) + "...";
            }
            else {
                return text;
            }
        }

        ClearFilter () {
            this.filterText = "";
            this.selectedTheme = "";
        };

        CreateNewPost() {
            this.location.path('/newpost');
        };
        PostDetails(id: string) {
            this.location.path('/post/' + id);
        };

        DeletePost(postId: number) {
            this.service.PostMessageWithId(postId, "DeletePost").then((response) => {                
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