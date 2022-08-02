import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from "./users/users.component";
import {StudentComponent} from "./student/student.component";
import {GradeComponent} from "./grade/grade.component";
import { ClassComponent} from "./class/class.component";
import { ExcelUploadComponent} from "./excel-upload/excel-upload.component";
import { SubjectComponent } from './subject/subject.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'teachers',
    pathMatch: 'full',
  },
  {
    path: 'teachers',
    component: UsersComponent,
  },
  {
    path: 'students',
    component: StudentComponent,
  },
  {
    path: 'subject',
    component: SubjectComponent,
  },
  {
    path: 'grades',
    component: GradeComponent,
  },
  {
    path:'class',
    component:ClassComponent
  },
  {
    path:'excel-upload',
    component:ExcelUploadComponent
  }

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class AdminRoutingModule { }
