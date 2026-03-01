import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { RouterLink } from '@angular/router';
import { HbsIndexService } from './hbs-index.service';
import { SetTargetsComponent } from './set-targets/set-targets.component';
import { SetTargetModel } from './models/settarget.model';
import { TargetInputModel } from './models/targetinput.model';
import { ToastrService } from 'ngx-toastr';
import { BalanceSheetDateModel } from './models/balancesheetdate.model';
import { jqxChartModule } from 'jqwidgets-ng/jqxchart';

@Component({
  selector: 'app-hbs-index',
  standalone: true,
  templateUrl: './hbs-index.component.html',
  styleUrl: './hbs-index.component.css',
  imports: [DatePipe, jqxChartModule, RouterLink]
})
export class HbsIndexComponent implements OnInit {
  private dialog = inject(MatDialog);
  private toastr = inject(ToastrService);
  public balanceSheetDateModels = signal<BalanceSheetDateModel[]>([]);
  public count: number = 24;
  public liabilitiesSource = signal<any[]>([]);
  public netWorthSource = signal<any[]>([]);
  public padding: any = { left: 10, top: 5, right: 10, bottom: 5 };
  public xAxis1: any = { dataField: 'date', type: 'date' };
  public xAxis2: any = { dataField: 'date', type: 'date' };
  public seriesGroup1: any[] = [
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
        ];
  public seriesGroup2: any[] = [
          {
            type: 'spline',
            valueAxis: {
              description: 'Net Worth',
              minValue: 0,
              displayValueAxis: true
            },
            series: [
              { dataField: 'netWorth', displayText: 'Net Worth' }
            ]
          }
        ];

  constructor(private service: HbsIndexService
  ) {}

 ngOnInit() {
  this.service.getHasTargets()
    .subscribe({
      next: (output) => {
        if (output.length > 0) {
          this.dialog.open(SetTargetsComponent, {
            data: output
          })
          .afterClosed()
          .subscribe((result: TargetInputModel[]) => {
            this.service.setTargets(result.map(datum => new SetTargetModel(datum.assetTypeId, datum.percentage / 100)))
            .subscribe({
              next: (response: HttpResponse<any>) => {
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
  this.getBalanceSheetData();
  this.getLiabilityChartData();
  this.getNetWorthChartData();
 }

 getBalanceSheetData(): void {
  this.service.getBalanceSheetData(this.count)
    .subscribe({
      next: (output) => {
        this.balanceSheetDateModels.set(output);
      }
    })
 }

 getLiabilityChartData(): void {
  this.service.getLiabilityChartData(this.count)
    .subscribe({
      next: (output) => {
        this.liabilitiesSource.set(output);
      }
    })
 }

 getNetWorthChartData(): void {
  this.service.getNetWorthChartData(this.count)
    .subscribe({
      next: (output) => {
        this.netWorthSource.set(output);
      }
    })
 }

 onDropdownChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.count = Number(value);
    this.getBalanceSheetAndChartData();
  }
}


