import { Injectable } from "@angular/core";
import { CheckBoxModel } from "../common/check.box.model";
@Injectable()
export class UserModel {
    id:number;
    fullName:string;
    gender:string;
    roles:string;
    username:string;
    password:string;
    timeZoneId:string;
    assignedSubjectsInText:string;
    assignedSubjects:number[];
    assignedRoles:number[];

}