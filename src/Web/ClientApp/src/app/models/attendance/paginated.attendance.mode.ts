import { PaginatedItemsModel } from "../common/paginated-Items.model";
import { BasicAttendanceModel } from "./basic.attendance.model";

export class PaginatedAttendanceModel extends PaginatedItemsModel
{
    data:BasicAttendanceModel[];
}