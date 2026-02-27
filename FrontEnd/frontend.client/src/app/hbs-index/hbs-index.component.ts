import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { HbsIndexService } from './hbs-index.service';
import { SetTargetsComponent } from './set-targets/set-targets.component';
import { SetTargetModel } from './models/settarget.model';
import { TargetInputModel } from './models/targetinput.model';
import { NgxSpinnerService, NgxSpinnerComponent } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BalanceSheetDateModel } from './models/balancesheetdate.model';
import { jqxChartModule } from 'jqwidgets-ng/jqxchart';

@Component({
  selector: 'app-hbs-index',
  standalone: true,
  templateUrl: './hbs-index.component.html',
  styleUrl: './hbs-index.component.css',
  imports: [NgxSpinnerComponent, DatePipe, jqxChartModule]
})
export class HbsIndexComponent implements OnInit {
  private dialog = inject(MatDialog);
  private toastr = inject(ToastrService);
  public balanceSheetDateModels = signal<BalanceSheetDateModel[]>([]);
  public count: number = 24;
  public source = signal<any[]>([]);
  public padding: any = { left: 10, top: 5, right: 10, bottom: 5 };
  public xAxis: any = { dataField: 'date', type: 'date'};
  public seriesGroups = signal<any[]>([]);

  constructor(private service: HbsIndexService,
              private spinner: NgxSpinnerService
  ) {}

 ngOnInit() {
  this.spinner.show();
  this.service.getHasTargets()
    .subscribe({
      next: (output) => {
        if (output.length > 0) {
          this.spinner.hide();
          this.dialog.open(SetTargetsComponent, {
            data: output
          })
          .afterClosed()
          .subscribe((result: TargetInputModel[]) => {
            this.spinner.show();
            this.service.setTargets(result.map(datum => new SetTargetModel(datum.assetTypeId, datum.percentage / 100)))
            .subscribe({
              next: (response: HttpResponse<any>) => {
                this.spinner.hide();
                if (response.status === 200) {
                  this.toastr.success('Targets saved successfully', 'Success');
                }
              }
            });
          });
        } else {
          this.getBalanceSheetAndChartData();
        }
      }
    },
  );
 }

 getBalanceSheetAndChartData(): void {
  this.spinner.show();
  this.getBalanceSheetData();
  this.getLiabilityChartData();
 }

 getBalanceSheetData(): void {
  this.service.getBalanceSheetData(this.count)
    .subscribe({
      next: (output) => {
        this.spinner.hide();
        this.balanceSheetDateModels.set(output);
      }
    })
 }

 getLiabilityChartData(): void {
  this.service.getLiabilityChartData(this.count)
    .subscribe({
      next: (output) => {
        this.source.set(output);
        this.seriesGroups.set([
          {
            type: 'spline',
            valueAxis: {
              description: 'Liabilities',
              minValue: 0,
              displayValueAxis: true
            },
            series: [
              { dataField: 'totalLiabilities', displayText: 'Total Liabilities' }
            ]
          }
        ]);
      }
    })
 }
}
