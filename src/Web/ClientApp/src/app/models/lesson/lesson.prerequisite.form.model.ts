import { Injectable } from "@angular/core";
import { LessonPrerequisiteModel } from "../lesson/lesson.prerequisite.model";

@Injectable()
export class LessonPrerequisiteForm
{
    lessonId:number;
    lessonPrerequisites:LessonPrerequisiteModel[];
}