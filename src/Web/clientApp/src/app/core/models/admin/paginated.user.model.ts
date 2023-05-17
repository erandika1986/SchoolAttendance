import {PaginatedItemsModel} from '../common/paginated-Items.model'
import { UserModel } from './user.model';
export class PaginatedUserModel extends PaginatedItemsModel
{
    data:UserModel[];
}