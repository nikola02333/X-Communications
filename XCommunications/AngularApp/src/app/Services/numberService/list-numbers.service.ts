import { Injectable } from '@angular/core';
import { Number } from 'src/app/Models/Number';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms"
import {RequestOptions, Request, RequestMethod} from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class ListNumbersService {

 readonly baseUrl='https://localhost:44350/api/Numbers';

  
  form = new FormGroup({
    cc: new FormControl('', Validators.required),
    ndc: new FormControl('', Validators.required),
    sn: new FormControl('', Validators.required)
  });
  constructor(private http: HttpClient) { }

  postNumber(services: Number) : Observable<Number>
  {
    return this.http.post<Number>(this.baseUrl,services);
    
  }

  getAllNumbers(): Observable<Number[]> {

    return this.http.get<Number[]>(this.baseUrl +'/'+'GetNumbers');
} 

  deleteNumber(id: number) {
    return this.http.delete( this.baseUrl +'/'+'DeleteNumber' + '/' + id);
    }

    updateNumber(number: Number) {  
      return this.http.put(this.baseUrl +'/'+'PutNumber'+ '/' + number.id , number);  
    } 
}