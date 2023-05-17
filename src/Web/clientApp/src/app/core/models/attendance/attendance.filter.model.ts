import { Injectable } from "@angular/core";

@Injectable()
export class AttendanceFilterModel
{
    id:number;
    gradeId:number;
    classId:number;
    subjectId:number;
    year:number;
    month:number;
    day:number;
    startHour:number;
    startMin:number;
    endHour:number;
    endMin:number;
}