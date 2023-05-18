import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
import { LessonLectureModel } from "./lesson.lecture.model";

@Injectable()
export class LessonTopicModel
{
    id:number;
    lessonId:number;
    name:string;
    sequenceNo:number;
    lessonLectures:LessonLectureModel[];
    editable:boolean=false;

    static asFormGroup(item:LessonTopicModel,isDisable:boolean,fb:FormBuilder,sanitizer: DomSanitizer): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            name: new FormControl(item.name,[Validators.required]),
            sequenceNo: new FormControl(item.sequenceNo),
            editable: new FormControl(item.editable),
            lessonLectures:fb.array([])
        });

        const cf = item.lessonLectures.map((value, index) => { return LessonLectureModel.asFormGroup(value, isDisable,sanitizer) });
        const fArray = new FormArray(cf);
        //fg.setControl('lessonLectures', fArray);
        (fg.get("lessonLectures") as FormArray).setValue(cf);

        if(isDisable)
        {
            fg.get("name").disable();
        }

        return fg;
    }
}