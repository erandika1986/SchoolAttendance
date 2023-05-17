import { Injectable } from "@angular/core";
import { LessonDetailModel } from "../lesson/lesson.detail.model";
import { LessonPrerequisiteForm } from "../lesson/lesson.prerequisite.form.model";
import { LessonOutcomeForm } from "../lesson/lesson.outcome.form.model";
import { LessonTopicForm } from "../lesson/lesson.topic.form.model";
import { LessonUnitTestModel } from "./unit-test/lesson.unit.test.model";

@Injectable()
export class LessonModel
{
    id:number;
    lessonDetail:LessonDetailModel;
    lessonPrerequisiteForm:LessonPrerequisiteForm;
    lessonOutcomeForm:LessonOutcomeForm;
    lessonTopicForm:LessonTopicForm;
    lessonUnitTest:LessonUnitTestModel;


}