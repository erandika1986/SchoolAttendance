import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MCQQuestionModel } from "../../question/mcq/mcq.question.model";
import { QuestionMCQTeacherAnswerModel } from "../../question/mcq/question.mcq.teacher.answer.model";
import { OpenEndedQuestionModel } from "../../question/open-ended/open.ended.question.model";
import { QuestionOpneEndedTeacherAnswerModel } from "../../question/open-ended/question.opne.ended.teacher.answer.model";

@Injectable()
export class LessonUnitTestTopicQuestionModel
{
    id:number;
    sequenceNo:number;
    score:number;
    academicYearId:number;
    gradeId:number;
    subjectId:number;
    mcqQuestion:MCQQuestionModel;
    openEndedQuestion:OpenEndedQuestionModel;

    static asFormGroup(item:LessonUnitTestTopicQuestionModel,isDisable:boolean,fb:FormBuilder,questionType:number): FormGroup
    {
        if(questionType==1)
        {
            const fg = new FormGroup({
                id: new FormControl(item.id),
                sequenceNo: new FormControl(item.sequenceNo),
                score: new FormControl(item.score,[Validators.required]),
                academicYearId: new FormControl(item.academicYearId,[Validators.required]),
                gradeId: new FormControl(item.gradeId,[Validators.required]),
                subjectId: new FormControl(item.subjectId,[Validators.required]),
                mcqQuestion: fb.group({
                    id: new FormControl(item.id,[Validators.required]),
                    question: new FormControl(item.mcqQuestion.question,[Validators.required]),
                    questionRT: new FormControl(item.mcqQuestion.questionRT,[Validators.required]),
                    questionType: new FormControl(item.mcqQuestion.questionType,[Validators.required]),
                    ownerId: new FormControl(item.mcqQuestion.ownerId,[Validators.required]),
                    acdemicYearId: new FormControl(item.mcqQuestion.acdemicYearId,[Validators.required]),
                    gradeId: new FormControl(item.mcqQuestion.gradeId,[Validators.required]),
                    subjectId: new FormControl(item.mcqQuestion.subjectId,[Validators.required]),
                    teacherAnswers:fb.array([])
                  }),
            });
    
            const cf = item.mcqQuestion.teacherAnswers.map((value, index) => { return QuestionMCQTeacherAnswerModel.asFormGroup(value, isDisable) });
            const fArray = new FormArray(cf);
            fg.setControl('mcqQuestion.teacherAnswers', fArray);
    
            if(isDisable)
            {
                fg.get("sequenceNo").disable();
                fg.get("score").disable();
                fg.get("mcqQuestion.questionType");
                fg.get("mcqQuestion.question");
                fg.get("mcqQuestion.questionRT");
            }
    
            return fg;
        }
        else if(questionType==2)
        {
            const fg = new FormGroup({
                id: new FormControl(item.id),
                sequenceNo: new FormControl(item.sequenceNo),
                score: new FormControl(item.score,[Validators.required]),
                academicYearId: new FormControl(item.academicYearId,[Validators.required]),
                gradeId: new FormControl(item.gradeId,[Validators.required]),
                subjectId: new FormControl(item.subjectId,[Validators.required]),
                openEndedQuestion: fb.group({
                    id: new FormControl(item.id,[Validators.required]),
                    question: new FormControl(item.mcqQuestion.question,[Validators.required]),
                    questionRT: new FormControl(item.mcqQuestion.questionRT,[Validators.required]),
                    questionType: new FormControl(item.mcqQuestion.questionType,[Validators.required]),
                    ownerId: new FormControl(item.mcqQuestion.ownerId,[Validators.required]),
                    acdemicYearId: new FormControl(item.mcqQuestion.acdemicYearId,[Validators.required]),
                    gradeId: new FormControl(item.mcqQuestion.gradeId,[Validators.required]),
                    subjectId: new FormControl(item.mcqQuestion.subjectId,[Validators.required]),
                    teacherAnswers:fb.array([])
                  }),
            });
    
            const cf = item.openEndedQuestion.teacherAnswers.map((value, index) => { return QuestionOpneEndedTeacherAnswerModel.asFormGroup(value, isDisable) });
            const fArray = new FormArray(cf);
            fg.setControl('openEndedQuestion.teacherAnswers', fArray);
    
            if(isDisable)
            {
                fg.get("sequenceNo").disable();
                fg.get("score").disable();
                fg.get("openEndedQuestion.questionType");
                fg.get("openEndedQuestion.question");
                fg.get("openEndedQuestion.questionRT");
            }
    
            return fg;
        }
        else if(questionType==3)
        {
            return new FormGroup({});
        }

        return new FormGroup({});

    }
}