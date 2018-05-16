import { Component, OnInit } from '@angular/core';
import { TransactionTypeService } from '../../services/transaction-type.service';

@Component({
  selector: 'app-transaction-category-form',
  templateUrl: './transaction-category-form.component.html',
  styleUrls: ['./transaction-category-form.component.css']
})
export class TransactionCategoryFormComponent implements OnInit {

    transactionTypes: any[];
    transactionCategory = {}; // initialy set to blank object

    constructor(private transactionTypeService: TransactionTypeService) { }

    ngOnInit() {
        this.transactionTypeService.getCategoryTypes().subscribe(types =>
            this.transactionTypes = types);
    }
}
