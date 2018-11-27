import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Worker } from '../../Models/Worker';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  constructor(private http: HttpClient) { }

  readonly baseUrl = 'http://localhost:44350/api/Workers';

  postWorker(worker: Worker): Observable<Worker> {
    return this.http.post<Worker>(this.baseUrl, worker);
  }

  getAllWorkers(): Observable<Worker[]> {
    return this.http.get<Worker[]>(this.baseUrl);
  }

  deleteWorker(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  updateWorker(worker: Worker) {
    return this.http.put(this.baseUrl + '/' + worker.id, worker);
  }
}
