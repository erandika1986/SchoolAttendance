import { Injectable } from "@angular/core";

@Injectable()
export class BasicClassDetailModel
{
    id:number;
    name:string;
    classTeacherName:string;
    totalStudentCount:number;
    academicYearId:number;
    gradeId:number;
}