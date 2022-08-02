import { Injectable } from "@angular/core";

@Injectable()
export class LessonFilter
{
    currentPage:number;
    pageSize:number;
    searchText:string;
    academicYear:number;
    gradeId:number;
    subjectId:number;
}