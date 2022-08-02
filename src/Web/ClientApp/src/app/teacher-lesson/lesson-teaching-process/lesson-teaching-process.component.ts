import { Component, OnInit } from '@angular/core';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'lesson-teaching-process',
  templateUrl: './lesson-teaching-process.component.html',
  styleUrls: ['./lesson-teaching-process.component.sass']
})
export class LessonTeachingProcessComponent implements OnInit {

  public Editor = ClassicEditor;
  
  constructor() { }

  ngOnInit(): void {
  }

}
