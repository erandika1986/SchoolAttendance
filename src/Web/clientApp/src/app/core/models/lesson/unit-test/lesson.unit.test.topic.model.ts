import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { LessonUnitTestTopicQuestionModel } from "./lesson.unit.test.topic.question.model";

@Injectable()
export class LessonUnitTestTopicModel
{
    id:number;
    lessonUnitTestId:number;
    name:string;
    instruction:string;
    questionTypeId:number;
    editable:boolean;
    questions:LessonUnitTestTopicQuestionModel[];

    static asFormGroup(item:LessonUnitTestTopicModel,isDisable:boolean,fb:FormBuilder): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            lessonUnitTestId: new FormControl(item.lessonUnitTestId,[Validators.required]),
            name: new FormControl(item.name,[Validators.required]),
            instruction: new FormControl(item.instruction,[Validators.required]),
            questionTypeId: new FormControl(item.questionTypeId),
            editable:new FormControl(item.editable),
            questions:fb.array([])
        });

        const cf = item.questions.map((value, index) => { return LessonUnitTestTopicQuestionModel.asFormGroup(value, isDisable,fb,item.questionTypeId) });
        const fArray = new FormArray(cf);
        //fg.setControl('questions', fArray);
        (fg.get("questions") as FormArray).setValue(cf);

        if(isDisable)
        {
            fg.get("name").disable();
            fg.get("instruction").disable();
        }

        return fg;
    }
}