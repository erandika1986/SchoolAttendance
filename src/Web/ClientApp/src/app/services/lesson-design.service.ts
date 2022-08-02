import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, Subject, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseModel } from '../models/common/response.model';
import { LessonLectureModel } from '../models/lesson/lesson.lecture.model';
import { LessonModel } from "../models/lesson/lesson.model";
import { LessonTopicModel } from '../models/lesson/lesson.topic.model';
import { LessonUnitTestModel } from '../models/lesson/unit-test/lesson.unit.test.model';
import { LessonFilter } from "../models/lesson/lesson.filter";
import { PaginatedLessonModel } from "../models/lesson/paginated.lesson.model";
import { LessonListFilterMasterData } from "../models/lesson/lesson.list.filter.masterdata.model";
import { LessonDetailModel } from '../models/lesson/lesson.detail.model';
import { LessonPrerequisiteForm } from '../models/lesson/lesson.prerequisite.form.model';
import { LessonOutcomeForm } from '../models/lesson/lesson.outcome.form.model';
import { upload, Upload } from '../models/common/upload';
import { LessonUnitTestTopicModel } from '../models/lesson/unit-test/lesson.unit.test.topic.model';

@Injectable({
  providedIn: 'root'
})
export class LessonDesignService {

  onLessonValueAssigned:Subject<any>;
  onLessonTestCheckBoxChanged:Subject<boolean>;
  constructor(private httpClient: HttpClient) 
  { 
    this.onLessonValueAssigned = new Subject();
    this.onLessonTestCheckBoxChanged = new Subject();
  }

  getLessonDesignDropdownMasterData(): Observable<LessonListFilterMasterData> {
    return this.httpClient.get<LessonListFilterMasterData>(environment.apiUrl +'LessonDesign/getLessonDesignDropdownMasterData' );
  }

  createNewLesson(): Observable<LessonModel> {
    return this.httpClient.post<LessonModel>(environment.apiUrl +'LessonDesign/createNewLesson',null);
  }

  getLessonById(id:number): Observable<LessonModel> {
    return this.httpClient.get<LessonModel>(environment.apiUrl +'LessonDesign/getLessonById/'+ id );
  }

  saveLesson(vm:LessonModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLesson',vm);
  }

  saveLessonDetail(vm:LessonDetailModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLessonDetail',vm);
  }

  saveLessonPrerequisite(vm:LessonPrerequisiteForm): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLessonPrerequisite',vm);
  }

  saveLessonLearningOutcome(vm:LessonOutcomeForm): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLessonLearningOutcome',vm);
  }

  createNewLessonTopic(lessonId:number): Observable<LessonTopicModel> {
    return this.httpClient.post<LessonTopicModel>(environment.apiUrl +'LessonDesign/createNewLessonTopic',lessonId);
  }

  saveLessonTopic(vm:LessonTopicModel): Observable<LessonTopicModel> {
    return this.httpClient.post<LessonTopicModel>(environment.apiUrl +'LessonDesign/saveLessonTopic',vm);
  }

  saveLessonTopicName(vm:LessonTopicModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLessonTopicName',vm);
  }

  createNewLecture(topicId:number): Observable<LessonLectureModel> {
    return this.httpClient.post<LessonLectureModel>(environment.apiUrl +'LessonDesign/createNewLecture',topicId);
  }

  saveLessonLectureName(vm:LessonLectureModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/saveLessonLectureName',vm);
  }

  saveLessonLectureContent(vm:LessonLectureModel): Observable<LessonLectureModel> {
    return this.httpClient.post<LessonLectureModel>(environment.apiUrl +'LessonDesign/saveLessonLectureContent',vm);
  }

  saveLessonLecture(vm:LessonLectureModel): Observable<LessonLectureModel> {
    return this.httpClient.post<LessonLectureModel>(environment.apiUrl +'LessonDesign/saveLessonLecture',vm);
  }

  saveLessonUnitTest(vm:LessonUnitTestModel): Observable<LessonUnitTestModel> {
    return this.httpClient.post<LessonUnitTestModel>(environment.apiUrl +'LessonDesign/saveLessonUnitTest',vm);
  }

  saveUnitTestDetail(vm:LessonUnitTestModel): Observable<LessonUnitTestModel> {
    return this.httpClient.post<LessonUnitTestModel>(environment.apiUrl +'LessonDesign/saveUnitTestDetail',vm);
  }

  saveLessonUnitTestTopic(vm:LessonUnitTestTopicModel): Observable<LessonUnitTestTopicModel> {
    return this.httpClient.post<LessonUnitTestTopicModel>(environment.apiUrl +'LessonDesign/saveLessonUnitTestTopic',vm);
  }

  copyLesson(id:number): Observable<LessonUnitTestModel> {
    return this.httpClient.post<LessonUnitTestModel>(environment.apiUrl +'LessonDesign/copyLesson',id);
  }

  deleteLesson(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'LessonDesign/deleteLesson/'+id);
  }

  deleteTopic(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'LessonDesign/deleteTopic/'+id);
  }

  deleteLecture(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'LessonDesign/deleteLecture/'+id);
  }

  deleteLectureContent(id:number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(environment.apiUrl +'LessonDesign/deleteLectureContent/'+id);
  }

  getNotPublishedLesson(filter:LessonFilter): Observable<PaginatedLessonModel> {
    return this.httpClient.post<PaginatedLessonModel>(environment.apiUrl +'LessonDesign/getNotPublishedLesson',filter);
  }

  publishLesson(id:number): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl +'LessonDesign/publishLesson',id);
  }

  uploadLessonFile(data: FormData): Observable<any> {
    return this.httpClient.post(environment.apiUrl + 'LessonDesign/uploadLessonFile', data,{reportProgress: true,observe: 'events'}).pipe(catchError(this.errorMgmt));;
  }

  errorMgmt(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
