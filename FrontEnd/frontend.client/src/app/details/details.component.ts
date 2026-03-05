import { Component, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { DetailsModel } from './models/details.model';
import { DetailsService } from './details.service';

@Component({
  selector: 'app-details.component',
  standalone: true,
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',
  imports: [ DatePipe ]
})
export class DetailsComponent implements OnInit {
  balanceSheetId?: number | null;
  detailsModel = signal<DetailsModel | null>(null);

  constructor(private service: DetailsService,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.balanceSheetId = Number(this.route.snapshot.paramMap.get('id')) || null;

    if (this.balanceSheetId !== null) {
      this.service.getDetails(this.balanceSheetId)
        .subscribe((result: DetailsModel) => {
          this.detailsModel.set(result);
        });
    }
  }
}
