import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { AttendanceFilterModel } from 'src/app/models/attendance/attendance.filter.model';
import { BasicAttendanceModel } from 'src/app/models/attendance/basic.attendance.model';
import { StudentAttendanceModel } from 'src/app/models/attendance/student.attendance.model';
import { SubjectAttendaceModel } from 'src/app/models/attendance/subject.attendace.model';
import { DropDownModel } from 'src/app/models/common/drop.down.modal';
import { AttendanceService } from 'src/app/services/attendance.service';
import { DropdownService } from 'src/app/services/dropdown.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-subject-attendance',
  templateUrl: './subject-attendance.component.html',
  styles: ['::ng-deep .p-calendar .p-inputtext {flex: 0 0 auto;width: 100%;margin-top: -11px;    margin-left: -16px;margin-right: -16px;}']
})
export class SubjectAttendanceComponent implements OnInit {
  id:number=0;
  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  classes:DropDownModel[]=[];
  subjects:DropDownModel[]=[];
  softwares=[{id:'Zoom',name:'Zoom'},{id:'Microsoft Team',name:'Microsoft Team'},{id:'Google Meet',name:'Google Meet'},{id:'Skype',name:'Skype'}];
  classTypes=[{id:false,name:'No'},{id:true,name:'Yes'}];
  filter:AttendanceFilterModel = new AttendanceFilterModel();
  filterForm:FormGroup;

  data:StudentAttendanceModel[]=[];

  date:Date;

  constructor(private attendanceService:AttendanceService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    public activateRoute: ActivatedRoute,
    private router: Router,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) {

      this.date= new Date();
      this.createFilterForm();
     }

  ngOnInit(): void {
    this.activateRoute.params.subscribe(params => {
      this.id = +params.id;
      if(this.id==0)
      {
  
        this.spinner.show();
        this.loadGrades();
      }
      else
      {
        this.spinner.show();
        this.generateExistingAttendanceSheet();
      }

    });


  }

  createFilterForm() {
    this.filterForm = this.formBuilder.group({
   
      id:new FormControl(this.id),
      year:new FormControl(this.date.getFullYear()),
      month:new FormControl(this.date.getMonth()+1),
      day:new FormControl(this.date.getDate()),
      gradeId:new FormControl(0),
      classId: new FormControl(0),
      subjectId:new FormControl(0),
      startHour:new FormControl(0),
      startMin:new FormControl(0),
      endHour:new FormControl(0),
      endMin:new FormControl(0),
      isExtraClass:new FormControl(false),
      lessonDetails:new FormControl("",Validators.required),
      softwareName:new FormControl("Zoom"),
      timeSlotId:new FormControl(0),
      studentsAttendance: this.formBuilder.array([]),

      selectedDate: new FormControl(this.date),
      startTime:new FormControl(this.date), 
      endTime:new FormControl(this.date)
    });
  }

  generateExistingAttendanceSheet()
  {
    this.attendanceService.getAttendanceDetailForSubjectClassById(this.id)
    .subscribe(response=>
    {

      this.spinner.hide();

      this.data= response.studentsAttendance;
      this.date = new Date(response.year,response.month-1,response.day,0,0,0);

      let startTime = new Date(response.year,response.month-1,response.day,response.startHour,response.startMin,0);
      let endTime = new Date(response.year,response.month-1,response.day,response.endHour,response.endMin,0);

      this.filterForm = this.formBuilder.group({
   
        id:new FormControl(this.id),
        year:new FormControl(this.date.getFullYear()),
        month:new FormControl(this.date.getMonth()-1),
        day:new FormControl(this.date.getDate()),
        gradeId:new FormControl(response.gradeId),
        classId: new FormControl(response.classId),
        subjectId:new FormControl(response.subjectId),
        startHour:new FormControl(response.startHour),
        startMin:new FormControl(response.startMin),
        endHour:new FormControl(response.endHour),
        endMin:new FormControl(response.endMin),
        isExtraClass:new FormControl(response.isExtraClass),
        lessonDetails:new FormControl(response.lessonDetails,Validators.required),
        softwareName:new FormControl(response.softwareName),
        timeSlotId:new FormControl(response.timeSlotId),
        studentsAttendance: this.formBuilder.array([]),
  
        selectedDate: new FormControl(this.date),
        startTime:new FormControl(startTime), 
        endTime:new FormControl(endTime)
      });

      this.spinner.show();
      this.loadGrades();

    },error=>{
      this.spinner.hide();
    })

    this.filterForm.controls["selectedDate"].disable();
    this.filterForm.get("gradeId").disable();
    this.filterForm.get("classId").disable();
    this.filterForm.get("subjectId").disable();
  }

  loadGrades()
  {

    this.dropdownService.getTeachGradesForLoggedInUser(this.date.getFullYear())
      .subscribe(response=>{
        this.grades=response;
        if(this.grades.length>0)
        {
          if(this.id==0)
          {
            this.filterForm.get("gradeId").setValue(this.grades[0].id);
          }

          this.loadClasses();
        }
        this.spinner.hide();

      },error=>{
        this.spinner.hide();
      })
  }

  loadClasses()
  {
      this.dropdownService.getAssignedClassForLoggedInUser(this.date.getFullYear(),this.gradeId)
        .subscribe(respone=>{
          this.classes= respone;
          if(this.classes.length>0)
          {
            if(this.id==0)
            {
              this.filterForm.get("classId").setValue(this.classes[0].id);
            }

            this.loadSubject();
          }
          this.spinner.hide();
        },error=>{
          this.spinner.hide();
        });
  }

  loadSubject()
  {
    this.dropdownService.getAssignedClassSubjectForLoggedInUser(this.classId)
    .subscribe(respone=>{
      this.subjects= respone;
      if(this.subjects.length>0)
      {
        if(this.id==0)
        {
          this.filterForm.get("subjectId").setValue(this.subjects[0].id);
          this.setStartDateEndDate();
        }

      }
      this.spinner.hide();
    },error=>{
      this.spinner.hide();
    });
  }

  onDateChanged(item:any)
  {
    this.spinner.show();
    this.filterForm.get("year").setValue(this.selectedDate.getFullYear());
    this.filterForm.get("month").setValue(this.selectedDate.getMonth()+1);
    this.filterForm.get("day").setValue(this.selectedDate.getDate());
    this.setStartDateEndDate();
  }

  onStartTimeChanged(item:any)
  {
    this.filterForm.get("startHour").setValue(this.startTime.getHours());
    this.filterForm.get("startMin").setValue(this.startTime.getMinutes());

  }

  onEndTimeChanged(item:any)
  {
    this.filterForm.get("endHour").setValue(this.endTime.getHours());
    this.filterForm.get("endMin").setValue(this.endTime.getHours());
  }

  onGradeFilterChanged(item:any)
  {
    this.spinner.show();
    this.loadClasses();
  }

  onClassFilterChanged(item:any)
  {
    this.spinner.show();
    this.loadSubject();
  }

  onSubjectChanged(item:any)
  {
    this.spinner.show();
    this.setStartDateEndDate();
  }

  setStartDateEndDate()
  {
    let filter:AttendanceFilterModel = new AttendanceFilterModel();
    filter.classId=this.classId;
    filter.subjectId=this.subjectId;
    filter.year= this.selectedDate.getFullYear();
    filter.month = this.selectedDate.getMonth()+1;
    filter.day = this.selectedDate.getDate();

    this.attendanceService.getStartAndEndTime(filter)
      .subscribe(response=>{
        let startTime = new Date(response.year,response.month-1,response.day,response.startHour,response.startMin,0);
        let endTime = new Date(response.year,response.month-1,response.day,response.endHour,response.endMin,0);

        this.filterForm.get("startHour").setValue(response.startHour);
        this.filterForm.get("startMin").setValue(response.startMin);
        this.filterForm.get("endHour").setValue(response.endHour);
        this.filterForm.get("endMin").setValue(response.endMin);
        this.filterForm.get("startTime").setValue(startTime);
        this.filterForm.get("endTime").setValue(endTime); 

        this.loadSubjectAttendance();
      },error=>{
        this.spinner.hide();
      });
  }

  loadSubjectAttendance()
  {
    this.attendanceService.getAttendanceDetailForClassSubject(this.filterForm.getRawValue())
      .subscribe(response=>{
        if(response.id>0)
        {
          this.filterForm.get("id").setValue(response.id);
          this.filterForm.get("lessonDetails").setValue(response.lessonDetails);
          this.filterForm.get("softwareName").setValue(response.softwareName);
          this.filterForm.get("isExtraClass").setValue(response.isExtraClass);
        }

        this.filterForm.get("timeSlotId").setValue(response.timeSlotId);
        this.data= response.studentsAttendance;
        this.spinner.hide();

      },error=>{
        this.spinner.hide();
      });
  }

  save()
  {
    let subjectAttendance:SubjectAttendaceModel= this.filterForm.getRawValue();
    subjectAttendance.studentsAttendance = this.data;

    this.attendanceService.saveAttendanceDetailForClassSubject(subjectAttendance)
      .subscribe(response=>{

        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });
          this.router.navigate(['/attendance/attendance-list']);
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
      });
  }

  reset()
  {

  }

  get selectedDate():Date
  {
    return this.filterForm.get("selectedDate").value;
  }

  get startTime():Date
  {
    return this.filterForm.get("startTime").value;
  }

  get endTime():Date
  {
    return this.filterForm.get("endTime").value;
  }

  get gradeId()
  {
    return this.filterForm.get("gradeId").value;
  }

  get classId()
  {
    return this.filterForm.get("classId").value;
  }

  get subjectId()
  {
    return this.filterForm.get("subjectId").value;
  }

  get studentArray(): FormArray {
    return this.filterForm.get('studentsAttendance') as FormArray;
  }


}
