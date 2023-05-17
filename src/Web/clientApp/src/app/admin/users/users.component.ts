import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { UserModel } from 'src/app/core/models/admin/user.model';
import { CheckBoxModel } from 'src/app/core/models/common/check.box.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.modal';
import { DropdownService } from 'src/app/core/services/dropdown.service';
import { UserService } from 'src/app/core/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {

  rows = [];
  newUserImg = 'assets/images/users/user-2.png';
  scrollBarHorizontal = window.innerWidth < 1200;
  loadingIndicator = true;
  reorderable = true;
  selectedRowData: UserModel;


  genders =[{id:"M",name:"Male"},{id:"F",name:"Female"}];

  subjects:CheckBoxModel[]=[];
  userRoles:DropDownModel[]=[];

  filterForm:FormGroup;
  userForm:FormGroup;
  passwordUpdateForm:FormGroup;

  data = new Array<UserModel>();

  currentPage:number=1;
  pageSize:number=45;
  totalRecord:number=0;
  loading: boolean;

  constructor(private userService:UserService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.filterForm = this.createFilterForm();
    this.userForm = this.createNewUserForm();
    this.passwordUpdateForm = this.createUpdatePasswordForm();
    this.spinner.show();
    this.loadSystemRole();

  }

  createFilterForm(): FormGroup {
    return new FormGroup({
      searchText: new FormControl("")
    });
  }

  createNewUserForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      fullName: ['', Validators.required],
      gender: [null, Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      role:['Teacher'],
      timeZoneId:['Sri Lanka Standard Time'],
      assignedSubjects:[null],
      assignedRoles:[null]
    });
  }

  createUpdatePasswordForm():FormGroup{
    return this.formBuilder.group({
      id: [0, Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  filterDatatable(event) {
    // get the value of the key pressed and make it lowercase
    const val = event.target.value.toLowerCase();
    this.spinner.show();
    console.log('xxxx');
    this.loadUsers();

  }

  loadSystemRole()
  {
    this.dropdownService.getAllSystemRoles()
      .subscribe(response=>{
        this.userRoles= response;
        this.loadSubjects();
      },error=>{
        this.spinner.hide();
      });
  }

  loadSubjects()
  {
    this.dropdownService.getAllSubjects().subscribe(response=>{

      this.subjects = response;
      console.log('xxx');
      this.loadUsers();

    },error=>{
      this.spinner.hide();
    })
  }

  loadUsers()
  {
    this.userService.getUsersList(this.searchFilterText,this.currentPage,this.pageSize)
      .subscribe(response=>{
        this.data=response.data;
        console.log(this.data);
        
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
    console.log('xxxxx');
    this.loadUsers();
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
    console.log('xxx222');
    this.loadUsers();
  }


  get searchFilterText()
  {
    return this.filterForm.get("searchText").value;
  }


  //For Add/Edit view

      // add new record
      addRow(content) {

        this.selectedRowData=null;

        this.userForm.get('id').setValue(0);
        this.userForm.get('fullName').setValue('');
        this.userForm.get('gender').setValue(null);
        this.userForm.get('username').setValue('');
        this.userForm.get('password').setValue('');
        this.userForm.get('role').setValue('Teacher');
        this.userForm.get('timeZoneId').setValue('Sri Lanka Standard Time');
        this.userForm.get('password').setValidators([Validators.required]);
        this.userForm.get('password').updateValueAndValidity();
        this.userForm.get('assignedSubjects').setValue([]);
        this.userForm.get('assignedRoles').setValue([]);
        this.modalService.open(content, {
          ariaLabelledBy: 'modal-basic-title',
          size: 'lg',
        });
  
      }
    
  
    editRow(row:UserModel, rowIndex, content) {
  
      if(row.id>0)
      {
        this.userForm.get('password').clearValidators();
        this.userForm.get('password').updateValueAndValidity();
      }

      console.log('====================================');
      console.log(row);
      console.log('====================================');

      this.selectedRowData = row;
      this.spinner.show();
      this.userService.getUserById(row.id).subscribe(response=>{

        this.userForm.get('id').setValue(response.id);
        this.userForm.get('fullName').setValue(response.fullName);
        this.userForm.get('gender').setValue(response.gender);
        this.userForm.get('username').setValue(response.username);
        this.userForm.get('assignedRoles').setValue(response.assignedRoles);
        this.userForm.get('timeZoneId').setValue(response.timeZoneId);
        this.userForm.get('timeZoneId').setValue(response.timeZoneId);
        this.userForm.get('assignedSubjects').setValue(response.assignedSubjects)

        this.spinner.hide();
      },error=>{
        this.spinner.hide();
      })

      this.modalService.open(content, {
        ariaLabelledBy: 'modal-basic-title',
        size: 'lg',
      });




  
  
    }
  

  deleteSelected(row:UserModel) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#868a87',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {

        this.userService.deleteUser(row.id).subscribe(response=>{
          if(response.isSuccess)
          {
            Swal.fire({
              icon: 'success',
              title: 'Done',
              text: response.message,
            });

            this.spinner.show();
            this.loadUsers();
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
    this.userService.saveUser(this.userForm.getRawValue())
      .subscribe(response=>{
        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });
          this.modalService.dismissAll();
          this.loadUsers();
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
  get userId()
  {
    return this.userForm.get("id").value;
  }

}
