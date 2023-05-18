import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DropDownModel } from 'src/app/core/models/common/drop.down.modal';
import { AttendanceService } from 'src/app/core/services/attendance.service';
import { DropdownService } from 'src/app/core/services/dropdown.service';
import { ReportService } from 'src/app/core/services/report.service';


@Component({
  selector: 'app-attendance-reports',
  templateUrl: './attendance-reports.component.html',
  styles: ['::ng-deep .p-calendar .p-inputtext {flex: 0 0 auto;width: 100%;margin-top: -11px;    margin-left: -16px;margin-right: -16px;}']
})
export class AttendanceReportsComponent implements OnInit {

  reports:any[]=[{id:1,name:"Class Week Attendance Report"},{id:2,name:"Subject Attendance Report"},{id:2,name:"Class Weekly Zonal Report"}]
  
  academicYears:DropDownModel[]=[];
  grades:DropDownModel[]=[];
  classes:DropDownModel[]=[];
  subjects:DropDownModel[]=[];

  filterForm:FormGroup;

  constructor(private attendanceService:AttendanceService,
    private reportService:ReportService,
    private router: Router,
    private dropdownService:DropdownService,
    private spinner: NgxSpinnerService) {
    this.filterForm = this.createFilterForm();
   }

  ngOnInit(): void {
    this.getTeacherMasterData();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectedReportId:new FormControl(1),
      fromYear: new FormControl(0),
      fromMonth:new FormControl(0),
      fromDay: new FormControl(0),
      toYear:new FormControl(0),
      toMonth: new FormControl(0),
      toDay: new FormControl(0),
      fromDate:new FormControl(new Date()),
      toDate:new FormControl(new Date()),
      selectedYearId: new FormControl(0),
      selectedGradeId: new FormControl(0),
      selectedClassId: new FormControl(0),
      selectedSubjectId: new FormControl(0)
    });

  }

  getTeacherMasterData()
  {
    this.reportService.getTeacherClassMasterData().subscribe(response=>{

      this.classes =response.classes;
      this.academicYears = response.academicYears;
      this.grades = response.grades;
      this.filterForm.get("selectedYearId").setValue(response.selectedYearId);
      this.filterForm.get("selectedGradeId").setValue(response.selectedGradeId);
      this.filterForm.get("selectedClassId").setValue(response.selectedClassId);

      this.filterForm.get("fromDate").setValue(new Date(response.fromYear,response.fromMonth-1,response.fromDay,0,0,0));
      this.filterForm.get("toDate").setValue(new Date(response.toYear,response.toMonth-1,response.toDay,0,0,0));
      this.setFromandToDate();
      this.filterForm.get("toDate").disable()

      if(response.role!="Admin")
      {
        this.filterForm.get("selectedYearId").disable();
        this.filterForm.get("selectedGradeId").disable();
        this.filterForm.get("selectedClassId").disable();
      }
    },error=>{

    });
  }

  onSelectedReportChanged(item:any)
  {

  }

  onAcademicYearFilterChanged(item:any)
  {

  }

  onGradeFilterChanged(item:any)
  {

  }

  onClassFilterChanged(item:any)
  {

  }

  onSubjectFilterChanged(item:any)
  {

  }

  onFromDateChanged(item:any)
  {
    let newdate = new Date(this.fromDate.getTime() + (1000 * 60 * 60 * 24*4));

    this.toDate.setDate(this.fromDate.getDate() + 4);

    this.filterForm.get("toDate").setValue(newdate); 

    this.setFromandToDate();

  }

  setFromandToDate()
  {
    this.filterForm.get("fromYear").setValue(this.fromDate.getFullYear()); 
    this.filterForm.get("fromMonth").setValue(this.fromDate.getMonth()+1); 
    this.filterForm.get("fromDay").setValue(this.fromDate.getDate()); 
    this.filterForm.get("toYear").setValue(this.toDate.getFullYear()); 
    this.filterForm.get("toMonth").setValue(this.toDate.getMonth()+1); 
    this.filterForm.get("toDay").setValue(this.toDate.getDate()); 
  }

  onToDateChanged(item:any)
  {
    
  }

  downloadPercentage:number=0;
  isDownloading:boolean;
  generateReport()
  {
    this.isDownloading=true;
    this.spinner.show();
    
    this.reportService.downloadClassAttendanceForAllSubjects(this.filterForm.getRawValue())
      .subscribe((response: HttpResponse<Blob>)=>{

     
        
/*         if (response.type === HttpEventType.DownloadProgress) {
          this.downloadPercentage = Math.round(100 * response.loaded / response.total);
        } */
        
        if (response.type === HttpEventType.Response) {
          if(response.status == 204)
          {
            this.isDownloading=false;
            this.downloadPercentage=0;
            this.spinner.hide();
          }
          else
          {
            //let headers =response.headers;
            let contentDisposition = response.headers.get('content-disposition');
            const objectUrl: string = URL.createObjectURL(response.body);
            const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
    
            a.href = objectUrl;
            a.download = this.parseFilenameFromContentDisposition(contentDisposition);
            document.body.appendChild(a);
            a.click();
    
            document.body.removeChild(a);
            URL.revokeObjectURL(objectUrl);
            this.isDownloading=false;
            this.downloadPercentage=0;
            this.spinner.hide();
          }

        }




      },error=>{
        this.spinner.hide();
        this.isDownloading=false;
        this.downloadPercentage=0;
      });
  }



 parseFilenameFromContentDisposition(contentDisposition) {
    if (!contentDisposition) return null;
    let matches = /filename="(.*?)"/g.exec(contentDisposition);

    return matches && matches.length > 1 ? matches[1] : null;
  }

  get toDate():Date
  {
    return  this.filterForm.get("toDate").value;
  }

  get fromDate():Date{
    return this.filterForm.get("fromDate").value;
  }

}
