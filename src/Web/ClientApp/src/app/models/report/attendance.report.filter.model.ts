import { Injectable } from "@angular/core";

@Injectable()
export class AttendanceReportFilter
{
    fromYear:number;
    fromMonth:number;
    fromDay:number;
    toYear:number;
    toMonth:number;
    toDay:number;
    selectedYearId:number;
    selectedGradeId:number;
    selectedClassId:number;
    selectedSubjectId:number;
}