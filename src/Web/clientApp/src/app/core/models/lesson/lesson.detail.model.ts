import { Injectable } from "@angular/core";

@Injectable()
export class LessonDetailModel
{
    lessonId:number;
    name:string;
    lessonIntroduction:string;
    duration:number;
    competencyLevel:string;

    teachingProcess:string;

    ownerId:number;
    academicYearId:number;
    gradeId:number;

    subjectId:number;
    lessonStatus:number;

    teacherAids:number[];
    assignedClasses:number[];
    hasLessonTest:boolean;
}