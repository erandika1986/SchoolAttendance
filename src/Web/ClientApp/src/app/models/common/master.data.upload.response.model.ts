import { Injectable } from '@angular/core';
import { MasterDataFileValidateResult } from './master.datafile.validate.result.model';

@Injectable()
export class MasterDataUploadResponse
{
    isSucccess:boolean;
    results:MasterDataFileValidateResult[];
}