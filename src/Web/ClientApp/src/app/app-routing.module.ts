import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthLayoutComponent } from './layout/app-layout/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layout/app-layout/main-layout/main-layout.component';
import { AuthGuard } from './core/guard/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: '/authentication/signin', pathMatch: 'full' },
      {
        path: 'admin',
        loadChildren: () =>
          import('./admin/admin.module').then((m) => m.AdminModule),
      },
      {
        path: 'attendance',
        loadChildren: () =>
          import('./attendance/attendance.module').then((m) => m.AttendanceModule),
      },
      {
        path: 'teacher-lessons',
        loadChildren: () =>
          import('./teacher-lesson/teacher-lesson.module').then((m) => m.TeacherLessonModule),
      },
    ]   
  },
  {
    path: 'authentication',
    component: AuthLayoutComponent,
    loadChildren: () =>
      import('./authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
