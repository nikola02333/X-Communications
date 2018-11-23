import { Injectable } from '@angular/core';
import { Customer } from '../Models/Customer';
import { Observable } from 'rxjs';

@Injectable()
export class UserServiceMock {
    constructor() {}

    getAll():Array<{}>{
        return [{
            fullName: 'name',
            lastname: 'lastName',
            id: 0
        }];
    }

    post(){
        
    }

    deleteUser(){

    }

    updateUser(){

    }
}