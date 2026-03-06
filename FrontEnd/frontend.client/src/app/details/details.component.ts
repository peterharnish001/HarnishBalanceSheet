import { Component, OnInit, signal } from '@angular/core';
import { CurrencyPipe, DatePipe, PercentPipe, NgClass } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { DetailsModel } from './models/details.model';
import { DetailsService } from './details.service';
import { TargetComparisonModel } from './models/target-comparison.model';
import { jqxChartModule } from 'jqwidgets-ng/jqxchart';

@Component({
  selector: 'app-details.component',
  standalone: true,
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',
  imports: [CurrencyPipe, DatePipe, PercentPipe, NgClass, jqxChartModule ]
})
export class DetailsComponent implements OnInit {
  balanceSheetId?: number | null;
  detailsModel = signal<DetailsModel | null>(null);
  chartSettings1 = signal<any>({});
  chartSettings2 = signal<any>({});

  constructor(private service: DetailsService,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.balanceSheetId = Number(this.route.snapshot.paramMap.get('id')) || null;

    if (this.balanceSheetId !== null) {
      this.service.getDetails(this.balanceSheetId)
        .subscribe((result: DetailsModel) => {
          this.detailsModel.set(result);
          this.chartSettings1.set({
            title: 'Bullion Shares',
            description: '',
            enableAnimations: true,
            showLegend: true,
            padding: { left: 5, top: 5, right: 5, bottom: 5 },
            titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
            source: this.detailsModel()!.bullionSummary.bullion,
            seriesGroups: [
              {
                type: 'pie',
                showLabels: true,
                series: [
                  {
                    dataField: 'totalPrice',
                    displayText: 'metalName',
                    labelRadius: 100,
                    initialAngle: 15,
                    radius: 120,
                    centerOffset: 0,
                    formatFunction: (value: number) => {
                      return value.toLocaleString('en-US', {
                        style: 'currency',
                        currency: 'USD'
                      });
                    }
                  }
                ]
              }
            ]
          });
          this.chartSettings2.set({
            title: 'Asset allocation',
            description: '',
            enableAnimations: true,
            showLegend: true,
            padding: { left: 5, top: 5, right: 5, bottom: 5 },
            titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
            source: this.detailsModel()!.targetComparisons,
            seriesGroups: [
              {
                type: 'pie',
                showLabels: true,
                series: [
                  {
                    dataField: 'actual',
                    displayText: 'name',
                    labelRadius: 100,
                    initialAngle: 15,
                    radius: 120,
                    centerOffset: 0,
                    formatFunction: (value: any) => {
                      if (typeof value === 'number') {
                        return (value * 100).toFixed(2) + '%';
                      } else {
                        return value;
                      }
                    }
                  }
                ]
              }
            ]
          });
        });
    }
  }

  public isMinimum(target: TargetComparisonModel): boolean {
    return target.difference === this.findMin(this.detailsModel()!.targetComparisons);
  }

  private findMin(arr: TargetComparisonModel[]): number | null {
    if (!Array.isArray(arr) || arr.length === 0) {
      return null;
    }
    return Math.min(...arr.map(target => target.difference));
  }
}
