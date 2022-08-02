import { Injectable } from "@angular/core";

@Injectable()
export class DownloadFileModel {
    isGenerationSuccess:string;
    fileData:number;
    fileType:string;
    fileName:string;

}