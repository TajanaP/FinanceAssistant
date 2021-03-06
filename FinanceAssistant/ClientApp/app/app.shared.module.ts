import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ChartModule } from 'angular-highcharts';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TransactionCategoryFormComponent } from './components/transaction-category-form/transaction-category-form.component';
import { TransactionCategoryListComponent } from './components/transaction-category-list/transaction-category-list.component';
import { TransactionFormComponent } from './components/transaction-form/transaction-form.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { TransactionTypeService } from './services/transaction-type.service';
import { TransactionCategoryService } from './services/transaction-category.service';
import { TransactionService } from './services/transaction.service';
import { HomeService } from './services/home.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
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
        ChartModule,
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
        TransactionService,
        HomeService
    ]
})
export class AppModuleShared {
}
