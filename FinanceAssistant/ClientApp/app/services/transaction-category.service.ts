import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class TransactionCategoryService {

    constructor(private http: Http) { }

    getCategory(id: number) {
        return this.http.get("/api/transactionCategory/" + id)
            .map(response => response.json());
    }

    getCategories(id?: number) {
        return this.http.get("/api/transactionCategory/all/" + id)
            .map(response => response.json());
    }

    createCategory(category: any) {
        return this.http.post("/api/transactionCategory", category)
            .map(response => response.json());
    }

    updateCategory(category: any) {
        return this.http.put("/api/transactionCategory/" + category.id, category)
            .map(response => response.json());
    }

    deleteCategory(id: number) {
        return this.http.delete("/api/transactionCategory/" + id)
            .map(response => response.json());
    }
}
