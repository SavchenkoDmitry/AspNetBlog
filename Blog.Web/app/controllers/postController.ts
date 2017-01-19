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
        
        service: BlogService;
        postId: string;        
        post: Post;        
        roles: Roles;
        newComentText: string;

        constructor($scope: IPostScope, $location: ng.ILocationService, blogService: BlogService, $routeParams: IRouteParams) {            
            $scope.ctrl = this;
            this.postId = $routeParams.id;
            this.service = blogService;

            if (this.postId !== 'undefined') {
                blogService.GetPostById(this.postId).then((response) => this.post = response.data as Post, () => alert('Error'));
            }
            else {
                $location.path('/');
            }
            this.service.GetUserStatus().then((response) => this.roles = response.data as Roles, () => alert('Error'));
        }

        PostComment() {
            var obj = { "Id": this.post.Id, "Text": this.newComentText };            
            this.service.PostMessage(obj, "AddComment").then((response) => {
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

        DeleteComment(commentId: number) {
            this.service.PostMessageWithId(commentId, "DeleteComment").then((response) => {
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