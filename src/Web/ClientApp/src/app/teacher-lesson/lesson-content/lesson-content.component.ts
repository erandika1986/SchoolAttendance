import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { EMPTY, Observable } from 'rxjs';
import { Upload } from 'src/app/models/common/upload';
import { LessonLectureModel } from 'src/app/models/lesson/lesson.lecture.model';
import { LessonTopicModel } from 'src/app/models/lesson/lesson.topic.model';
import { LessonDesignService } from 'src/app/services/lesson-design.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'lesson-content',
  templateUrl: './lesson-content.component.html',
  styleUrls: ['./lesson-content.component.scss']
})
export class LessonContentComponent implements OnInit {

  form!: FormGroup;
  @Input() formGroupName: string;
  lessonTopicForm: FormGroup;

  selectedTopicIndex:number;
  selectedLectureIndex:number;

  displayBasic:boolean;

  constructor(private rootFormGroup: FormGroupDirective,    
    private fb: FormBuilder,private lessonDesignService:LessonDesignService, private spinner: NgxSpinnerService ,private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.lessonTopicForm = this.rootFormGroup.control.get(this.formGroupName) as FormGroup;

    console.log('====================================');
    console.log(this.lessonTopicForm);
    console.log('====================================');
  }

  trackByFn(index, row) {
    return index;
  } 

  addNewLessonTopic()
  {
    this.lessonDesignService.createNewLessonTopic(this.lessonId).subscribe(response=>{

      const fg = new FormGroup({
        id: new FormControl(response.id),
        lessonId: new FormControl(this.lessonId),
        name: new FormControl(response.name,[Validators.required]),
        sequenceNo: new FormControl(response.sequenceNo),
        editable:new FormControl(true),
        lessonLectures:this.fb.array([])
    });
  
    this.lessonTopics().push(fg);

    },error=>{

    })

  }


  onDeleteLessonTopic(rowIndex:number,item:FormGroup): void {  

    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#868a87',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.value) {

        this.spinner.show();
        this.lessonDesignService.deleteTopic(item.get('id').value)
        .subscribe(response=>{
         this.spinner.hide();
         if(response.isSuccess)
         {
           Swal.fire({
             icon: 'success',
             title: 'Done',
             text: response.message,
           });
   
           this.lessonTopics().removeAt(rowIndex); 
         }
         else
         {
           this.spinner.hide();
           Swal.fire({
             icon: 'error',
             title: 'Failed',
             text: response.message,
           });
         }
   
   
        },error=>{
         this.spinner.hide();
         Swal.fire({
           icon: 'error',
           title: 'Failed',
           text: "Network error has been occured. Please try again.",
         });
        });

      }
    });


 
 }  

  lessonTopics(): FormArray {  
    return this.lessonTopicForm.get('lessonTopics') as FormArray;  
 } 

 editLessonTopic(item:FormGroup)
 {
  item.get('editable').setValue(true);
 }

 saveLessonTopic(item:FormGroup)
 {
   this.spinner.show();
   this.lessonDesignService.saveLessonTopicName(item.getRawValue())
      .subscribe(response=>{
        this.spinner.hide();
        if(response.isSuccess)
        {
          Swal.fire({
            icon: 'success',
            title: 'Done',
            text: response.message,
          });

          item.get('editable').setValue(false);
        }
        else
        {
          this.spinner.hide();
          Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: response.message,
          });
        }

      },error=>{
        this.spinner.hide();
        Swal.fire({
          icon: 'error',
          title: 'Failed',
          text: "Network error has been occured. Please try again.",
        });
      })

 }


 lessonLectures(topicIndex:number):FormArray{

  return this.lessonTopics().at(topicIndex).get("lessonLectures") as FormArray;
 }

 addNewLecture(topicIndex:number) {

  this.spinner.show();
  this.lessonDesignService.createNewLecture(this.lessonTopics().at(topicIndex).get('id').value)
    .subscribe(response=>{
      this.spinner.hide();

      const formGroup = new FormGroup({
        id: new FormControl(response.id),
        topicId: new FormControl(this.lessonTopics().at(topicIndex).get('id').value),
        name: new FormControl(response.name,[Validators.required]),
        contentType: new FormControl(0),
        mimeType:new FormControl(''),
        content: new FormControl(''),
        editable:new FormControl(true),
        isuploading:new FormControl(false),
        uploadPrecentage:new FormControl(0),
        youtubeLink:new FormControl(''),
      });

          this.lessonLectures(topicIndex).push(formGroup);
    },error=>{
      this.spinner.hide();
    });
  }

  editLectureTitle(item:FormGroup)
  {
    item.get('editable').setValue(true);
  }

  saveLectureTitle(item:FormGroup)
  {
    this.spinner.show();
    this.lessonDesignService.saveLessonLectureName(item.getRawValue())
       .subscribe(response=>{
         this.spinner.hide();
         if(response.isSuccess)
         {
           Swal.fire({
             icon: 'success',
             title: 'Done',
             text: response.message,
           });
 
           item.get('editable').setValue(false);
         }
         else
         {
           this.spinner.hide();
           Swal.fire({
             icon: 'error',
             title: 'Failed',
             text: response.message,
           });
         }
 
       },error=>{
         this.spinner.hide();
         Swal.fire({
           icon: 'error',
           title: 'Failed',
           text: "Network error has been occured. Please try again.",
         });
       })
  }

  saveLessonLectureContent(item:FormGroup)
  {
    this.spinner.show();
    this.lessonDesignService.saveLessonLectureContent(item.getRawValue())
       .subscribe(response=>{
         this.spinner.hide();

         Swal.fire({
          icon: 'success',
          title: 'Done',
          text: "Lecture content has been saved.",
        });

         if(item.get("contentType").value==4)
         {
           item.get("content").setValue(response.content);
           item.get("youtubeLink").setValue(this.sanitizer.bypassSecurityTrustResourceUrl(response.content))
         }

         item.markAsPristine();
         item.markAsUntouched();
 
       },error=>{
         this.spinner.hide();
         Swal.fire({
           icon: 'error',
           title: 'Failed',
           text: "Network error has been occured. Please try again.",
         });
       })
  }

 lessonLectureLength(topicIndex:number):number
 {
    return this.lessonTopics().at(topicIndex).get("lessonLectures").value.length;
 }

 showModalDialog(topicIndex:number,lectureIndex:number) {

  this.displayBasic = true;
  this.selectedTopicIndex = topicIndex;
  this.selectedLectureIndex = lectureIndex;

 }

 setContentType(contentType:number)
 {
   this.displayBasic = false;
   this.lessonLectures(this.selectedTopicIndex).at(this.selectedLectureIndex).get("contentType").setValue(contentType);
   //item.get("contentType").setValue(contentType);
 }


 onDeleteLessonLecture(topicIndex:number,lectureIndex:number,item:FormGroup): void {  

  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#868a87',
    confirmButtonText: 'Yes, delete it!',
  }).then((result) => {
    if (result.value) {

      this.spinner.show();
      this.lessonDesignService.deleteLecture(item.get('id').value)
      .subscribe(response=>{
       this.spinner.hide();
       if(response.isSuccess)
       {
         Swal.fire({
           icon: 'success',
           title: 'Done',
           text: response.message,
         });
 
         this.lessonLectures(topicIndex).removeAt(lectureIndex); 
       }
       else
       {
         this.spinner.hide();
         Swal.fire({
           icon: 'error',
           title: 'Failed',
           text: response.message,
         });
       }
 
 
      },error=>{
       this.spinner.hide();
       Swal.fire({
         icon: 'error',
         title: 'Failed',
         text: "Network error has been occured. Please try again.",
       });
      });

    }
  });



}  

onDeleteLessonLectureContent(item:FormGroup): void {  

  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#868a87',
    confirmButtonText: 'Yes, delete it!',
  }).then((result) => {
    if (result.value) {

      this.spinner.show();
      this.lessonDesignService.deleteLectureContent(item.get('id').value)
      .subscribe(response=>{
       this.spinner.hide();
       if(response.isSuccess)
       {
         Swal.fire({
           icon: 'success',
           title: 'Done',
           text: response.message,
         });
 
     
         item.get('contentType').setValue(0);
         item.get('mimeType').setValue("");
         item.get('content').setValue("");
         item.get('youtubeLink').setValue("");
       }
       else
       {
         this.spinner.hide();
         Swal.fire({
           icon: 'error',
           title: 'Failed',
           text: response.message,
         });
       }
 
 
      },error=>{
       this.spinner.hide();
       Swal.fire({
         icon: 'error',
         title: 'Failed',
         text: "Network error has been occured. Please try again.",
       });
      });

    }
  });



}  


 uploadedFiles: any[] = [];
 //upload$: Observable<Upload> = EMPTY;
 //precentage:any;
 onBasicUploadAuto(event,item:FormGroup) {

  console.log('====================================');
  console.log(event);
  console.log('====================================');
  if(event.files.length>0)
  {
    let selectedFile = event.files[0];
    item.get('isuploading').setValue(true);
    const formData = new FormData();
    formData.set("id",item.get("id").value.toString());
    formData.set("topicId",item.get("topicId").value.toString());
    formData.set("contentType",item.get("contentType").value.toString());

    formData.append('file', selectedFile, selectedFile.name);
    this.spinner.show();

    this.lessonDesignService.uploadLessonFile(formData).subscribe((event: HttpEvent<any>) =>
      {
        switch (event.type) {
          case HttpEventType.Sent:
            console.log('Request has been made!');
            break;
          case HttpEventType.ResponseHeader:
            console.log('Response header has been received!');
            break;
          case HttpEventType.UploadProgress:
            {
              let progress:number = Math.round(event.loaded / event.total * 100);
              item.get('uploadPrecentage').setValue(progress);
              console.log(`Uploaded! ${progress}%`);
            }
            break;
          case HttpEventType.Response:
            console.log('File successfully uploaded!', event.body);
            item.get('isuploading').setValue(false);
            item.get('contentType').setValue(event.body.contentType);
            item.get('content').setValue(event.body.content);
            item.get('mimeType').setValue(event.body.mimeType);
            item.markAsPristine();
            item.markAsUntouched();

            setTimeout(() => {
              item.get('uploadPrecentage').setValue(0);
            }, 1500);
  
        }

        this.spinner.hide();
      },error=>{
        this.spinner.hide();
        item.get('isuploading').setValue(false);

      });
  }
  //this.messageService.add({severity: 'info', summary: 'Success', detail: 'File Uploaded with Auto Mode'});
}



get lessonId()
{
  return this.form.get("id").value;
}

}
