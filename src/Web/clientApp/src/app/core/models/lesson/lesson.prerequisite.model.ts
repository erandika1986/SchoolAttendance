import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class LessonPrerequisiteModel
{
    id:number;
    prerequisite:string;

    static asFormGroup(item:LessonPrerequisiteModel,isDisable:boolean): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            prerequisite: new FormControl(item.prerequisite,[Validators.required])
        });

        if(isDisable)
        {
            fg.get("prerequisite").disable();
        }

        return fg;
    }
}