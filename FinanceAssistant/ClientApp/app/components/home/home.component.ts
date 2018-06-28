import { Component } from '@angular/core';
import { HomeService } from '../../services/home.service';
import { Chart } from 'angular-highcharts';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/zip';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    sum: any;
    startDate: any;
    endDate: any;
    currency: any;
    typeExpense: number = 1;
    typeIncome: number = 2;
    balanceChart: any;
    balanceLabelList: any[];
    balanceAmountList: any[];
    expenseChart: any;
    expenseLabelList: any[];
    expenseAmountList: any[];
    incomeChart: any;
    incomeLabelList: any[];
    incomeAmountList: any[];

    constructor(private homeService: HomeService) {
        this.currency = "EUR";
        this.displayHomePageData();
    }

    displayHomePageData() {
        this.clearChartData();
        Observable.zip(
            this.homeService.getChartDataByCategory(this.currency, this.typeExpense, this.startDate, this.endDate),
            this.homeService.getChartDataByCategory(this.currency, this.typeIncome, this.startDate, this.endDate),
            this.homeService.getChartDataByType(this.currency, this.startDate, this.endDate))
            .subscribe(([expenseData, incomeData, balanceData]) => {
                this.expenseLabelList = expenseData.chartLabels;
                this.expenseAmountList = expenseData.chartAmounts;
                this.incomeLabelList = incomeData.chartLabels;
                this.incomeAmountList = incomeData.chartAmounts;
                this.balanceLabelList = balanceData.chartLabels;
                this.balanceAmountList = balanceData.chartAmounts;
                this.createCharts();
            });

        this.homeService.calculateSum(this.currency, this.startDate, this.endDate).subscribe(sum => {
            this.sum = sum;
            this.sum = this.sum.toFixed(2); // show trailing zeros
        });
    }

    clearChartData() {
        this.expenseLabelList = [];
        this.expenseAmountList = [];
        this.incomeLabelList = [];
        this.incomeAmountList = [];
        this.balanceLabelList = [];
        this.balanceAmountList = [];
    }

    createCharts() {
        this.balanceChart = new Chart({
            chart: {
                type: 'pie'
            },
            title: {
                text: ''
            },
            tooltip: {
                //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                pointFormat: 'Amount: <b>{point.y:.2f}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    }
                }
            },
            series: [{
                name: 'Total',
                data: [{
                    name: this.balanceLabelList[0],
                    y: this.balanceAmountList[0],
                    sliced: true,
                    color: 'rgba(250, 5, 5, 1)'
                }, {
                    name: this.balanceLabelList[1],
                    y: this.balanceAmountList[1],
                    color: 'rgba(53, 133, 10, 1)'
                }]
            }]
        });

        this.expenseChart = new Chart({
            chart: {
                type: 'pie'
            },
            title: {
                text: ''
            },
            tooltip: {
                //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                pointFormat: 'Amount: <b>{point.y:.2f}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    }
                }
            },
            series: [{
                name: 'Expense',
                data: [{
                    name: this.expenseLabelList[0],
                    y: this.expenseAmountList[0],
                    sliced: true
                }, {
                    name: this.expenseLabelList[1],
                    y: this.expenseAmountList[1],
                }, {
                    name: this.expenseLabelList[2],
                    y: this.expenseAmountList[2],
                }, {
                    name: this.expenseLabelList[3],
                    y: this.expenseAmountList[3],
                }, {
                    name: this.expenseLabelList[4],
                    y: this.expenseAmountList[4],
                }, {
                    name: this.expenseLabelList[5],
                    y: this.expenseAmountList[5],
                }, {
                    name: this.expenseLabelList[6],
                    y: this.expenseAmountList[6],
                }, {
                    name: this.expenseLabelList[7],
                    y: this.expenseAmountList[7],
                }, {
                    name: this.expenseLabelList[8],
                    y: this.expenseAmountList[8],
                }, {
                    name: this.expenseLabelList[9],
                    y: this.expenseAmountList[9],
                }, {
                    name: this.expenseLabelList[10],
                    y: this.expenseAmountList[10],
                }, {
                    name: this.expenseLabelList[11],
                    y: this.expenseAmountList[11],
                }, {
                    name: this.expenseLabelList[12],
                    y: this.expenseAmountList[12],
                }]
            }]
        });

        this.incomeChart = new Chart({
            chart: {
                type: 'pie'
            },
            title: {
                text: ''
            },
            tooltip: {
                //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                pointFormat: 'Amount: <b>{point.y:.2f}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    }
                }
            },
            series: [{
                name: 'Income',
                data: [{
                    name: this.incomeLabelList[0],
                    y: this.incomeAmountList[0],
                    sliced: true
                }, {
                    name: this.incomeLabelList[1],
                    y: this.incomeAmountList[1],
                }, {
                    name: this.incomeLabelList[2],
                    y: this.incomeAmountList[2],
                }, {
                    name: this.incomeLabelList[3],
                    y: this.incomeAmountList[3],
                }, {
                    name: this.incomeLabelList[4],
                    y: this.incomeAmountList[4],
                }, {
                    name: this.incomeLabelList[5],
                    y: this.incomeAmountList[5],
                }, {
                    name: this.incomeLabelList[6],
                    y: this.incomeAmountList[6],
                }, {
                    name: this.incomeLabelList[7],
                    y: this.incomeAmountList[7],
                }, {
                    name: this.incomeLabelList[8],
                    y: this.incomeAmountList[8],
                }]
            }]
        });

        // COLUMN CHART
        //this.chart = new Chart({
        //    chart: {
        //        type: 'column'
        //    },
        //    title: {
        //        text: 'Expense'
        //    },
        //    xAxis: {
        //        type: 'category',
        //        labels: {
        //            rotation: -45,
        //            style: {
        //                fontSize: '13px',
        //                fontFamily: 'Verdana, sans-serif'
        //            }
        //        }
        //    },
        //    yAxis: {
        //        title: {
        //            text: 'Amount (EUR)'
        //        }
        //    },
        //    legend: {
        //        enabled: false
        //    },
        //    tooltip: {
        //        pointFormat: 'Amount: <b>{point.y:.2f} EUR</b>'
        //    },
        //    series: [{
        //        type: 'column',
        //        data: [
        //            { name: this.labelList[0], y: this.amountList[0], color: 'rgba(253, 185, 19, 0.85)' },
        //            { name: this.labelList[1], y: this.amountList[1], color: 'rgba(0, 76, 147, 0.85)' },
        //            { name: this.labelList[2], y: this.amountList[2], color: 'rgba(170, 69, 69, 0.85)' },
        //            { name: this.labelList[3], y: this.amountList[3], color: 'rgba(112, 69, 143, 0.85)' },
        //            { name: this.labelList[4], y: this.amountList[4], color: 'rgba(0, 93, 160, 0.85)' },
        //            { name: this.labelList[5], y: this.amountList[5], color: 'rgba(45, 77, 157, 0.85)' },
        //            { name: this.labelList[6], y: this.amountList[6], color: 'rgba(45, 77, 157, 0.85)' },
        //            { name: this.labelList[7], y: this.amountList[7], color: 'rgba(45, 77, 157, 0.85)' },
        //            { name: this.labelList[8], y: this.amountList[8], color: 'rgba(45, 77, 157, 0.85)' }
        //        ],
        //    }]
        //});
    }
}