module App {

    var app = angular.module("blogApp");

    export class BlogService 
    {        
        static id = "blogService";
        http: any;

        constructor(private $http: ng.IHttpService) {
            this.http = $http;
        }

        getMessages(count: number) {           
            return this.$http.get("/api/blog/GetPosts?count=" + count);
        }
        getPostById(id: string) {
            return this.$http.get("/api/blog/GetPost?id=" + id);
        }
        postMessage(obj: any, method: string) {
            return this.$http.post("/api/blog/" + method, obj);
        }
        deleteMessageWithId(id: number, method: string) {
            return this.$http.delete("/api/blog/" + method + "/" + id);
        }
        getThemes() {
            return this.$http.get("/api/blog/GetThemes");
        }
        getUserStatus() {
            return this.$http.get("/api/blog/GetUserStatus");
        }

    }

    app.service(BlogService.id, BlogService);

}