import { PaginatedItemsModel } from "../common/paginated-Items.model";
import { BasicStudentModel } from "./basic.student.model";


export class PaginatedStudentModel extends PaginatedItemsModel
{
    data:BasicStudentModel[];
}