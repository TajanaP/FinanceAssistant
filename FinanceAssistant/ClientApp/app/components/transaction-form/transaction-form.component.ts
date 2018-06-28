import { Component, OnInit } from '@angular/core';
import { TransactionCategoryService } from '../../services/transaction-category.service';
import { TransactionTypeService } from '../../services/transaction-type.service';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import { SaveTransaction, Transaction } from '../../models/transaction';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {

    transactionTypes: any[];
    transactionCategories: any[];
    transaction: SaveTransaction = {
        id: 0,
        typeId: 0,
        categoryId: 0,
        description: '',
        amount: 0,
        currency: '',
        date: new Date
    };

    constructor(
        private transactionTypeService: TransactionTypeService,
        private transactionCategoryService: TransactionCategoryService,
        private transactionService: TransactionService,
        private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(params => {
            this.transaction.id = +params['id'] || 0 // '+' converts 'id' to a number, '0' if 'id' is not defined (new transaction)
        });
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

        this.transactionCategoryService.getCategories(selectedType.id).subscribe(categories =>
            this.transactionCategories = categories);
    }

    setTranasction(transaction: Transaction) {
        this.transaction.id = transaction.id;
        this.transaction.typeId = transaction.category.type.id
        this.transaction.categoryId = transaction.category.id;
        this.transaction.description = transaction.description;
        this.transaction.amount = transaction.amount;
        this.transaction.currency = transaction.currency;
        this.transaction.date = transaction.date;
    }

    submit() {
        if (this.transaction.id)
            this.transactionService.updateTransaction(this.transaction)
                .subscribe(result => this.router.navigate(['/transactions']));
        else
            this.transactionService.createTransaction(this.transaction)
                .subscribe(result => this.router.navigate(['/transactions']));
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
