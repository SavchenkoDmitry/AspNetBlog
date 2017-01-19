module App {
    'use strict';
    var app = angular.module("blogApp");

    export interface INewPostScope extends ng.IScope {
        ctrl: NewPostController;
    }
    export class NewPostController {
        static id = "newPostController";

        service: BlogService;
        newPostText: string;
        themes: string[];
        selectedTheme: string;
        location: ng.ILocationService;

        constructor($scope: INewPostScope, $location: ng.ILocationService, blogService: BlogService) {
            $scope.ctrl = this;
            this.location = $location;
            this.service = blogService;
            this.service.GetThemes().then((response) => this.themes = response.data as string[], () => alert('Error'));
        }

        PostPost() {
            var obj = { "Topic": this.selectedTheme, "Text": this.newPostText }           
            this.service.PostMessage(obj, "AddPost").then((response) => {
                if (response.data) {
                    this.location.path('/');
                }
                else {
                    alert('Failed');
                }
            }, () => alert('Error'));
        }
    }
    
    NewPostController.$inject = ["$scope", "$location", "blogService"];

    app.controller(NewPostController.id, NewPostController);
}