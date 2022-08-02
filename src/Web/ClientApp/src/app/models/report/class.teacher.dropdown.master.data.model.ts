import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";

@Injectable()
export class ClassTeacheeDropDownMasterData
{

    academicYears:DropDownModel[];
    grades:DropDownModel[];
    classes:DropDownModel[];



    role:string;

    selectedYearId:number;
    selectedGradeId:number;
    selectedClassId:number;
    selectedSubjectId:number;


    fromYear:number;
    fromMonth:number;
    fromDay:number;
    toYear:number;
    toMonth:number;
    toDay:number;

}