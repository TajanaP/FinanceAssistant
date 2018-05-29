import { Component, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

    transaction: any = {}
    transactionTypes: any[];
    transactionCategories: any[];

    constructor(
        private transactionTypeService: TransactionTypeService,
        private transactionCategoryService: TransactionCategoryService,
        private transactionService: TransactionService,
        private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(params =>
            this.transaction.id = +params['id']); // '+' converts 'id' to a number
    }

    ngOnInit() {
        var sources = [
            this.transactionTypeService.getCategoryTypes(),
        ];

        if (this.transaction.id)
            sources.push(this.transactionService.getTransaction(this.transaction.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.transactionTypes = data[0];
            if (this.transaction.id) {
                this.setTranasction(data[1]);
                this.populateCategories();
            } 
        });
    }

    populateCategories() {
        var selectedType = this.transactionTypes.find(t => t.id == this.transaction.typeId);

        this.transactionCategoryService.getCategoriesForType(selectedType.id).subscribe(categories =>
            this.transactionCategories = categories);
    }

    setTranasction(transaction: any) {
        this.transaction.id = transaction.id;
        this.transaction.typeId = transaction.typeId;
        this.transaction.categoryId = transaction.categoryId;
        this.transaction.description = transaction.description;
        this.transaction.amount = transaction.amount;
        this.transaction.currency = transaction.currency;
        this.transaction.date = transaction.date;
    }

    submit() {
        if (this.transaction.id) {
            this.transactionService.updateTransaction(this.transaction)
                .subscribe(result => console.log(result));
        }
        else {
            this.transaction.id = 0; // EF will create 'id'
            this.transactionService.createTransaction(this.transaction)
                .subscribe(result => console.log(result));
        }
    }

    delete() {
        if (confirm('Are you sure you want do delete this transaction?')) {
            this.transactionService.deleteTransaction(this.transaction.id)
                .subscribe(x => {
                    this.router.navigate(['/home']);
                });
        }
    }
}
