import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class HomeService {

    constructor(private http: Http) { }

    calculateSum(currencyToDisplay: string, startDate?: Date, endDate?: Date) {
        return this.http.get("/sum/" + currencyToDisplay + "/" + startDate + "/" + endDate)
            .map(response => response.json());
    }

    getChartDataByCategory(currencyToDisplay: string, id?: number, startDate?: Date, endDate?: Date) {
        return this.http.get("/chartDataByCategory/" + currencyToDisplay + "/" + id + "/" + startDate + "/" + endDate)
            .map(response => response.json());
    }

    getChartDataByType(currencyToDisplay: string, startDate?: Date, endDate?: Date) {
        return this.http.get("/chartDataByType/" + currencyToDisplay + "/" + startDate + "/" + endDate)
            .map(response => response.json());
    }
}