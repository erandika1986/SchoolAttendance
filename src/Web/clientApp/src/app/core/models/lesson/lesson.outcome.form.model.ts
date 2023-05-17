import { Injectable } from "@angular/core";
import { LessonLearningOutcomeModel } from "../lesson/lesson.learning.outcome.model";

@Injectable()
export class LessonOutcomeForm
{
    lessonId:number;
    lessonOutcomes:LessonLearningOutcomeModel[];
}