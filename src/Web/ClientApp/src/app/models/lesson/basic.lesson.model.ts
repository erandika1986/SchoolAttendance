import { Injectable } from "@angular/core";

@Injectable()
export class BasicLessonModel
{
    id:number;
    name:string;
    owner:string;
    academicYear:number;
    gradeName:string;
    subject:string;
    createdOn:string;
    status:string;
}