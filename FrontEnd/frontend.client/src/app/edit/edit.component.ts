import { Component, OnInit } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { CreateEditComponent } from '../create-edit/create-edit.component';
import { CreateEditService } from '../create-edit/create-edit.service';

@Component({
  selector: 'app-edit.component',
  standalone: true,
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css',
  imports: [CreateEditComponent, DatePipe]
})
export class EditComponent implements OnInit{
  private balanceSheetId?: number | null;
  public date: any;

  constructor(private service: CreateEditService,
              private router: Router,
              private route: ActivatedRoute
    ) {}

  ngOnInit(): void {
    this.balanceSheetId = Number(this.route.snapshot.paramMap.get('id')) || null;
    if (this.balanceSheetId !== null) {
      this.service.getBalanceSheet(this.balanceSheetId);
      this.date = this.service.date;
    }
  }

  public isLoading(): boolean {
    return this.service.isLoading();
  }

   clickSave(): void {
    this.service.editBalanceSheet(this.balanceSheetId!)
      .subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status === 200) {
            this.router.navigate(['/details/' + this.balanceSheetId!.toString()]);
          }
        }
      });
  }
}
