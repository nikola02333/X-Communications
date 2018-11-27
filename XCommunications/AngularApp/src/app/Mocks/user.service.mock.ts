import { Injectable } from '@angular/core';
import { Customer } from '../Models/Customer';
import { Observable } from 'rxjs';

@Injectable()
export class UserServiceMock {
    constructor() {}

    getAll():Array<{}>{
        return [{
            name: 'name',
            lastname: 'lastName',
            id: 0
        }];
    }

    post(customer: Customer){
        return [{
            name: 'name',
            lastname: 'lastName',
            id: 0
        }];
    }

    deleteUser(id: number){
        return [{
            name: 'name',
            lastname: 'lastName',
            id: 0
        }];
    }

    updateUser(customer: Customer){
        return [{
            name: 'name',
            lastname: 'lastName',
            id: 0
        }];
    }
}