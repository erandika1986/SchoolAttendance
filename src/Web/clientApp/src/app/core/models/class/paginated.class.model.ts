import { Injectable } from "@angular/core";
import { ClassModel } from "./class.model";
import { PaginatedItemsModel } from "../common/paginated-Items.model";
import { BasicClassDetailModel } from "./basic.class.detail.model";

@Injectable()
export class PaginatedClassModel extends PaginatedItemsModel
{
    data:BasicClassDetailModel[];
}