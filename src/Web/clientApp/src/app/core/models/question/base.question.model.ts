import { Injectable } from "@angular/core";

@Injectable()
export class BaseQuestionModel
{
    id:number;
    question:string;
    questionRT:string;
    questionType:number;
    ownerId:number;
    acdemicYearId:number;
    gradeId:number;
    subjectId:number;
}