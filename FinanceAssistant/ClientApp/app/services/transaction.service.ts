import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveTransaction } from '../models/transaction';

@Injectable()
export class TransactionService {

    constructor(private http: Http) { }

    getTransaction(id: number) {
        return this.http.get("/api/transaction/" + id)
            .map(response => response.json());
    }

    getTransactions(startDate?: Date, endDate?: Date) {
        return this.http.get("/api/transaction/all/" + startDate + "/" + endDate)
            .map(response => response.json());
    }

    createTransaction(transaction: any) {
        return this.http.post("/api/transaction", transaction)
            .map(response => response.json());
    }

    updateTransaction(transaction: SaveTransaction) {
        return this.http.put("/api/transaction/" + transaction.id, transaction)
            .map(response => response.json());
    }

    deleteTransaction(id: number) {
        return this.http.delete("/api/transaction/" + id)
            .map(response => response.json());
    }

    calculateSum() {
        return this.http.get("/api/transaction/sum")
            .map(response => response.json());
    }

    getChartDataByCategory(id?: number, startDate?: Date, endDate?: Date) {
        return this.http.get("/api/transaction/chartDataByCategory/" + id + "/" + startDate + "/" + endDate)
            .map(response => response.json());
    }

    getChartDataByType(startDate?: Date, endDate?: Date) {
        return this.http.get("/api/transaction/chartDataByType/" + startDate + "/" + endDate)
            .map(response => response.json());
    }
}
