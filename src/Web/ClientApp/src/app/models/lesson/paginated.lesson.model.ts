import { Injectable } from "@angular/core";
import { PaginatedItemsModel } from "../common/paginated-Items.model";
import { BasicLessonModel } from "./basic.lesson.model";

@Injectable()
export class PaginatedLessonModel extends PaginatedItemsModel
{
    data:BasicLessonModel[];
}