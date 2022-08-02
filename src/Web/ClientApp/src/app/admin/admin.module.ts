import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { ClassComponent } from './class/class.component';
import { GradeComponent } from './grade/grade.component';
import { StudentComponent } from './student/student.component';
import { ExcelUploadComponent } from './excel-upload/excel-upload.component';
import { AdminRoutingModule } from './admin-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { MultiSelectModule } from 'primeng/multiselect';
import { SubjectComponent } from './subject/subject.component';


import {TableModule} from 'primeng/table';
import { ProgressBarModule } from 'primeng/progressbar';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CalendarModule } from 'primeng/calendar';
import {ButtonModule} from 'primeng/button';



@NgModule({
  declarations: [
    UsersComponent,
    ClassComponent,
    GradeComponent,
    StudentComponent,
    ExcelUploadComponent,
    SubjectComponent
  ],
  imports: [
    CommonModule,
    ProgressBarModule,
    FormsModule,
    ReactiveFormsModule,
    MultiSelectModule,
    InputSwitchModule,
    TableModule,
    ButtonModule,
    CalendarModule,
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right'
    }),
    AdminRoutingModule
  ]
})
export class AdminModule { }
