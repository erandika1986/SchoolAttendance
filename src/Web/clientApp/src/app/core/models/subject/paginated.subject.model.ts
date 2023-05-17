import { PaginatedItemsModel } from "../common/paginated-Items.model";
import { SubjectModel } from "./subject.model";


export class PaginatedSubjectModel extends PaginatedItemsModel
{
    data:SubjectModel[];
}