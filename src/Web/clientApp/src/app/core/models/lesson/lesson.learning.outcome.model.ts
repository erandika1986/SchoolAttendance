import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class LessonLearningOutcomeModel
{
    id:number;
    lessonOutcome:string;

    static asFormGroup(item:LessonLearningOutcomeModel,isDisable:boolean): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            lessonOutcome: new FormControl(item.lessonOutcome,[Validators.required])
        });

        if(isDisable)
        {
            fg.get("lessonOutcome").disable();
        }

        return fg;
    }
}