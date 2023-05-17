import { Injectable } from "@angular/core";

@Injectable()
export class GradeModel
{
    id:number;
    name:string;
    levelHeadName:string;
    levelHeadId:number
    gradeSubjectsText:string;
    gradeSubjects:number[]
}