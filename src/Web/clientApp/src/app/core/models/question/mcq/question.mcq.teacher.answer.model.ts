import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class QuestionMCQTeacherAnswerModel
{
    id:number;
    questionId:number;
    answerText:string;
    answerTextRT:string;
    sequenceNo:number;
    isCorrectAnswer:boolean;


    static asFormGroup(item:QuestionMCQTeacherAnswerModel,isDisable:boolean): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            questionId: new FormControl(item.questionId,[Validators.required]),
            answerText: new FormControl(item.answerText,[Validators.required]),
            answerTextRT: new FormControl(item.answerTextRT,[Validators.required]),
            sequenceNo: new FormControl(item.sequenceNo),
            isCorrectAnswer: new FormControl(item.isCorrectAnswer)
        });

        if(isDisable)
        {
            fg.get("answerText").disable();
            fg.get("answerTextRT").disable();
            fg.get("isCorrectAnswer").disable();
        }

        return fg;
    }
}