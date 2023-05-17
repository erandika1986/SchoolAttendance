import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { BasicClassDetailModel } from 'src/app/core/models/class/basic.class.detail.model';
import { ClassSubjectModel } from 'src/app/core/models/class/class.subject.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.modal';
import { ClassService } from 'src/app/core/services/class.service';
import { DropdownService } from 'src/app/core/services/dropdown.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.sass']
})
export class ClassComponent implements OnInit {
  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: BasicClassDetailModel;

  currentAcademicYearId:number;
  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  teachers:DropDownModel[]=[];
  
  filterForm:FormGroup;
  classForm:FormGroup;
  classSubjects:ClassSubjectModel[]=[];
  allSubjectTeachersAssigned:boolean=false;

  data = new Array<BasicClassDetailModel>();

  currentPage:number=1;
  pageSize:number=45;
  totalRecord:number=0;
  loading: boolean;

  constructor(private classService:ClassService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.filterForm = this.createFilterForm();
    this.classForm = this.createNewClassForm();
    this.spinner.show();
    this.getMasterDate();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
      academicYearId:new FormControl(null),
      gradeId:new FormControl(null),
      searchText: new FormControl("")
    });
  }

  getMasterDate()
  {
    this.classService.getClassMasterData().subscribe(response=>{
      this.academicYears=response.academicYears;
      this.grades=response.grades;
      this.teachers = response.allTeachers;
      this.currentAcademicYearId = response.currentAcademicYearId;
      this.filterForm.get("academicYearId").setValue(response.currentAcademicYearId);
      this.filterForm.get("academicYearId").disable();
      this.filterForm.get("gradeId").setValue(response.grades[0].id);

      this.getAllClasses();
    },error=>{
      this.spinner.hide();
    })
  }

  onGradeChanged(item:any)
  {
    this.currentPage=1;
    this.pageSize=45;
    this.totalRecord=0;
    this.spinner.show();
    this.getAllClasses();
  }

  getAllClasses()
  {
    this.classService.getClassList(this.searchText,this.currentPage,this.pageSize,this.academicYearId,this.gradeId)
      .subscribe(response=>{
        this.data= response.data;
        this.totalRecord= response.totalRecordCount;
        this.loading = false;
        this.spinner.hide();
      },error=>{
        this.spinner.hide();
      });
  }

  setPage(pageInfo) {
    this.spinner.show();
    this.currentPage = pageInfo.offset;
    this.getAllClasses();
   }

   filterDatatable(event) {
    // get the value of the key pressed and make it lowercase
    const val = event.target.value.toLowerCase();
    this.spinner.show();
    this.getAllClasses();
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
    this.getAllClasses();
  }

  get searchText()
  {
    return this.filterForm.get("searchText").value;
  }

  get academicYearId()
  {
    return this.filterForm.get("academicYearId").value;
  }

  get gradeId()
  {
    return this.filterForm.get("gradeId").value;
  }

  //For Edit/New View

  createNewClassForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      selectedAcademicYearId: [null, Validators.required],
      selectedGradeId: [null, Validators.required],
      selectedClassTeacherId: [null, Validators.required],
      name: ['', Validators.required],
      classSubjects:[null]
    });
  }

  addRow(content) {

    this.selectedRowData=null;
    this.classForm.get('selectedAcademicYearId').setValue(this.currentAcademicYearId);
    this.classForm.get('selectedGradeId').setValue(this.grades[0].id);
    this.spinner.show();
    this.getClassById(this.selectedClassGradeId,0,content);
  }


editRow(row:BasicClassDetailModel, rowIndex, content) {

  this.selectedRowData = row;
  this.classForm.get('selectedAcademicYearId').setValue(row.academicYearId);
  this.classForm.get('selectedGradeId').setValue(row.gradeId);
  this.spinner.show();
  this.getClassById(row.gradeId,row.id,content);
}

  getClassById(gradeId:number,classId:number,content:any)
  {
    this.classService.getClassDetail(gradeId,classId)
      .subscribe(response=>{
        this.spinner.hide();

        if(response.id>0)
        {
          this.classForm.get('id').setValue(response.id);
          this.classForm.get('selectedAcademicYearId').setValue(response.selectedAcademicYearId);
          this.classForm.get('selectedGradeId').setValue(response.selectedGradeId);
          this.classForm.get('selectedClassTeacherId').setValue(response.selectedClassTeacherId);
          this.classForm.get('name').setValue(response.name);

          this.classForm.get('selectedGradeId').disable();
        }
        else
        {
          this.classForm.get('id').setValue(0);
          this.classForm.get('selectedClassTeacherId').setValue(null);
          this.classForm.get('name').setValue('');
        }

        this.classForm.get('selectedAcademicYearId').disable();

        this.classSubjects = response.classSubjects;
        this.checkSubjectTeachersValidity();
        this.modalService.open(content, {
          ariaLabelledBy: 'modal-basic-title',
          size: 'lg',
        });
      },error=>{
        this.spinner.hide();
      });
  }

  onClassGradeChanged(item:any)
  {
    this.spinner.show();
      this.classService.getClassSubjectsForSelectedGrade(this.selectedClassGradeId)
        .subscribe(response=>{
            this.classSubjects = response;
          this.spinner.hide();
        },error=>{
          this.spinner.hide();
        });

  }



  onSubjectTeacherSelectionChange(item:any)
  {
    this.checkSubjectTeachersValidity()
  }

  checkSubjectTeachersValidity()
  {
    if(this.classSubjects.length<=0)
    {
      return false;
    }
    
     for (let index = 0; index < this.classSubjects.length; index++) {
       if(!this.classSubjects[index].subjectTeacherId)
       {
         this.allSubjectTeachersAssigned=false;
         return this.allSubjectTeachersAssigned;
       }
     }

     this.allSubjectTeachersAssigned=true;

     return this.allSubjectTeachersAssigned;
  }

  save()
  {
    this.spinner.show();
    var classObject = this.classForm.getRawValue();
    classObject.classSubjects = this.classSubjects;

    this.classService.saveClassDetail(classObject).subscribe(response=>{
      if(response.isSuccess)
      {
        Swal.fire({
          icon: 'success',
          title: 'Done',
          text: response.message,
        });
        this.modalService.dismissAll();
        this.getAllClasses();
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
    });
  }

  get selectedClassGradeId()
  {
    return this.classForm.get('selectedGradeId').value;
  }
}
