import { Injectable } from '@angular/core';
import { SimCard } from 'src/app/Models/SimCard';
import { Observable } from 'rxjs';

@Injectable()
export class SimcardServiceMock {
    constructor() {}

    getAllSimCards():Array<{}>{
        return [{
            imsi: 0,
            iccid: 0,
            pin: 0,
            puk: 0
        }];
    }

    deleteSimCard(){

    }

    updateSimCard(){

    }

    updateUser(){
        
    }

    getAvailabeSimCards():Array<{}>{
        return [{
            imsi: 0,
            iccid: 0,
            pin: 0,
            puk: 0
        }];
    }
}