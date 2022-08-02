import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class QuestionOpneEndedTeacherAnswerModel
{
    id:number;
    questionId:number;
    answerText:string;
    answerTextRT:string;

    static asFormGroup(item:QuestionOpneEndedTeacherAnswerModel,isDisable:boolean): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            questionId: new FormControl(item.questionId,[Validators.required]),
            answerText: new FormControl(item.answerText,[Validators.required]),
            answerTextRT: new FormControl(item.answerTextRT,[Validators.required])
        });

        if(isDisable)
        {
            fg.get("answerText").disable();
            fg.get("answerTextRT").disable();
        }

        return fg;
    }
}