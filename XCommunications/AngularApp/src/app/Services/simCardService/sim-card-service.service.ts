import { Injectable } from '@angular/core';
import { SimCard } from 'src/app/Models/SimCard';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

import { FormControl, FormGroup, Validators } from "@angular/forms"
import {RequestOptions, Request, RequestMethod} from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class SimCardServiceService {

    baseUrl='https://localhost:44350/api/Simcards';
    form = new FormGroup({
      imsi: new FormControl('', Validators.required),
      iccid: new FormControl('', Validators.required),
      pin: new FormControl('', Validators.required),
      puk: new FormControl('', Validators.required)
     
    });

  
  constructor(private http: HttpClient) { }

  post(services: SimCard) : Observable<SimCard>
  {
    console.log(services);
    return this.http.post<SimCard>(this.baseUrl,services);
    
  }

  getAllsimCards(): Observable<SimCard[]> {

    return this.http.get<SimCard[]>(this.baseUrl);
} 

  deleteSimCard(id: SimCard) {
    debugger
    return this.http.delete( this.baseUrl + '/' + id.imsi);
    }

    updateSimCard(simCard: SimCard) {  
      debugger
      return this.http.put(this.baseUrl + '/' + simCard.imsi , simCard);  
    }  
}
