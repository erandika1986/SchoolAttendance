import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";
import { ClassSubjectModel } from "./class.subject.model";

@Injectable()
export class ClassModel
{
    id:number;
    selectedAcademicYearId:number;
    selectedGradeId:number;
    selectedClassTeacherId:number;
    name:string;
    classSubjects:ClassSubjectModel[];
}