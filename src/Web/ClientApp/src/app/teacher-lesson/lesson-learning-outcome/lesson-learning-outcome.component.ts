import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { LessonDesignService } from 'src/app/services/lesson-design.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'lesson-learning-outcome',
  templateUrl: './lesson-learning-outcome.component.html',
  styleUrls: ['./lesson-learning-outcome.component.sass']
})
export class LessonLearningOutcomeComponent implements OnInit {

  form!: FormGroup;
  @Input() formGroupName: string;
  lessonOutcomeForm: FormGroup;

  constructor(private rootFormGroup: FormGroupDirective,private lessonDesignService:LessonDesignService, private spinner: NgxSpinnerService) {

   }

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.lessonOutcomeForm = this.rootFormGroup.control.get(this.formGroupName) as FormGroup;
  }

  trackByFn(index, row) {
    return index;
  } 

  addNewLessonOutcome()
  {
    const fg = new FormGroup({
      id: new FormControl(0),
      lessonOutcome: new FormControl(null,[Validators.required])
  });

  this.lessonOutcomes.push(fg);
  }

  onDeleteLessonOutcome(rowIndex:number): void {  
    this.lessonOutcomes.removeAt(rowIndex);  
 } 
 
 saveLessonLearningOutcomes()
 {
  this.spinner.show();
  this.lessonDesignService.saveLessonLearningOutcome(this.lessonOutcomeForm.getRawValue())
   .subscribe(response=>{
     this.spinner.hide();
     if(response.isSuccess)
     {
       this.lessonOutcomeForm.markAsPristine();
       this.lessonOutcomeForm.markAsUntouched();
       Swal.fire({
         icon: 'success',
         title: 'Done',
         text: response.message,
       });
     }
     else
     {
       this.spinner.hide();
       Swal.fire({
         icon: 'error',
         title: 'Failed',
         text: response.message,
       });
     }
   },error=>{
     this.spinner.hide();
     Swal.fire({
       icon: 'error',
       title: 'Failed',
       text: "Network error has been occured. Please try again.",
     });
   })
 }

  get lessonOutcomes(): FormArray {  
    return this.lessonOutcomeForm.get('lessonOutcomes') as FormArray;  
 } 
}
