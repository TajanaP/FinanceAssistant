import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class TransactionService {

    constructor(private http: Http) { }

    getTransaction(id: number) {
        return this.http.get("/api/transaction/" + id)
            .map(response => response.json());
    }

    getTransactions() {
        return this.http.get("/api/transaction")
            .map(response => response.json());
    }

    createTransaction(transaction: any) {
        return this.http.post("/api/transaction", transaction)
            .map(response => response.json());
    }

    updateTransaction(transaction: any) {
        return this.http.put("/api/transaction/" + transaction.id, transaction)
            .map(response => response.json());
    }

    deleteTransaction(id: number) {
        return this.http.delete("/api/transaction/" + id)
            .map(response => response.json());
    }
}
