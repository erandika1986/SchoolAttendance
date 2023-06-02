import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MegaMenuItem, MenuItem, PrimeNGConfig } from 'primeng/api';
import { LessonLearningOutcomeModel } from 'src/app/core/models/lesson/lesson.learning.outcome.model';
import { LessonModel } from 'src/app/core/models/lesson/lesson.model';
import { LessonPrerequisiteModel } from 'src/app/core/models/lesson/lesson.prerequisite.model';
import { LessonTopicModel } from 'src/app/core/models/lesson/lesson.topic.model';
import { LessonUnitTestTopicModel } from 'src/app/core/models/lesson/unit-test/lesson.unit.test.topic.model';
import { LessonDesignService } from 'src/app/core/services/lesson-design.service';



@Component({
  selector: 'app-lesson-container',
  templateUrl: './lesson-container.component.html',
  styleUrls: ['./lesson-container.component.scss']
})
export class LessonContainerComponent implements OnInit {

  className="dirtySection";
  menuItems: LessonMenu[];
  selectedMenu:LessonMenu;
  lessonId:number=0;
  lesson:LessonModel = new LessonModel();
  lessonForm:FormGroup;

  isDisable:boolean=false;

  constructor(
    public activateRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private primengConfig: PrimeNGConfig,
    private lessonDesignService:LessonDesignService,
    private fb: FormBuilder,
    private sanitizer: DomSanitizer
    ) { 

      this.lessonForm = this.createLessonForm();
    }

  ngOnInit(): void {

    this.lessonDesignService.onLessonTestCheckBoxChanged.subscribe(value=>{
      this.menuItems[4].visible= value;
    });
    this.primengConfig.ripple = true;
    this.menuItems = [
      {name: 'Lesson Details',id: 1,img:'../../../assets/images/lesson_icon/lesson.png',visible:true},
      {name: 'Prerequisites',id: 2,img:'../../../assets/images/lesson_icon/conditions.png',visible:true},
      {name: 'Learning Outcome',id: 3,img:'../../../assets/images/lesson_icon/goal.png',visible:true},
      {name: 'Lesson Content',id: 4,img:'../../../assets/images/lesson_icon/content.png',visible:true},
      {name: 'Lesson Test',id: 5,img:'../../../assets/images/lesson_icon/question.png',visible:false}
  ];

  this.selectedMenu=this.menuItems[0];

  this.activateRoute.params.subscribe(params => {
    this.lessonId = +params["id"];
    this.spinner.show();
    this.getLesson();

  });
  }

  createLessonForm():FormGroup {
    return new FormGroup({
   
      id: new FormControl(0),
      lessonDetail: this.fb.group({
        lessonId:new FormControl(0),
        name:new FormControl("",[Validators.required]),
        lessonIntroduction: new FormControl("",[Validators.required]),
        duration: new FormControl(0.0),
        competencyLevel: new FormControl(""),
        teachingProcess: new FormControl(""),
        academicYearId: new FormControl(null,[Validators.required]),
        gradeId: new FormControl(null,[Validators.required]),
        subjectId: new FormControl(null,[Validators.required]),
        lessonStatus:new FormControl(null,[Validators.required]),
        teacherAids:new FormControl([],[Validators.required]),
        assignedClasses:new FormControl([],[Validators.required]),
        hasLessonTest:new FormControl(false)
      }),
      lessonPrerequisiteForm:this.fb.group({
        lessonId:new FormControl(0),
        lessonPrerequisites: this.fb.array([])
      }),
      lessonOutcomeForm:this.fb.group({
        lessonId:new FormControl(0),
        lessonOutcomes: this.fb.array([])
      }),
      lessonTopicForm:this.fb.group({
        lessonId:new FormControl(0),
        lessonTopics: this.fb.array([])
      }),
      lessonUnitTest:this.fb.group({
        id:new FormControl(0),
        lessonId:new FormControl(0),
        name:new FormControl(""),
        studentGuide: new FormControl(""),
        topics: this.fb.array([])
      }),
/*       lessonPrerequisites: this.fb.array([]),
      lessonOutcomes: this.fb.array([]),
      lessonTopics: this.fb.array([]),
      lessonUnitTests: this.fb.array([]) */
    });
  }

  menuClicked(menu:LessonMenu)
  {
    this.selectedMenu=menu;
  }

  getLesson()
  {
    this.lessonDesignService.getLessonById(this.lessonId)
      .subscribe(response=>{
        this.spinner.hide();
        this.lesson = response;
/*         this.lessonForm = new FormGroup({
   
          id: new FormControl(response.id),
          lessonDetail: this.fb.group({
            name:new FormControl(response.lessonDetail.name),
            lessonIntroduction: new FormControl(response.lessonDetail.lessonIntroduction),
            duration: new FormControl(response.lessonDetail.duration),
            competencyLevel: new FormControl(response.lessonDetail.competencyLevel,[Validators.required]),
            teachingProcess: new FormControl(response.lessonDetail.teachingProcess,[Validators.required]),
            academicYearId: new FormControl(response.lessonDetail.academicYearId,[Validators.required]),
            gradeId: new FormControl(response.lessonDetail.gradeId,[Validators.required]),
            subjectId: new FormControl(response.lessonDetail.subjectId,[Validators.required]),
            lessonStatus:new FormControl(response.lessonDetail.lessonStatus,[Validators.required]),
            teacherAids:new FormControl(response.lessonDetail.teacherAids),
            assignedClasses:new FormControl(response.lessonDetail.assignedClasses),
          }),
          lessonPrerequisites: this.fb.array([]),
          lessonOutcomes: this.fb.array([]),
          lessonTopics: this.fb.array([]),
          lessonUnitTests: this.fb.array([])
        }); */

        this.lessonForm.get("id").setValue(response.id);
        this.lessonForm.get("lessonDetail.lessonId").setValue(response.id);
        this.lessonForm.get("lessonDetail.name").setValue(response.lessonDetail.name);
        this.lessonForm.get("lessonDetail.lessonIntroduction").setValue(response.lessonDetail.lessonIntroduction);
        this.lessonForm.get("lessonDetail.duration").setValue(response.lessonDetail.duration);
        this.lessonForm.get("lessonDetail.competencyLevel").setValue(response.lessonDetail.competencyLevel);
        this.lessonForm.get("lessonDetail.teachingProcess").setValue(response.lessonDetail.teachingProcess);
        this.lessonForm.get("lessonDetail.academicYearId").setValue(response.lessonDetail.academicYearId);
        this.lessonForm.get("lessonDetail.hasLessonTest").setValue(response.lessonDetail.hasLessonTest);
        this.menuItems[4].visible= response.lessonDetail.hasLessonTest;
        this.lessonForm.get("lessonDetail.academicYearId").disable();
        if(response.lessonDetail.gradeId>0)
        {
          this.lessonForm.get("lessonDetail.gradeId").setValue(response.lessonDetail.gradeId);
        }

        if(response.lessonDetail.subjectId>0)
        {
          this.lessonForm.get("lessonDetail.subjectId").setValue(response.lessonDetail.subjectId);
        }


        this.lessonForm.get("lessonDetail.lessonStatus").setValue(response.lessonDetail.lessonStatus);
        this.lessonForm.get("lessonDetail.teacherAids").setValue(response.lessonDetail.teacherAids);
        this.lessonForm.get("lessonDetail.assignedClasses").setValue(response.lessonDetail.assignedClasses);

        this.lessonForm.get("lessonPrerequisiteForm.lessonId").setValue(response.id);
        const lessonPrerequisteform = response.lessonPrerequisiteForm.lessonPrerequisites.map((value, index) => { return LessonPrerequisiteModel.asFormGroup(value, this.isDisable) });
        const lessonPrerequisteArray = new FormArray(lessonPrerequisteform);
        (this.lessonForm.get("lessonPrerequisiteForm") as FormGroup).setControl("lessonPrerequisites", lessonPrerequisteArray);

        this.lessonForm.get("lessonOutcomeForm.lessonId").setValue(response.id);
        const lessonOutcomesform = response.lessonOutcomeForm.lessonOutcomes.map((value, index) => { return LessonLearningOutcomeModel.asFormGroup(value, this.isDisable) });
        const lessonOutcomesArray = new FormArray(lessonOutcomesform);
        (this.lessonForm.get("lessonOutcomeForm") as FormGroup).setControl("lessonOutcomes", lessonOutcomesArray);


        this.lessonForm.get("lessonTopicForm.lessonId").setValue(response.id);

        console.log(response);
        
        const lessonTopicsform = response.lessonTopicForm.lessonTopics.map((value, index) => { return LessonTopicModel.asFormGroup(value, this.isDisable,this.fb,this.sanitizer) });
        
        console.log("xxxx");
        console.log(lessonTopicsform);
        
        
        const lessonTopicsformArray = new FormArray(lessonTopicsform);
        (this.lessonForm.get("lessonTopicForm") as FormGroup).setControl("lessonTopics", lessonTopicsformArray);
        

        this.lessonForm.get("lessonUnitTest.lessonId").setValue(response.id);
        this.lessonForm.get("lessonUnitTest.id").setValue(response.lessonUnitTest.id);
        this.lessonForm.get("lessonUnitTest.name").setValue(response.lessonUnitTest.name);
        this.lessonForm.get("lessonUnitTest.studentGuide").setValue(response.lessonUnitTest.studentGuide);

        const lessonTestform = response.lessonUnitTest.topics.map((value, index) => { return LessonUnitTestTopicModel.asFormGroup(value, this.isDisable,this.fb) });
        const lessonTestformArray = new FormArray(lessonTestform);
        (this.lessonForm.get("lessonUnitTest") as FormGroup).setControl("topics", lessonTestformArray);


        this.lessonDesignService.onLessonValueAssigned.next(true);


      },error=>{
        this.spinner.hide();
      });
  }


  get lessonPrerequisiteForm()
  {
    return this.lessonForm.get('lessonOutcomeForm');
  }
  get lessonName()
  {
    return  this.lessonForm.get("lessonDetail.name").value;
  }
  get IsInValidLessonDetail()
  {
    return (!this.lessonForm.get("lessonDetail").dirty && this.lessonForm.get("lessonDetail").valid)?false:true;
  }

  get IsInValidLessonPrerequisite()
  {
    return (!this.lessonForm.get("lessonPrerequisiteForm").dirty && this.lessonForm.get("lessonPrerequisiteForm").valid)?false:true;
  }

  get IsInValidLessonOutcome()
  {
    return (!this.lessonForm.get("lessonOutcomeForm").dirty && this.lessonForm.get("lessonOutcomeForm").valid)?false:true;
  }

  get IsInValidLessonTopic()
  {
    return (!this.lessonForm.get("lessonTopicForm").dirty && this.lessonForm.get("lessonTopicForm").valid)?false:true;
  }

  get IsInValidLessonUnitTestForm()
  {
    return (!this.lessonForm.get("lessonUnitTest").dirty && this.lessonForm.get("lessonUnitTest").valid)?false:true;
  }
}

interface LessonMenu {
  name: string,
  img:string,
  id:number,
  visible:boolean
}
