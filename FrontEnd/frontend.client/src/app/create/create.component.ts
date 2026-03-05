import { Component,  OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { CreateEditComponent } from '../create-edit/create-edit.component';
import { CreateEditService } from '../create-edit/create-edit.service';

@Component({
  selector: 'app-create',
  standalone: true,
  templateUrl: './create.component.html',
  styleUrl: './create.css',
  imports: [CreateEditComponent, DatePipe]
})
export class CreateComponent implements OnInit {
  public today: Date = new Date();

  constructor(private service: CreateEditService,
              private router: Router
    ) {}

  public isLoading(): boolean {
    return this.service.isLoading();
  }

  ngOnInit(): void {
    this.service.getCurrent();
  }

  clickSave(): void {
    this.service.createBalanceSheet()
      .subscribe({
        next: (response: HttpResponse<any>) => {
          if (response.status === 200) {
            this.router.navigate(['/details/' + response.body.toString()]);
          }
        }
      });
  }
}
