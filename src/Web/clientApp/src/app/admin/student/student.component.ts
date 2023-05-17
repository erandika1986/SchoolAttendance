import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';
import { LazyLoadEvent } from 'primeng/api';
import { BasicStudentModel } from 'src/app/core/models/admin/basic.student.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.modal';
import { UserService } from 'src/app/core/services/user.service';
import { DropdownService } from 'src/app/core/services/dropdown.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.sass']
})
export class StudentComponent implements OnInit {

  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: BasicStudentModel;
  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  classes:DropDownModel[]=[];

  genders =[{id:"M",name:"Male"},{id:"F",name:"Female"}];

  filterForm:FormGroup;
  studentForm:FormGroup;
  passwordUpdateForm:FormGroup;

  data = new Array<BasicStudentModel>();

  currentPage:number=1;
  pageSize:number=45;
  totalRecord:number=0;
  loading: boolean;

  columns = [
    { name: 'Full Name' },
    { name: 'Gender' },
    { name: 'Designation' },
    { name: 'IndexNo' },
    { name: 'Year' },
    { name: 'Grade' },
    { name: 'ClassName' }];

  constructor(private userService:UserService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.loadMasterDataDropdown();
    this.filterForm = this.createFilterForm();
    this.studentForm = this.createNewStudentForm();
    this.passwordUpdateForm = this.createUpdatePasswordForm();
  }

  loadMasterDataDropdown()
  {
    this.spinner.show();
    this.userService.getStudentDropdownsMasterData()
      .subscribe(response=>{
        this.spinner.hide();
        this.academicYears = response.academicYears;
        this.grades=response.grades;
        this.classes=response.classes;

        this.filterForm.get("selectedAcademicYearId").setValue(response.currentAcademicYear);

        this.loadStudents();
      },error=>{
        this.spinner.hide();
      })
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectedAcademicYearId: new FormControl(0),
      selectedGradeId:new FormControl(0),
      selectedClassId: new FormControl(0),
      searchText: new FormControl("")
    });
  }

  createNewStudentForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      fullName: ['', Validators.required],
      gender: [null, Validators.required],
      academicYearId: [null, Validators.required],
      gradeId: [null, Validators.required],
      classId: [null, Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  createUpdatePasswordForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  onAcademicYearFilterChanged(item:any)
  {
    this.filterForm.get("selectedGradeId").setValue(0);
    this.filterForm.get("selectedClassId").setValue(0);

    this.spinner.show();
    this.resetPagination();
    this.loadClasses(this.selectedAcademicYearFilterId,this.selectedGradeFilterId);
  }

  onGradeFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPagination();
    this.loadClasses(this.selectedAcademicYearFilterId,this.selectedGradeFilterId);
  }

  onClassFilterChanged(item:any)
  {
    this.spinner.show();
    this.resetPagination();
    this.loadStudents();
  }


  loadClasses(academicYearId:number,gradeId:number)
  {
    this.dropdownService.getClassesForSelectedGrade(academicYearId,gradeId)
    .subscribe(response=>{
      this.classes=response;
      let defaultItem = new DropDownModel();
      defaultItem.id=0;
      defaultItem.name="--All--";
      this.classes.unshift(defaultItem);
      this.loadStudents();

    },error=>{
      this.spinner.hide();
    });
  }

  loadStudents()
  {
    this.userService.getStudentList(this.searchFilterText,this.currentPage,this.pageSize,this.selectedAcademicYearFilterId,this.selectedGradeFilterId,this.selectedClassFilterId)
      .subscribe(response=>{
        this.data=response.data;
        this.totalRecord=response.totalRecordCount;
        this.loading = false;
        this.spinner.hide();
      },error=>{
        this.spinner.hide();
      });
  }

    // filter table data
    filterDatatable(event) {
      // get the value of the key pressed and make it lowercase
      const val = event.target.value.toLowerCase();
      this.spinner.show();
      this.resetPagination();
      this.loadStudents();

    }

    setPage(pageInfo) {
     this.spinner.show();
     this.currentPage = pageInfo.offset;
     this.loadStudents();
    }

    resetPagination()
    {
      this.currentPage=1;
      this.pageSize=45;
      this.totalRecord=0;
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
      this.loadStudents();
    }


//For List view 
  get selectedAcademicYearFilterId()
  {
    return this.filterForm.get("selectedAcademicYearId").value;
  }

  get selectedGradeFilterId()
  {
    return this.filterForm.get("selectedGradeId").value;
  }

  get selectedClassFilterId()
  {
    return this.filterForm.get("selectedClassId").value;
  }

  get searchFilterText()
  {
    return this.filterForm.get("searchText").value;
  }

  //For Add/Edit view

      // add new record
  addRow(content) {
    this.selectedRowData=null;

        this.studentForm.get('id').setValue(0);
        this.studentForm.get('fullName').setValue('');
        this.studentForm.get('gender').setValue(null);
        this.studentForm.get('username').setValue('');
        this.studentForm.get('academicYearId').setValue(null);
        this.studentForm.get('gradeId').setValue(null);
        this.studentForm.get('classId').setValue(null);
        this.studentForm.get('password').setValue('');
        
        this.studentForm.get('password').setValidators([Validators.required]);
        this.studentForm.get('password').updateValueAndValidity();
  
        this.modalService.open(content, {
          ariaLabelledBy: 'modal-basic-title',
          size: 'lg',
        });
  
  }
    
  
    editRow(row:BasicStudentModel, rowIndex, content) {
  
      this.userService.getStudentById(row.id)
        .subscribe(response=>{
  
          if(row.id>0)
          {
            this.studentForm.get('password').clearValidators();
            this.studentForm.get('password').updateValueAndValidity();
          }
          
          this.modalService.open(content, {
            ariaLabelledBy: 'modal-basic-title',
            size: 'lg',
          });
  
          this.studentForm.get('id').setValue(response.id);
          this.studentForm.get('fullName').setValue(response.fullName);
          this.studentForm.get('gender').setValue(response.gender);
          this.studentForm.get('username').setValue(response.username);
          this.studentForm.get('academicYearId').setValue(response.academicYearId);
          this.studentForm.get('gradeId').setValue(response.gradeId);
          this.studentForm.get('classId').setValue(response.classId);
  
          this.selectedRowData = row;
  
    
          this.loadClasses(response.academicYearId,response.gradeId)
        },error=>{
  
        });
  
  
    }
  

  deleteSelected(row:BasicStudentModel) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#868a87',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {

        this.userService.deleteStudent(row.id).subscribe(response=>{
          if(response.isSuccess)
          {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: response.message,
            });

            this.spinner.show();
            this.loadStudents();
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

  save()
  {
    this.spinner.show();
    this.userService.saveStudent(this.studentForm.getRawValue())
      .subscribe(response=>{
        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });
          this.modalService.dismissAll();
          this.loadStudents();
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

  onAcademicYearChanged(item:any)
  {
    this.spinner.show();
    this.loadClasses(this.selectedAcademicYearId,this.selectedGradeId);
  }

  onGradeChanged(item:any)
  {
    this.spinner.show();
    this.loadClasses(this.selectedAcademicYearId,this.selectedGradeId);
  }

  onClassChanged(item:any)
  {
    this.spinner.show();
    this.loadStudents();
  }

  get studentId()
  {
    return this.studentForm.get("id").value;
  }

  get selectedAcademicYearId()
  {
    return this.studentForm.get("academicYearId").value;
  }

  get selectedGradeId()
  {
    return this.studentForm.get("gradeId").value;
  }

  get selectedClassId()
  {
    return this.studentForm.get("classId").value;
  }
}

