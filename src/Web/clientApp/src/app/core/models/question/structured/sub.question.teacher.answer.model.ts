import { Injectable } from "@angular/core";

@Injectable()
export class SubQuestionTeacherAnswerModel
{
    subQuestionId:number;
    questionId:number;
    answerText:string;
    answerTextRT:string;
}