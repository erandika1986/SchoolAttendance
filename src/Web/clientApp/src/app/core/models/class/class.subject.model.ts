import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";

@Injectable()
export class ClassSubjectModel
{
    classId:number;
    subjectId :number;
    subjectName:string;
    subjectTeacherId:number;
    subjectTeachers:DropDownModel[]
}