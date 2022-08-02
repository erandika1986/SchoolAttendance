import { Injectable } from "@angular/core";

@Injectable()
export class StudentModel {
    id:number;
    fullName:string;
    gender :string;
    role :string;
    username :string;
    password :string;
    timeZoneId :string;

    academicYearId:number;
    gradeId:number;
    classId:number;

}