import { Injectable } from '@angular/core';
import { SimCard } from 'src/app/Models/SimCard';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms"
import { WorkerService } from '../workerService/worker.service';

@Injectable({
  providedIn: 'root'
})
export class SimCardServiceService {

  baseUrl = 'https://localhost:44351/api/Simcards';
  
  form:FormGroup;

  constructor(private http: HttpClient,
              private workerService:WorkerService) { 
    this.form = new FormGroup({
      imsi: new FormControl('', Validators.required),
      iccid: new FormControl('', Validators.required),
      pin: new FormControl('', [Validators.required, Validators.maxLength(4), Validators.minLength(4)]),
      puk: new FormControl('', [Validators.required, Validators.maxLength(4), Validators.minLength(4)])
    });
  }

  post(services: SimCard): Observable<SimCard> {
    return this.http.post<SimCard>(this.baseUrl, services, this.workerService.getAuthorization());

  }

  getAllSimCards(): Observable<SimCard[]> {
    return this.http.get<SimCard[]>(this.baseUrl, this.workerService.getAuthorization());
  }

  deleteSimCard(id: SimCard) {
    return this.http.delete(this.baseUrl + '/' + id.imsi, this.workerService.getAuthorization());
  }

  updateSimCard(simCard: SimCard) {
    return this.http.put(this.baseUrl + '/' + simCard.imsi, simCard, this.workerService.getAuthorization());
  }

  getAvailabeSimCards():Observable<SimCard[]>{
    return this.http.get<SimCard[]>(this.baseUrl + '/GetAvailableSimcard', this.workerService.getAuthorization());
  }
}
