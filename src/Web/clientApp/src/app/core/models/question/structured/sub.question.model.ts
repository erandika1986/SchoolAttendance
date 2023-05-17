import { Injectable } from "@angular/core";
import { SubQuestionTeacherAnswerModel } from "./sub.question.teacher.answer.model";

@Injectable()
export class SubQuestionModel
{
    structuredQuestionId:number;
    questionText:string;
    questionTextRT:string;
    teacherAnswers:SubQuestionTeacherAnswerModel[];
}