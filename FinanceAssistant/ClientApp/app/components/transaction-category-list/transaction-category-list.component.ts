import { Component, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { TransactionTypeService } from '../../services/transaction-type.service';

@Component({
  selector: 'app-transaction-category-list',
  templateUrl: './transaction-category-list.component.html',
  styleUrls: ['./transaction-category-list.component.css']
})
export class TransactionCategoryListComponent implements OnInit {
    categories: any[];
    allCategories: any[];
    types: any[];
    filter: any = {};

    constructor(private transactionCategoryService: TransactionCategoryService,
                private transactionTypeService: TransactionTypeService) { }

    ngOnInit() {
        this.transactionCategoryService.getCategories().subscribe(categories =>
            this.categories = this.allCategories = categories);

        this.transactionTypeService.getCategoryTypes().subscribe(types =>
            this.types = types);
    }

    onFilterChange() {
        var categories = this.allCategories;

        if (this.filter.typeId)
            categories = categories.filter(c => c.type.id == this.filter.typeId);

        this.categories = categories;
    }

    resetFilter() {
        this.filter = {};
        this.onFilterChange();
    }
}
