import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class TransactionTypeService {

    constructor(private http: Http) { }

    getCategoryTypes() {
        return this.http.get('/transactionTypes')
            .map(response => response.json());
    }
}