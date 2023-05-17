import { Injectable } from "@angular/core";

@Injectable()
export class SubjectModel
{
    id:number;
    name :string;
    description :string;
    medium :string;
    departmentHeadName :string;
    departmentHeadId :number;
    assignedGrades :string;
}