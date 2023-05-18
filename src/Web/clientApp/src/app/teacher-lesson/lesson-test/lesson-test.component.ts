import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { LessonUnitTestTopicModel } from 'src/app/core/models/lesson/unit-test/lesson.unit.test.topic.model';
import { DropdownService } from 'src/app/core/services/dropdown.service';
import { LessonDesignService } from 'src/app/core/services/lesson-design.service';

@Component({
  selector: 'lesson-test',
  templateUrl: './lesson-test.component.html',
  styleUrls: ['./lesson-test.component.scss']
})
export class LessonTestComponent implements OnInit {

  form!: FormGroup;
  @Input() formGroupName: string;
  lessonUnitTestForm: FormGroup;

  displaySectionType:boolean;

  constructor(private lessonDesignService:LessonDesignService,
    private dropDownService:DropdownService,
    private fb: FormBuilder,
    private rootFormGroup: FormGroupDirective,private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.lessonUnitTestForm = this.rootFormGroup.control.get(this.formGroupName) as FormGroup;
  }


  addNewTestSection()
  {
    this.displaySectionType=true;
/*      this.lessonDesignService.createNewLessonTopic(this.lessonId).subscribe(response=>{

      const fg = new FormGroup({
        id: new FormControl(response.id),
        lessonId: new FormControl(this.lessonId),
        name: new FormControl(response.name,[Validators.required]),
        sequenceNo: new FormControl(response.sequenceNo),
        editable:new FormControl(true),
        lessonLectures:this.fb.array([])
    });
  
    this.lessonTopics().push(fg);

    },error=>{

    }) */
  }

  setSectionType(type:number)
  {
    this.displaySectionType=false;

    const fg = new FormGroup({
      id: new FormControl(0),
      lessonUnitTestId: new FormControl(this.lessonTestId),
      name: new FormControl('',[Validators.required]),
      questionTypeId: new FormControl(type),
      editable:new FormControl(false),
      questions:this.fb.array([])
  });

    let value:any = fg.getRawValue();
    this.lessonDesignService.saveLessonUnitTestTopic(value).subscribe(response=>{

      fg.get('id').setValue(response.id);
      fg.get('name').setValue(response.name);
  
    this.topics().push(fg);

    },error=>{

    })
  }

  saveSaveTestDetails()
  {
    this.spinner.show();
    this.lessonDesignService.saveUnitTestDetail(this.lessonUnitTestForm.getRawValue())
      .subscribe(response=>{
        this.spinner.hide();
        this.lessonUnitTestForm.get("id").setValue(response.id);
      },error=>{
        this.spinner.hide();
      })
  }

  addNewQuestion(section:any)
  {
    console.log(section);
    
  }

  get lessonTestId()
  {
    return this.lessonUnitTestForm.get("id").value;
  }

  topics(): FormArray {  
    return this.lessonUnitTestForm.get('topics') as FormArray;  
 } 


}
