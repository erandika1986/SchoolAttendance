import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { LessonDesignService } from 'src/app/core/services/lesson-design.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'lesson-prerequisites',
  templateUrl: './lesson-prerequisites.component.html',
  styleUrls: ['./lesson-prerequisites.component.sass']
})
export class LessonPrerequisitesComponent implements OnInit {

  //@Input() formArrayName!: string;
  //formArray!: FormArray;


  @Input() formGroupName: string;
  lessonPrerequisiteForm: FormGroup;

  form!: FormGroup;
  constructor(private rootFormGroup: FormGroupDirective,private lessonDesignService:LessonDesignService, private spinner: NgxSpinnerService) {}

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.lessonPrerequisiteForm = this.rootFormGroup.control.get(this.formGroupName) as FormGroup;

    //this.formArray = this.form.get(this.formArrayName) as FormArray;
    
  }

  trackByFn(index, row) {
    return index;
  } 

  addNewPrerequisite()
  {
    const fg = new FormGroup({
      id: new FormControl(0),
      prerequisite: new FormControl(null,[Validators.required])
  });

  this.lessonPrerequisites.push(fg);
  }

  onDeletePrerequisite(rowIndex:number): void {  
    this.lessonPrerequisites.removeAt(rowIndex);  
 } 

 saveLessonPrerequisite()
 {
  this.spinner.show();
  this.lessonDesignService.saveLessonPrerequisite(this.lessonPrerequisiteForm.getRawValue())
   .subscribe(response=>{
     this.spinner.hide();
     if(response.isSuccess)
     {
       this.lessonPrerequisiteForm.markAsPristine();
       this.lessonPrerequisiteForm.markAsUntouched();
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

  get lessonPrerequisites(): FormArray {  
    return this.lessonPrerequisiteForm.get('lessonPrerequisites') as FormArray;  
 } 
}
