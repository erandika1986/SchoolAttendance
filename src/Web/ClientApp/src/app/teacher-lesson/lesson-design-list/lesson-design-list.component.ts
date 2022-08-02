import { Component, OnInit } from '@angular/core';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { LessonDesignService } from "../../services/lesson-design.service";
import { DropdownService } from "../../services/dropdown.service";
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { BasicLessonModel } from 'src/app/models/lesson/basic.lesson.model';
import { LazyLoadEvent, PrimeNGConfig } from 'primeng/api';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lesson-design-list',
  templateUrl: './lesson-design-list.component.html',
  styleUrls: ['./lesson-design-list.component.scss']
})
export class LessonDesignListComponent implements OnInit {

  academicYears:DropDownModel[];
  grades:DropDownModel[];
  subjects:DropDownModel[];
  searchText:string;

  filterForm:FormGroup;


  pageSize:number=25;
  currentPage:number=1;
  totalRecord:number=0;
  loading: boolean;

  data:BasicLessonModel[]=[];

  constructor(private lessonDesignService:LessonDesignService,
    private dropDownService:DropdownService,
    private router: Router,
    private primengConfig: PrimeNGConfig,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.getMasterData();
    this.filterForm = this.createFilterForm();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      currentPage: new FormControl(1),
      pageSize:new FormControl(25),
      searchText: new FormControl(""),
      academicYear: new FormControl(0),
      gradeId: new FormControl(0),
      subjectId: new FormControl(0),
    });
  }

  getMasterData()
  {
    this.lessonDesignService.getLessonDesignDropdownMasterData()
      .subscribe(response=>{
        this.academicYears =response.academicYears;
        this.grades = response.grades;

        let defaultItem = new DropDownModel();
        defaultItem.id=0;
        defaultItem.name="--All--";

        this.academicYears.unshift(defaultItem);
        this.grades.unshift(defaultItem);

        this.getTeacherAssignedSubjects();
      },error=>{

      });
  }

  getTeacherAssignedSubjects()
  {    
    this.spinner.show();
    this.dropDownService.getTeacherAssignedSubject(this.selectedGradeFilterId)
    .subscribe(response=>{
      this.spinner.hide();
      this.subjects = response;

      let defaultItem = new DropDownModel();
      defaultItem.id=0;
      defaultItem.name="--All--";
      
      this.subjects.unshift(defaultItem);

      this.filterForm.get("subjectId").setValue(0);

      this.spinner.show();
      this.resetPagination();
      this.loading = true;
       this.loadTeacherLesson();

    },error=>{
      this.spinner.hide();
    })
  }

  onAcademicYearFilterChanged(item:any)
  {
    this.filterForm.get("gradeId").setValue(0);
    this.filterForm.get("subjectId").setValue(0);

    this.spinner.show();
    this.resetPagination();
    this.loading = true;
    this.loadTeacherLesson();
  }

  onGradeFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPagination();
    this.getTeacherAssignedSubjects();
  }

  onSubjectFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPagination();
    this.loading = true;
     this.loadTeacherLesson();
  }

      // filter table data
  filterDatatable(event) {
        // get the value of the key pressed and make it lowercase
    const val = event.target.value.toLowerCase();
    this.spinner.show();
    this.resetPagination();
    this.loading = true;
     this.loadTeacherLesson();
  }

  loadTeacherLesson()
  {
    this.filterForm.get("currentPage").setValue(this.currentPage);
    this.filterForm.get("pageSize").setValue(this.pageSize);

    this.lessonDesignService.getNotPublishedLesson(this.filterForm.getRawValue())
      .subscribe(response=>{
        this.spinner.hide();
        this.data =response.data;
        this.totalRecord = response.totalRecordCount;
        this.loading = false;
      },error=>{
        this.spinner.hide();
      });
  }

  loadLessons(event: LazyLoadEvent) {  
    this.loading = true;

    //in a real application, make a remote request to load data using state metadata from event
    //event.first = First row offset
    //event.rows = Number of rows per page
    //event.sortField = Field name to sort with
    //event.sortOrder = Sort order as number, 1 for asc and -1 for dec
    //filters: FilterMetadata object having field as key and filter value, filter matchMode as value
    this.currentPage = (event.first/this.pageSize)+1;
    this.spinner.show();
    this.loadTeacherLesson();
  }

  addNewLesson()
{
  Swal.fire({
    title: 'Creating New Lesson',
    text: "Do you want to create new lesson",
    showCancelButton: true,
    confirmButtonColor: '#54ca68',
    cancelButtonColor: '#868a87',
    confirmButtonText: 'Yes, create new lesson!',
  }).then((result) => {
    if (result.value) {

      this.spinner.show();
      this.lessonDesignService.createNewLesson()
        .subscribe(response=>{
    
          this.spinner.hide();
          if(response.id==0)
          {
            Swal.fire({
              icon: 'error',
              title: 'Failed',
              text: "Failed to create new lesson. Please contact ur support team for more details.",
            });
          }
          else
          {
            this.router.navigate(['/teacher-lessons/lessons-in-design',response.id]);
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
  });

  }

  editLesson(item:BasicLessonModel)
  {
    this.router.navigate(['/teacher-lessons/lessons-in-design',item.id]);
  }

  deleteLesson(item:BasicLessonModel)
  {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#868a87',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {

        this.lessonDesignService.deleteLesson(item.id).subscribe(response=>{
          if(response.isSuccess)
          {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: response.message,
            });

            this.spinner.show();
            this.loadTeacherLesson();
          }
          else
          {
            Swal.fire({
              icon: 'error',
              title: 'Failed',
              text: response.message,
            });
          }
        },error=>{
          Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Network error has been occured. Please try again.",
          });
        })

      }
    });
  }


  resetPagination()
  {
    this.currentPage=1;
    this.pageSize=25;
    this.totalRecord=0;
  }

  get selectedAcademicYearFilterId()
  {
    return this.filterForm.get("academicYear").value;
  }

  get selectedGradeFilterId()
  {
    return this.filterForm.get("gradeId").value;
  }

  get selectedSubjectFilterId()
  {
    return this.filterForm.get("subjectId").value;
  }

  get searchFilterText()
  {
    return this.filterForm.get("searchText").value;
  }
}
