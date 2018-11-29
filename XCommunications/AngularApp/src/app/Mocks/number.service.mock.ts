import { Injectable } from '@angular/core';
import {AddNumberComponent} from '../Number/add-number/add-number.component';
import {Number} from '../Models/Number';
@Injectable()
export class NumberServiceMock{
    constructor() {}


    post(sim:Number):Array<{}>{
        return [{
            id: 1,
             status: false,
             cc: 123,
             ndc: 123,
             sn: 123456789,
        }];
    }
      getAllNumbers(): Array<{}> {
          return [{
            id: 1,
             status: false,
             cc: 123,
             ndc: 123,
             sn: 123456789,
          }];
      }
    
      deleteNumber(id: number) {
        return [{
            id:0,
            cc:0,
            ndc:0,
            sn:0
        }]
      }
    
      updateNumber(number: Number) {
        return [{
            id: 1,
            status: false,
            cc: 123,
            ndc: 123,
            sn: 123456789,
        }];  
    }
    
      getAvailableNumbers():Array<{}>{
          return [{
            id: 1,
             status: false,
             cc: 123,
             ndc: 123,
             sn: 123456789,
          }]
      }
}
