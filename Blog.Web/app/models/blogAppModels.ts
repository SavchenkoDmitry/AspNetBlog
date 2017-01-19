module App {
    'use strict';
    export class Comment {
        Time: string;
        Text: string;
        Id: number;
        Author: string;
        IsAuthor: boolean;
    }
    export class Post {
        Time: string;
        Topic: string;
        Text: string;
        Author: string;
        Id: number;
        Coments: Comment[];
    }
    export class PostReview {
        Author: string;
        Text: string;
        Time: string;
        Topic: string;
        Id: number;
    }
    export class Roles {
        IsAuthor: boolean;
        IsUser: boolean;
    }

}