import { Injectable } from '@angular/core';

@Injectable()
export class MasterDataFileValidateResult
{
    isSuccess:boolean;
    validateMessage:string;
}