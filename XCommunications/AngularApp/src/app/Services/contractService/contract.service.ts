import { Injectable } from '@angular/core';
import { Contract } from 'src/app/Models/Contract';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms"
import { WorkerService } from '../workerService/worker.service';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

  form = new FormGroup({
    id: new FormControl('', Validators.required),
    customerId: new FormControl('', Validators.required),
    workerId: new FormControl('', Validators.required),
    tarif: new FormControl('', Validators.required)
  });

  readonly baseUrl = 'https://localhost:44351/api/Contracts';

  constructor(private http: HttpClient,
              private workerService:WorkerService ) { }

  postContract(contract: Contract): Observable<Contract> {
    return this.http.post<Contract>(this.baseUrl, contract, this.workerService.getAuthorization());
  }

  getAllContract(): Observable<Contract[]> {

    return this.http.get<Contract[]>(this.baseUrl, this.workerService.getAuthorization());
  }

  deleteContract(id: number) {
    return this.http.delete(this.baseUrl + '/' + id, this.workerService.getAuthorization());
  }

  updateContract(contract: Contract) {
    return this.http.put(this.baseUrl + '/' + contract.id, contract, this.workerService.getAuthorization());
  }
}


