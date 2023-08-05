import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssessmentListComponent } from './assessment-list/assessment-list.component';
import { AssessmentResultComponent } from './assessment-result/assessment-result.component';



@NgModule({
  declarations: [
    AssessmentListComponent,
    AssessmentResultComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AssessmentModule { }
