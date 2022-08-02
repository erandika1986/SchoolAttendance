import { Injectable } from "@angular/core";
import { BaseQuestionModel } from "../base.question.model";
import { QuestionOpneEndedTeacherAnswerModel } from "./question.opne.ended.teacher.answer.model";

@Injectable()
export class OpenEndedQuestionModel extends BaseQuestionModel
{
    teacherAnswers:QuestionOpneEndedTeacherAnswerModel[];
}