import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { DropdownService } from 'src/app/services/dropdown.service';
import { BasicAttendanceModel } from '../../models/attendance/basic.attendance.model';
import { AttendanceService } from '../../services/attendance.service';

@Component({
  selector: 'app-subject-attendance-list',
  templateUrl: './subject-attendance-list.component.html',
  styleUrls: ['./subject-attendance-list.component.sass']
})
export class SubjectAttendanceListComponent implements OnInit {


  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: BasicAttendanceModel;

  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  classes:DropDownModel[]=[];
  subjects:DropDownModel[]=[];

  filterForm:FormGroup;

  data = new Array<BasicAttendanceModel>();

  currentPage:number=1;
  pageSize:number=25;
  totalRecord:number=0;
  loading: boolean;

  constructor(private attendanceService:AttendanceService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private router: Router,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) {
      
     }

  ngOnInit(): void {

    this.filterForm = this.createFilterForm();
    this.spinner.show();
    this.loadMasterData();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectedAcademicYearId: new FormControl(0),
      selectedGradeId:new FormControl(0),
      selectedClassId: new FormControl(0),
      selectedSubjectId:new FormControl(0),
      searchText: new FormControl("")
    });
  }

  onAcademicYearFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPaging();
    this.loadClassesForTecher();
  }

  onGradeFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPaging();
    this.loadClassesForTecher();
  }

  onClassFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPaging();
    this.loadSubjectForTeacher();
  }

  onSubjectFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPaging();
    this.loadAttendance();
  }

  filterDatatable(event) {
    // get the value of the key pressed and make it lowercase
    const val = event.target.value.toLowerCase();
    this.resetPaging();
    this.spinner.show();
    this.loadAttendance();

  }

  setPage(pageInfo) {
   this.spinner.show();
   this.currentPage = pageInfo.offset;
   this.loadAttendance();
  }

  loadMasterData()
  {
    this.attendanceService.getAttendanceListTeacherDropdownMasterData().subscribe(response=>{

      this.academicYears = response.academicYears;
      this.grades=response.grades;
      this.classes=response.classes;
      this.subjects=response.subjects;

      this.filterForm.get("selectedAcademicYearId").setValue(response.currentAcademicYear);
      this.filterForm.get("selectedGradeId").setValue(response.selectedGradeId);
      if(response.grades.length>0)
      {
        this.loadClassesForTecher();
      }
    },error=>{

    });
  }

  loadClassesForTecher()
  {
    this.dropdownService.getAssignedClassForLoggedInUser(this.selectedAcademicYearId,this.selectedGradeId)
      .subscribe(response=>{

        this.classes=response;
        if(this.classes.length>0)
        {
          this.filterForm.get("selectedClassId").setValue(this.classes[0].id);
          this.loadSubjectForTeacher();
        }
        else
        {
          this.subjects=[];
          this.spinner.hide();
        }


      },error=>{
        this.spinner.hide();
      });
  }

  loadSubjectForTeacher()
  {
    this.dropdownService.getAssignedClassSubjectForLoggedInUser(this.selectedClassId)
      .subscribe(response=>{

        this.subjects=response; 
        if(this.subjects.length>0)
        {
          this.filterForm.get("selectedSubjectId").setValue(this.subjects[0].id);
          this.loadAttendance();
        }
        else
        {
          this.spinner.hide();
        }
        
      },error=>{
        this.spinner.hide();
      });
  }



  loadAttendance()
  {
     this.attendanceService
      .getMySubjectAttendance(this.searchText,this.currentPage,this.pageSize,
        this.selectedAcademicYearId,this.selectedGradeId,
        this.selectedClassId,this.selectedSubjectId)
        .subscribe(response=>{
          this.data=response.data;
          this.totalRecord=response.totalRecordCount;
          this.loading = false;
          this.spinner.hide();
        },error=>{
          this.spinner.hide();
        });
  }

  resetPaging()
  {
    this.currentPage=1;
    this.totalRecord=0;
  }


  addNewAttendance()
  {
    this.router.navigate(['/attendance/attendance-list',0]);
  }

  editExsitingAttendance(row:BasicAttendanceModel, rowIndex:number)
  {
    this.router.navigate(['/attendance/attendance-list',row.id]);
  }

  deleteSelecedAttendance(row:BasicAttendanceModel)
  {

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
    this.loadAttendance();
  }

  get selectedAcademicYearId()
  {
    return this.filterForm.get("selectedAcademicYearId").value;
  }

  get selectedGradeId()
  {
    return this.filterForm.get("selectedGradeId").value;
  }

  get selectedClassId()
  {
    return this.filterForm.get("selectedClassId").value;
  }

  get selectedSubjectId()
  {
    return this.filterForm.get("selectedSubjectId").value;
  }

  get searchText()
  {
    return this.filterForm.get("searchText").value;
  }

}
