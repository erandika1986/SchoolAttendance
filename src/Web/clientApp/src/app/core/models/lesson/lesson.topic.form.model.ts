import { Injectable } from "@angular/core";
import { LessonTopicModel } from "../lesson/lesson.topic.model";

@Injectable()
export class LessonTopicForm
{
    lessonId:number;
    lessonTopics:LessonTopicModel[];
}