import { Injectable } from "@angular/core";
import { StudentAttendanceModel } from "./student.attendance.model";

@Injectable()
export class SubjectAttendaceModel
{
    id :number;
    gradeId:number;
    classId :number;
    subjectId :number;
    year :number;
    month :number;
    day :number;
    startHour :number;
    startMin :number;
    endHour :number;
    endMin :number;
    isExtraClass:boolean;
    lessonDetails:string;
    softwareName:string;
    timeSlotId:number;

    studentsAttendance:StudentAttendanceModel[];
}