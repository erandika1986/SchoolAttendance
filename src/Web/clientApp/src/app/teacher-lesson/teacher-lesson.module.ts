import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LessonDesignListComponent } from './lesson-design-list/lesson-design-list.component';
import { LessonContainerComponent } from './lesson-container/lesson-container.component';
import { LessonDetailsComponent } from './lesson-details/lesson-details.component';
import { LessonPrerequisitesComponent } from './lesson-prerequisites/lesson-prerequisites.component';
import { LessonLearningOutcomeComponent } from './lesson-learning-outcome/lesson-learning-outcome.component';
import { LessonTopicComponent } from './lesson-topic/lesson-topic.component';
import { LessonLectureComponent } from './lesson-lecture/lesson-lecture.component';
import { TeacherLessonRoutingModule } from './teacher-lesson-routing.module';
import { ProgressBarModule } from 'primeng/progressbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputSwitchModule } from 'primeng/inputswitch';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import {ButtonModule} from 'primeng/button';
import { ToastrModule } from 'ngx-toastr';
import {ListboxModule} from 'primeng/listbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { LessonContentComponent } from './lesson-content/lesson-content.component';
import { LessonTeachingProcessComponent } from './lesson-teaching-process/lesson-teaching-process.component';
import { MultiSelectModule } from 'primeng/multiselect';
import { EditorModule } from 'primeng/editor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { PanelModule } from 'primeng/panel';
import { ToastModule } from 'primeng/toast';
import { MenuModule } from 'primeng/menu';
import {DialogModule} from 'primeng/dialog';
import { LessonTestComponent } from './lesson-test/lesson-test.component';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';
import { TagModule } from 'primeng/tag';
import {TooltipModule} from 'primeng/tooltip';
import {FileUploadModule} from 'primeng/fileupload';
import { ProgressBar} from 'primeng/progressbar';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {CheckboxModule} from 'primeng/checkbox';
import { BorderDirective } from '../core/directives/border.directive';


@NgModule({
  declarations: [
    LessonDesignListComponent,
    LessonContainerComponent,
    LessonDetailsComponent,
    LessonPrerequisitesComponent,
    LessonLearningOutcomeComponent,
    LessonTopicComponent,
    LessonLectureComponent,
    LessonContentComponent,
    LessonTeachingProcessComponent,
    LessonTestComponent,
    BorderDirective
  ],
  imports: [
    CommonModule,
    TeacherLessonRoutingModule,
    CommonModule,
    ProgressBarModule,
    ProgressSpinnerModule,
    FormsModule,
    MultiSelectModule,
    ReactiveFormsModule,
    RxReactiveFormsModule,
    CalendarModule,
    EditorModule,
    InputSwitchModule,
    TableModule,
    ListboxModule,
    ButtonModule,
    CKEditorModule,
    DialogModule,
    NgbModule,
    PanelModule,
    ToastModule,
    MenuModule,
    TagModule,
    TooltipModule,
    FileUploadModule,
    CheckboxModule,
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right'
    }),
    
  ]
})
export class TeacherLessonModule { }
