import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop.down.modal";

@Injectable()
export class ClassMasterDataModel
{
    academicYears:DropDownModel[];
    grades:DropDownModel[]
    allTeachers:DropDownModel[];
    currentAcademicYearId:number;
}