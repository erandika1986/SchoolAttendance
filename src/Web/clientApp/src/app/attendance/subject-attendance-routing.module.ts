import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import {SubjectAttendanceComponent} from "./subject-attendance/subject-attendance.component";
import {AttendanceReportsComponent} from "./attendance-reports/attendance-reports.component";
import { SubjectAttendanceListComponent } from './subject-attendance-list/subject-attendance-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'attendance-list',
    pathMatch: 'full',
  },
  {
    path: 'attendance-list',
    component: SubjectAttendanceListComponent,
  },
  {
    path: 'attendance-list/:id',
    component: SubjectAttendanceComponent,
  },
  {
    path: 'reports',
    component: AttendanceReportsComponent,
  }

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class SubjectAttendanceRoutingModule { }
