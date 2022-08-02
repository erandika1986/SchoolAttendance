import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubjectAttendanceComponent } from './subject-attendance/subject-attendance.component';
import { SubjectAttendanceRoutingModule } from './subject-attendance-routing.module';
import { AttendanceReportsComponent } from './attendance-reports/attendance-reports.component';
import { SubjectAttendanceListComponent } from './subject-attendance-list/subject-attendance-list.component';
import { ProgressBarModule } from 'primeng/progressbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import {CalendarModule} from 'primeng/calendar';
import {InputSwitchModule} from 'primeng/inputswitch';
import {TableModule} from 'primeng/table';


import {ButtonModule} from 'primeng/button';



@NgModule({
  declarations: [
    SubjectAttendanceComponent,
    AttendanceReportsComponent,
    SubjectAttendanceListComponent
  ],
  imports: [
    CommonModule,
    ProgressBarModule,
    FormsModule,
    ReactiveFormsModule,
    CalendarModule,
    InputSwitchModule,
    ButtonModule,
    TableModule,
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right'
    }),
    SubjectAttendanceRoutingModule
  ]
})
export class AttendanceModule { }
