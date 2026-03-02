import { Component, inject } from '@angular/core';
import { AddEditAssetModel } from './models/addeditasset.model';
import { AssetModel } from './models/asset.model';
import { LiabilityModel } from './models/liability.model';
import { CreateEditService } from './create-edit.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CurrencyFormatDirective } from '../currency-format.directive';
import { MatDialog } from '@angular/material/dialog';
import { AddEditAssetComponent } from '../add-edit-asset/add-edit-asset.component';
import { AssetComponentModel } from './models/assetcomponent.model';

@Component({
  selector: 'app-create-edit',
  standalone: true,
  templateUrl: './create-edit.component.html',
  styleUrl: './create-edit.component.css',
   imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective]
})
export class CreateEditComponent {
  private dialog = inject(MatDialog);

  constructor(private service: CreateEditService
      ) {
      }

  public getAssets(): AssetModel[] {
    return this.service.assets();
  }

  public getLiabilities(): LiabilityModel[] {
    return this.service.liabilities();
  }

  public addAsset(): void {
    this.dialog.open(AddEditAssetComponent, {
      data: {
        addOrEdit: 'Add',
        assetTypes: this.service.assetTypes(),
        assetNames: this.service.assetNames,
        asset: new AddEditAssetModel('', null, false, 0,
           this.service.assetTypes().map((type) => new AssetComponentModel(type.assetTypeId, 0, 0, type.name)))
      }
    })
  }

  public getDisabled(asset: AssetModel): boolean {
    return (asset.assetComponents.length > 1 && !asset.isPercent) || asset.name.toLowerCase() === 'bullion' ? true : false;
  }

  public onBlur($event: any, asset: AssetModel ): void {
    const val = Number($event.target.value);
    asset.assetComponents.forEach((component) => {
      component.value = val * component.percentage!;
    });
  }
}
