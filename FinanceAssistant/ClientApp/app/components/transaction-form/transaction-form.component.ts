import { Component, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { TransactionTypeService } from '../../services/transaction-type.service';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

    transaction: any = {}
    transactionTypes: any[];
    transactionCategories: any[];

    constructor(private transactionTypeService: TransactionTypeService, private transactionCategoryService: TransactionCategoryService) { }

    ngOnInit() {
        this.transactionTypeService.getCategoryTypes().subscribe(types =>
            this.transactionTypes = types);
    }

    onTypeChange() {
        var selectedType = this.transactionTypes.find(t => t.id == this.transaction.typeId);

        this.transactionCategoryService.getCategoriesForType(selectedType.id).subscribe(categories =>
            this.transactionCategories = categories);
    }
}
