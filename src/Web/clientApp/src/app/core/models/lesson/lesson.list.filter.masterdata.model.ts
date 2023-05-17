import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";

@Injectable()
export class LessonListFilterMasterData
{
    currentAcademicYear:number;
    selectedGradeId:number;

    academicYears:DropDownModel[];
    grades:DropDownModel[];
    lessonStatuses:DropDownModel[];
    teacherAids:DropDownModel[];
}