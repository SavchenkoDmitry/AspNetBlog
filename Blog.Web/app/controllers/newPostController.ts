module App {
    'use strict';
    var app = angular.module("blogApp");

    export interface INewPostScope extends ng.IScope {
        ctrl: NewPostController;
    }
    export class NewPostController {
        static id = "newPostController";
        
        newPostText: string;
        themes: string[];
        selectedTheme: string;

        constructor($scope: INewPostScope, private $location: ng.ILocationService, private blogService: BlogService) {
            $scope.ctrl = this;
            this.blogService.getThemes().then((response) => this.themes = response.data as string[], () => alert('Error'));
        }

        postPost() {
            var obj = { "Topic": this.selectedTheme, "Text": this.newPostText }           
            this.blogService.postMessage(obj, "AddPost").then((response) => {
                if (response.data) {
                    this.$location.path('/');
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