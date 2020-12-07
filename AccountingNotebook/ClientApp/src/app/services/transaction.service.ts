import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Transaction } from '../home/home.component';

const API_URL = `${environment.apiUrl}/Transaction`;

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private http: HttpClient) { }

  getTransactions() {
    return this.http.get<Transaction[]>(API_URL);
  }
}
