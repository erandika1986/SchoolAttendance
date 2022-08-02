import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";

@Injectable()
export class AttendanceListFilterMasterData
{
    currentAcademicYear:number;
    selectedGradeId:number;
    selectedClassId:number;
    selectedSubjectId:number;

    academicYears:DropDownModel[];
    grades:DropDownModel[];
    classes:DropDownModel[];
    subjects:DropDownModel[];
}