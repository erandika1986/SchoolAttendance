import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { LessonUnitTestTopicModel } from "./lesson.unit.test.topic.model";

@Injectable()
export class LessonUnitTestModel
{
    id:number;
    lessonId:number;
    name:string;
    studentGuide:string;

    topics:LessonUnitTestTopicModel[];


/*     static asFormGroup(item:LessonUnitTestModel,isDisable:boolean,fb:FormBuilder): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            lessonId: new FormControl(item.lessonId,[Validators.required]),
            name: new FormControl(item.name,[Validators.required]),
            studentGuide: new FormControl(item.studentGuide,[Validators.required]),
            topics:fb.array([])
        });

        const cf = item.topics.map((value, index) => { return LessonUnitTestTopicModel.asFormGroup(value, isDisable,fb) });
        const fArray = new FormArray(cf);
        fg.setControl('topics', fArray);

        if(isDisable)
        {
            fg.get("name").disable();
            fg.get("studentGuide").disable();
            fg.get("instruction").disable();
        }

        return fg;
    } */
}