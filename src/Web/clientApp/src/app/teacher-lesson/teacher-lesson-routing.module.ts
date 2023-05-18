import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LessonDesignListComponent } from './lesson-design-list/lesson-design-list.component';
import { LessonContainerComponent } from './lesson-container/lesson-container.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'lessons-in-design',
    pathMatch: 'full',
  },
  {
    path: 'lessons-in-design',
    component: LessonDesignListComponent,
  },
  {
    path: 'lessons-in-design/:id',
    component: LessonContainerComponent,
  }

];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class TeacherLessonRoutingModule { }
