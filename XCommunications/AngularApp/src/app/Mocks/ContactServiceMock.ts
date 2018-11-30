import { Injectable } from '@angular/core';
import {AddNumberComponent} from '../Number/add-number/add-number.component';
import {Contract} from '../Models/contract';
@Injectable()
export class ContactServiceMock{
    constructor() {}

    
    post(sim:Contract):Array<{}>{
        return [{
            id: 0,
             customerId: 0,
             workerId: 0,
             tarif: ""
        }];
    }
      getAllContract(): Array<{}> {
          return [{
            id: 0,
            customerId: 0,
            workerId: 0,
            tarif: ""
          }];
      }
    
      deleteContract(id: number) {
        return [{
            id: 0,
            customerId: 0,
            workerId: 0,
            tarif: ""
        }]
      }
    
      updateContract(contact: Contract) {
        return [{
            id: 0,
            customerId: 0,
            workerId: 0,
            tarif: ""
        }];  
    }
    
      
      
}
