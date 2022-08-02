import { Injectable } from "@angular/core";

@Injectable()
export class StudentAttendanceModel
{
    studentId :number;
    indexNo :string;
    studentName :string;
    isPresent:boolean;
    gender:string;
    imagePath:string;
}