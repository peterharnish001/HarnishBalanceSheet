import { Component, OnInit, signal } from '@angular/core';
import { AssetModel } from './models/asset.model';
import { CreateEditService } from './create-edit.service';

@Component({
  selector: 'app-create-edit',
  standalone: true,
  templateUrl: './create-edit.component.html',
  styleUrl: './create-edit.component.css',
})
export class CreateEditComponent implements OnInit {
  constructor(private service: CreateEditService
      ) {}

  ngOnInit() {

  }

  public getAssets(): AssetModel[] {
    return this.service.assets();
  }
}
