import { Injectable } from "@angular/core";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";

@Injectable()
export class LessonLectureModel
{
    id:number;
    topicId:number;
    name:string;
    contentType:number;
    mimeType:string;
    content:string;
    editable:boolean;

    youtubeLink:string;
    isuploading:boolean;
    uploadPrecentage:number;
    

    static asFormGroup(item:LessonLectureModel,isDisable:boolean,sanitizer: DomSanitizer): FormGroup
    {
        const fg = new FormGroup({
            id: new FormControl(item.id),
            topicId: new FormControl(item.topicId),
            name: new FormControl(item.name,[Validators.required]),
            contentType: new FormControl(item.contentType,[Validators.required]),
            mimeType: new FormControl(item.mimeType),
            content: new FormControl(item.content,[Validators.required]),
            editable: new FormControl(item.editable),
            isuploading:new FormControl(false),
            uploadPrecentage:new FormControl(0),
            youtubeLink:new FormControl(null)
        });

        if(item.contentType==4)
        {
            fg.get("youtubeLink").setValue(sanitizer.bypassSecurityTrustResourceUrl(item.content));
            //(fg.get("youtubeLink") as FormArray).setValue(sanitizer.bypassSecurityTrustResourceUrl(item.content));
        }

        if(isDisable)
        {
            fg.get("name").disable();
            fg.get("contentType").disable();
            fg.get("content").disable();
        }

        return fg;
    }
}