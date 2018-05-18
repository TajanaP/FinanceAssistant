import { Component, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';

@Component({
  selector: 'app-transaction-category-list',
  templateUrl: './transaction-category-list.component.html',
  styleUrls: ['./transaction-category-list.component.css']
})
export class TransactionCategoryListComponent implements OnInit {
    categories: any[];

    constructor(private transactionCategoryService: TransactionCategoryService) { }

    ngOnInit() {
        this.transactionCategoryService.getCategories().subscribe(categories =>
            this.categories = categories);
    }

}
