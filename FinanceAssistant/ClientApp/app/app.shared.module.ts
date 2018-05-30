import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { TransactionCategoryFormComponent } from './components/transaction-category-form/transaction-category-form.component';
import { TransactionTypeService } from './services/transaction-type.service';
import { TransactionCategoryService } from './services/transaction-category.service';
import { TransactionCategoryListComponent } from './components/transaction-category-list/transaction-category-list.component';
import { TransactionFormComponent } from './components/transaction-form/transaction-form.component';
import { TransactionService } from './services/transaction.service';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        TransactionCategoryFormComponent,
        TransactionCategoryListComponent,
        TransactionFormComponent,
        TransactionListComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'transactions', component: TransactionListComponent },
            { path: 'transaction/new', component: TransactionFormComponent },
            { path: 'transaction/:id', component: TransactionFormComponent },
            { path: 'transactionCategories', component: TransactionCategoryListComponent },
            { path: 'transactionCategory/new', component: TransactionCategoryFormComponent },
            { path: 'transactionCategory/:id', component: TransactionCategoryFormComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        TransactionTypeService,
        TransactionCategoryService,
        TransactionService
    ]
})
export class AppModuleShared {
}
