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

        route.params.subscribe(params =>
            this.transactionCategory.id = +params['id']); // '+' converts 'id' to a number
    }

    ngOnInit() {
        this.transactionTypeService.getCategoryTypes().subscribe(types =>
            this.transactionTypes = types);

        this.transactionCategoryService.getCategory(this.transactionCategory.id).subscribe(category =>
            this.transactionCategory = category);
    }

    submit() {
        if (this.transactionCategory.id) {
            this.transactionCategoryService.updateCategory(this.transactionCategory)
                .subscribe(result => console.log(result));
        } else {
            this.transactionCategory.id = 0; // EF will create 'id'
            this.transactionCategoryService.createCategory(this.transactionCategory)
                .subscribe(result => console.log(result));
        }
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
