import { Injectable } from "@angular/core";

@Injectable()
export class BasicAttendanceModel
{
    id:number;
    className:string;
    subjectName:string;
    date:string;
    totalAttendedStudents:number;
    totalAbsenceStudents:number;
    startTime:string;
    endTime:string;
    subjectTeacherName:string;
}