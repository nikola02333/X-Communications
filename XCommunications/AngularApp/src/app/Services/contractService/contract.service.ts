import { Injectable } from '@angular/core';
import { Contract } from 'src/app/Models/Contract';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

import { FormControl, FormGroup, Validators } from "@angular/forms"
import {RequestOptions, Request, RequestMethod} from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

    baseUrl='https://localhost:44350/api';
    form = new FormGroup({
      imsi: new FormControl('', Validators.required),
      iccid: new FormControl('', Validators.required),
      pin: new FormControl('', Validators.required),
      puk: new FormControl('', Validators.required)
    });

  
  constructor(private http: HttpClient) { }

  postContract(services: Contract) 
  {
    return this.http.post<Contract>(this.baseUrl,services);
    
  }

  getAllContract(): Observable<Contract[]> {

    return this.http.get<Contract[]>(this.baseUrl +'/'+'Contracts');
} 

  deleteContract(id: Contract) {
    return this.http.delete( this.baseUrl +'/'+'Contracts' + '/' + id);
    }

    updateContract(contract: Contract) {  
      return this.http.put(this.baseUrl +'/'+'Contracts'+ '/' + contract.custumerId , contract);  
    }  
}


