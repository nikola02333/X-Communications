import { Injectable } from '@angular/core';
import { SimCard } from 'src/app/Models/SimCard';
import { Observable } from 'rxjs';

@Injectable()
export class SimcardServiceMock {
    constructor() {}

    post(sim:SimCard):Array<{}>{
        return [{
            imsi: 0,
            iccid: 0,
            pin: '0000',
            puk: '00000',
            status: false
        }];
    }

    getAllSimCards():Array<{}>{
        return [{
            imsi: 0,
            iccid: 0,
            pin: '0000',
            puk: '00000',
            status: false
        }];
    }

    deleteSimCard(id:number){
        return [{
            imsi: 0,
            iccid: 0,
            pin: '0000',
            puk: '00000',
            status: false
        }];
    }

    updateSimCard(sim:SimCard){
        return [{
            imsi: 0,
            iccid: 0,
            pin: '0000',
            puk: '00000',
            status: false
        }];
    }

    getAvailabeSimCards():Array<{}>{
        return [{
            imsi: 0,
            iccid: 0,
            pin: '0000',
            puk: '00000',
            status: false
        }];
    }
}