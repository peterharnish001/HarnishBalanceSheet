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
import { MetalPositionModel } from './models/metalposition.model';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-create-edit',
  standalone: true,
  templateUrl: './create-edit.component.html',
  styleUrl: './create-edit.component.css',
   imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective]
})
export class CreateEditComponent {
  private dialog = inject(MatDialog);

  constructor(private service: CreateEditService,
              private cdr: ChangeDetectorRef
      ) {
      }

  public isLoading(): boolean {
    return this.service.isLoading();
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
          this.service.assetTypes().map((type) => new AssetComponentModel(type.assetTypeId, 0, 0, type.name)),
          this.service.metals().map((metal) => new MetalPositionModel(metal.preciousMetalId, metal.name, 0, metal.pricePerOunce, 0)))
      }
    })
    .afterClosed()
    .subscribe((result: AddEditAssetModel | null) => {
      if (result !== null) {
        const model = result as AddEditAssetModel;
        if (model.name.trim().toLowerCase() === 'bullion') {
          const totalValue =  model.bullion.reduce((sum, item) => {
              const value = Number(item.numOunces);
              return sum + (isNaN(value) ? 0 : value * item.pricePerOunce);
            }, 0);
          this.service.addAsset(new AssetModel(
            'Bullion',
            totalValue,
            [new AssetComponentModel(3, totalValue, 1)],
            undefined,
            false
          ));
          this.service.addBullion(model.bullion.map((metal) => new MetalPositionModel(
            metal.preciousMetalId,
            metal.metalName,
            metal.numOunces,
            metal.pricePerOunce,
            metal.numOunces * metal.pricePerOunce)));
        } else if (model.type !== null) {
          this.service.addAsset(new AssetModel(
            model.name.trim(),
            model.totalValue,
            [new AssetComponentModel(model.type, model.totalValue)],
            undefined,
            false));
        } else if (model.isPercent) {
          this.service.addAsset(new AssetModel(
            model.name.trim(),
            model.totalValue,
            model.components.map((component) => new AssetComponentModel(
              component.assetTypeId,
              model.totalValue * component.percentage! / 100,
              component.percentage! / 100,
              component.name)),
            undefined,
            model.isPercent));
        } else {
          model.totalValue =  model.components.reduce((sum, item) => {
              const value = Number(item.value);
              return sum + (isNaN(value) ? 0 : value);
            }, 0)
          this.service.addAsset(new AssetModel(
            model.name.trim(),
            model.totalValue,
            model.components.map((component) => new AssetComponentModel(
              component.assetTypeId,
              component.value,
              component.value / model.totalValue,
              component.name)),
            undefined,
            false
          ));
        }
        this.cdr.detectChanges();
      }
     });
  }

  public editAsset(asset: AssetModel): void {
    console.log(this.service.bullion());
    this.dialog.open(AddEditAssetComponent, {
      data: {
        addOrEdit: 'Edit',
        assetTypes: this.service.assetTypes(),
        assetNames: this.service.assetNames,
        asset: new AddEditAssetModel(
          asset.name,
          asset.assetComponents.length > 1 ? null : asset.assetComponents[0].assetTypeId,
          asset.isPercent,
          asset.totalValue,
          asset.assetComponents,
          this.isBullion(asset) ? this.service.bullion() :[])
      }
    })
  }

  public getDisabled(asset: AssetModel): boolean {
    return (asset.assetComponents.length > 1 && !asset.isPercent) || this.isBullion(asset) ? true : false;
  }

  private isBullion(asset: AssetModel): boolean {
    return asset.name.toLowerCase() === 'bullion';
  }

  public onBlur($event: any, asset: AssetModel ): void {
    const val = Number($event.target.value);
    asset.assetComponents.forEach((component) => {
      component.value = val * component.percentage!;
    });
  }
}
