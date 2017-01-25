module App {
    'use strict';
    var app = angular.module("blogApp");

    export interface IPostScope extends ng.IScope {
        ctrl: PostController;
    }

    interface IRouteParams extends ng.route.IRouteParamsService {
        id: string;
    }

    export class PostController {
        static id = "postController";
        
        postId: string;        
        post: domain.Post;        
        roles: domain.Roles;
        newComentText: string;

        constructor($scope: IPostScope, $location: ng.ILocationService, private blogService: BlogService, $routeParams: IRouteParams) {            
            $scope.ctrl = this;
            this.postId = $routeParams.id;

            if (this.postId !== 'undefined') {
                blogService.getPostById(this.postId).then((response) => this.post = response.data as domain.Post, () => alert('Error'));
            }
            else {
                $location.path('/');
            }
            this.blogService.getUserStatus().then((response) => this.roles = response.data as domain.Roles, () => alert('Error'));
        }

        postComment() {
            var obj = { "Id": this.post.Id, "Text": this.newComentText };            
            this.blogService.postMessage(obj, "AddComment").then((response) => {
                let responseData: any = response.data;
                if (responseData.success) {
                    this.post.Coments.push(responseData.Content);
                    this.newComentText = "";
                }
                else {
                    alert('Failed');
                }
            }, () => alert('Error'));            
        }

        deleteComment(commentId: number) {
            this.blogService.deleteMessageWithId(commentId, "DeleteComment").then((response) => {
                if (response.data) {
                    for (var i = 0; i < this.post.Coments.length; i++) {
                        if (this.post.Coments[i].Id == commentId)
                            this.post.Coments.splice(i, 1);
                    }
                }
                else {
                    alert('Cant delete');
                }
            }, function () {
                alert('Error3');
            });
        };
    }


    PostController.$inject = ["$scope", "$location", "blogService", "$routeParams"];

    app.controller(PostController.id, PostController);
}