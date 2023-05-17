import { Injectable } from "@angular/core";
import { BaseQuestionModel } from "../base.question.model";
import { SubQuestionModel } from "./sub.question.model";

@Injectable()
export class QuestionStructuredModel extends BaseQuestionModel
{
    subQuestions:SubQuestionModel[];
}