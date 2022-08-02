import { Injectable } from "@angular/core";
import {DropDownModel} from "../common/drop.down.modal";

@Injectable()
export class StudentListDropDownMasterData {
    currentAcademicYear:number;
    selectedGradeId:number;
    selectedClassId:number;

    academicYears:DropDownModel[];
    grades:DropDownModel[];
    classes:DropDownModel[];

}