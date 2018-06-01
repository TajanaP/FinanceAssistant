import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../../services/transaction.service';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { TransactionCategoryService } from '../../services/transaction-category.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {

    transactions: any[];
    allTransactions: any[];
    categories: any[];
    allCategories: any[];
    types: any[];
    filter: any = {};

    constructor(private transactionService: TransactionService,
                private categoryService: TransactionCategoryService,
                private typeService: TransactionTypeService) { }

    ngOnInit() {
        this.transactionService.getTransactions().subscribe(transactions =>
            this.transactions = this.allTransactions = transactions);

        this.categoryService.getCategories().subscribe(categories =>
            this.categories = this.allCategories = categories);

        this.typeService.getCategoryTypes().subscribe(types =>
            this.types = types);
    }

    onFilterChange() {
        var transactions = this.allTransactions;

        if (this.filter.typeId) {
            transactions = transactions.filter(t => t.category.type.id == this.filter.typeId);
            var selectedType = this.types.find(t => t.id == this.filter.typeId);
            this.categoryService.getCategoriesForType(selectedType.id).subscribe(categories =>
                this.categories = categories);
        }

        if (this.filter.categoryId)
            transactions = transactions.filter(t => t.category.id == this.filter.categoryId);

        this.transactions = transactions;
    }

    resetFilter() {
        this.filter = {};
        this.categories = this.allCategories;
        this.onFilterChange();
    }
}
