import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  transactions: Transaction[] = [];

  constructor(private transactionService: TransactionService) { }

  ngOnInit() {
    this.getTransactions();
  }

  getTransactions() {
    this.transactionService.getTransactions()
    .subscribe(data => {
      this.transactions = data;
    });
  }
}

export interface Transaction {
  id: string;
  type: string;
  amount: string;
  effectiveDate: Date;
}
