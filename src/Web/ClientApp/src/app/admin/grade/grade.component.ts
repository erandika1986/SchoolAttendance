import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { DropdownService } from 'src/app/services/dropdown.service';
import Swal from 'sweetalert2';
import { GradeModel } from "../../models/grade/grade.model";
import { GradeService } from "../../services/grade.service";

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrls: ['./grade.component.sass']
})
export class GradeComponent implements OnInit {

  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: GradeModel;

  filterForm:FormGroup;
  gradeForm:FormGroup;

  teachers:DropDownModel[]=[];
  subjects:DropDownModel[]=[];

  data = new Array<GradeModel>();

  currentPage:number=1;
  pageSize:number=25;
  totalRecord:number=0;
  loading: boolean;

  constructor(private gradeService:GradeService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {

    this.filterForm = this.createFilterForm();
    this.gradeForm = this.createNewSubjectForm();
    this.spinner.show();
    this.getAllSubjects()

  }

  getAllSubjects()
  {
    this.dropdownService.getAllSubjects().subscribe(response=>{

      this.subjects = response;
      this.getAllLevelHeads();

    },error=>{
      this.spinner.hide();
    });
  }

  getAllLevelHeads()
  {
    this.dropdownService.getAllLevelHeads().subscribe(response=>{

      this.teachers = response;
      this.loadGrades();

    },error=>{
      this.spinner.hide();
    });
  }

  createFilterForm(): FormGroup {
    return new FormGroup({

      searchText: new FormControl("")
    });
  }

  createNewSubjectForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      name : ['', Validators.required],
      levelHeadId : [null],
      gradeSubjects : [null, Validators.required]
    });
  }


  loadGrades()
  {
    this.gradeService.getGradeList()
      .subscribe(response=>{
        this.data=response;
        this.loading = false;
        this.spinner.hide();
      },error=>{
        this.spinner.hide();
      });
  }

  setPage(pageInfo) {
    this.spinner.show();
    this.currentPage = pageInfo.offset;
    this.loadGrades();
   }

   onStatusFilterChanged(item:any)
   {
     this.spinner.show();
     this.currentPage=1;
     this.pageSize=25;
     this.totalRecord=0;

     this.loadGrades();
   }


    //For Add/Edit view
  
    editRow(row:GradeModel, rowIndex, content) {
  
      this.selectedRowData=row;
      
      this.gradeForm.get('id').setValue(row.id);
      this.gradeForm.get('name').setValue(row.name);
      this.gradeForm.get('gradeSubjects').setValue(row.gradeSubjects);
      if(row.levelHeadId>0)
      {
        this.gradeForm.get('levelHeadId').setValue(row.levelHeadId);
      }
      else
      {
        this.gradeForm.get('levelHeadId').setValue(null);
      }


      this.modalService.open(content, {
        ariaLabelledBy: 'modal-basic-title',
        size: 'lg',
      });
    }
  

  save()
  {
    this.spinner.show();
    this.gradeService.saveGradeDetail(this.gradeForm.getRawValue())
      .subscribe(response=>{
        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });
          this.modalService.dismissAll();
          this.loadGrades();;
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
    this.loadGrades();
  }


  get gradeId()
  {
    return this.gradeForm.get("id").value;
  }

}
