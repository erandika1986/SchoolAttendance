import { Injectable } from "@angular/core";
import { BaseQuestionModel } from "../base.question.model";
import { QuestionMCQTeacherAnswerModel } from "./question.mcq.teacher.answer.model";

@Injectable()
export class MCQQuestionModel extends BaseQuestionModel
{
    teacherAnswers:QuestionMCQTeacherAnswerModel[];
}