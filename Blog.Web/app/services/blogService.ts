module App {

    var app = angular.module("blogApp");

    export interface IBlogService {
        GetMessages(): number;
    }

    export class BlogService 
    {        
        static id = "blogService";
        http: any;

        constructor(private $http: ng.IHttpService) {
            this.http = $http;
        }

        GetMessages(count: number) {           
            return this.$http.get("/Home/GetPosts?count=" + count);
        }
        GetPostById(id: string) {
            return this.$http.get("/Home/GetPost?id=" + id);
        }
        PostMessage(obj: any, method: string) {
            return this.$http.post("/Home/" + method, obj);
        }
        PostMessageWithId(id: number, method: string) {
            return this.$http.post("/Home/" + method + "/" + id, "");
        }
        GetThemes() {
            return this.$http.get("/Home/GetThemes");
        }
        GetUserStatus() {
            return this.$http.get("/Home/GetUserStatus");
        }

    }

    app.service(BlogService.id, BlogService);

}