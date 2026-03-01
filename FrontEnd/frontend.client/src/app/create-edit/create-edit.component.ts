import { Component } from '@angular/core';
import { AssetModel } from './models/asset.model';
import { CreateEditService } from './create-edit.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CurrencyFormatDirective } from '../currency-format.directive';

@Component({
  selector: 'app-create-edit',
  standalone: true,
  templateUrl: './create-edit.component.html',
  styleUrl: './create-edit.component.css',
   imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective]
})
export class CreateEditComponent {

  constructor(private service: CreateEditService
      ) {
      }

  public getAssets(): AssetModel[] {
    return this.service.assets();
  }
}
