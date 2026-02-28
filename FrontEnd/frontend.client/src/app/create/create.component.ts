import { Component,  OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
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

  constructor(private service: CreateEditService
    ) {}

  ngOnInit(): void {
    this.service.getCurrent();
  }
}
