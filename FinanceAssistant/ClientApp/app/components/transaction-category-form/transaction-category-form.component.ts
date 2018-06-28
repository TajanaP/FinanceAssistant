import { Component, OnInit } from '@angular/core';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-transaction-category-form',
  templateUrl: './transaction-category-form.component.html',
  styleUrls: ['./transaction-category-form.component.css']
})
export class TransactionCategoryFormComponent implements OnInit {

    transactionTypes: any[];
    transactionCategory: any = {}; // initialy set to blank object

    constructor(
        private transactionTypeService: TransactionTypeService,
        private transactionCategoryService: TransactionCategoryService,
        private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(params => {
            this.transactionCategory.id = +params['id'] || 0  // '+' converts 'id' to a number, '0' if 'id' is not defined (new category)
        });
    }

    ngOnInit() {
        this.transactionTypeService.getCategoryTypes().subscribe(types =>
            this.transactionTypes = types);

        if (this.transactionCategory.id)
            this.transactionCategoryService.getCategory(this.transactionCategory.id).subscribe(category =>
                this.transactionCategory = category);
    }

    submit() {
        if (this.transactionCategory.id) {
            this.transactionCategory.typeId = this.transactionCategory.type.id;
            this.transactionCategoryService.updateCategory(this.transactionCategory)
                .subscribe(result => this.router.navigate(['/transactionCategories']));
        }
        else
            this.transactionCategoryService.createCategory(this.transactionCategory)
                .subscribe(result => this.router.navigate(['/transactionCategories']));
    }

    delete() {
        if (confirm('Are you sure you want do delete this category?')) {
            this.transactionCategoryService.deleteCategory(this.transactionCategory.id)
                .subscribe(x => {
                    this.router.navigate(['/home']);
                });
        }
    }
}
