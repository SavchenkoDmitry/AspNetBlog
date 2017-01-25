module App.domain {
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
}