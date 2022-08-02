import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { PrimeNGConfig } from 'primeng/api';
import { Observable } from 'rxjs';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { DropdownService } from 'src/app/services/dropdown.service';
import { LessonDesignService } from 'src/app/services/lesson-design.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'lesson-details',
  templateUrl: './lesson-details.component.html',
  styleUrls: ['./lesson-details.component.sass']
})
export class LessonDetailsComponent implements OnInit {

  @Input() formGroupName: string;
  basicLessonForm: FormGroup;

  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  subjects:DropDownModel[]=[];
  classes:DropDownModel[]=[];
  lessonStatuses:DropDownModel[]=[];
  teacherAids:DropDownModel[]=[];

  isDirty$: Observable<boolean>;

  constructor(private lessonDesignService:LessonDesignService,
    private dropDownService:DropdownService,
    private rootFormGroup: FormGroupDirective,
    private router: Router,
    private primengConfig: PrimeNGConfig,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private spinner: NgxSpinnerService) {

      this.lessonDesignService.onLessonValueAssigned.subscribe(response=>{
        this.getTeacherAssignedSubjects();
      });

     }

  ngOnInit(): void {
    this.basicLessonForm = this.rootFormGroup.control.get(this.formGroupName) as FormGroup;
    console.log(this.basicLessonForm);
    this.getMasterData();

  }

  getMasterData()
  {
    this.lessonDesignService.getLessonDesignDropdownMasterData()
      .subscribe(response=>{
        this.academicYears =response.academicYears;
        this.grades = response.grades;
        this.lessonStatuses=response.lessonStatuses;
        this.teacherAids = response.teacherAids;
        if(this.subjectId && this.subjectId>0)
        {
          this.getTeacherAssignedSubjects();
        }
      },error=>{

      });
  }

  getTeacherAssignedSubjects()
  {    
    this.spinner.show();
    this.dropDownService.getTeacherAssignedSubjectForSelectedGrade(this.selectedGradeFilterId)
    .subscribe(response=>{
      //this.spinner.hide();
      this.subjects = response;
      this.getTeacherAssignedClasses();
    },error=>{
      this.spinner.hide();
    })
  }

  getTeacherAssignedClasses()
  {
    //this.spinner.show();
    this.dropDownService.getSubjectClasses(this.selectedGradeFilterId,this.subjectId,this.lessonId)
    .subscribe(response=>{
      this.spinner.hide();
      this.classes = response;
    },error=>{
      this.spinner.hide();
    })
  }

  onAcademicYearFilterChanged(item:any)
  {

  }

  onGradeFilterChanged(item:any)
  {
    this.getTeacherAssignedSubjects();
  }

  onSubjectFilterChanged(item:any)
  {
    this.spinner.show();
    this.getTeacherAssignedClasses();
  }

  saveLessonDetails()
  {
     this.spinner.show();
     this.lessonDesignService.saveLessonDetail(this.basicLessonForm.getRawValue())
      .subscribe(response=>{
        this.spinner.hide();
        if(response.isSuccess)
        {
          this.basicLessonForm.markAsPristine();
          this.basicLessonForm.markAsUntouched();
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

  hasLessonTestChange(value:any)
  {
      this.lessonDesignService.onLessonTestCheckBoxChanged.next(value);
  }

   get selectedAcademicYearFilterId()
  {
    return this.basicLessonForm.get("academicYear").value;
  }

  get lessonId()
  {
    return this.basicLessonForm.get("lessonId").value;
  }

  get selectedGradeFilterId()
  {
    return this.basicLessonForm.get("gradeId").value;
  }

  get subjectId()
  {
    return this.basicLessonForm.get("subjectId").value;
  }

}
