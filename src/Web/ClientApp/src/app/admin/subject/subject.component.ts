import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { DropdownService } from 'src/app/services/dropdown.service';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';
import { SubjectModel } from "../../models/subject/subject.model";
import { SubjectService } from "../../services/subject.service";

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.sass']
})
export class SubjectComponent implements OnInit {

  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: SubjectModel;

  allTeachers:DropDownModel[]=[];

  statuses =[{id:true,name:"Active"},{id:false,name:"Inactive"}];

  teachMedium =[{id:'Sinhala',name:"Sinhala"},{id:"English",name:"English"},{id:"Tamil",name:"Tamil"}];

  filterForm:FormGroup;
  subjectForm:FormGroup;

  data = new Array<SubjectModel>();

  currentPage:number=1;
  pageSize:number=25;
  totalRecord:number=0;
  loading: boolean;

  constructor(private subjectService:SubjectService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {

    this.filterForm = this.createFilterForm();
    this.subjectForm = this.createNewSubjectForm();
    this.spinner.show();
    this.getAllDepartmentHead();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
      selectedStatus:new FormControl(true),
      searchText: new FormControl("")
    });
  }

  createNewSubjectForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      name : ['', Validators.required],
      description : [''],
      medium : [null, Validators.required],
      departmentHeadId  : [null],
    });
  }

  getAllDepartmentHead()
  {
    this.dropdownService.getAllDepartmentHeads()
      .subscribe(response=>{
        this.allTeachers = response;
        this.loadSubject();
      },error=>{
        this.spinner.hide();
      });
  }

  filterDatatable(event) {
    // get the value of the key pressed and make it lowercase
    const val = event.target.value.toLowerCase();
    this.spinner.show();
    this.loadSubject();

  }

  loadSubject()
  {
    this.subjectService.getSubjectList(this.searchFilterText,this.currentPage,this.pageSize,this.selectedStatus)
      .subscribe(response=>{
        this.data=response.data;
        this.totalRecord=response.totalRecordCount;
        this.loading = false;
        this.spinner.hide();
      },error=>{
        this.spinner.hide();
      });
  }

  setPage(pageInfo) {
    this.spinner.show();
    this.currentPage = pageInfo.offset;
    this.loadSubject();
   }

   onStatusFilterChanged(item:any)
   {
     this.spinner.show();
     this.currentPage=1;
     this.pageSize=25;
     this.totalRecord=0;

     this.loadSubject();
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
    this.loadSubject();
  }


  get selectedStatus()
  {
    return this.filterForm.get("selectedStatus").value;
  } 

  get searchFilterText()
  {
    return this.filterForm.get("searchText").value;
  }


    //For Add/Edit view

      // add new record
      addRow(content) {

        this.selectedRowData=null;

        this.subjectForm.get('id').setValue(0);
        this.subjectForm.get('name').setValue('');
        this.subjectForm.get('description').setValue(null);
        this.subjectForm.get('medium').setValue(null);
        this.subjectForm.get('departmentHeadId').setValue(null);
        this.modalService.open(content, {
          ariaLabelledBy: 'modal-basic-title',
          size: 'lg',
        });
  
      }
    
  
    editRow(row:SubjectModel, rowIndex, content) {
  
      this.selectedRowData=row;
      
      this.subjectForm.get('id').setValue(row.id);
      this.subjectForm.get('name').setValue(row.name);
      this.subjectForm.get('description').setValue(row.description);
      this.subjectForm.get('medium').setValue(row.medium);
      if(row.departmentHeadId>0)
      {
        this.subjectForm.get('departmentHeadId').setValue(row.departmentHeadId);
      }
      else
      {
        this.subjectForm.get('departmentHeadId').setValue(null);
      }


      this.modalService.open(content, {
        ariaLabelledBy: 'modal-basic-title',
        size: 'lg',
      });
    }
  

  deleteSelected(row:SubjectModel) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#868a87',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {

        this.subjectService.deleteSubject(row.id).subscribe(response=>{
          if(response.isSuccess)
          {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: response.message,
            });

            this.spinner.show();
            this.loadSubject();
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
    this.subjectService.saveSubject(this.subjectForm.getRawValue())
      .subscribe(response=>{
        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });
          this.modalService.dismissAll();
          this.loadSubject();;
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
  get subjectId()
  {
    return this.subjectForm.get("id").value;
  }
}
