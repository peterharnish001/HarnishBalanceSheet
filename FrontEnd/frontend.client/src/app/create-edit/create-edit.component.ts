import { Component, inject } from '@angular/core';
import { AddEditAssetModel } from './models/addeditasset.model';
import { AssetModel } from './models/asset.model';
import { LiabilityModel } from './models/liability.model';
import { CreateEditService } from './create-edit.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CurrencyFormatDirective } from '../currency-format.directive';
import { MatDialog } from '@angular/material/dialog';
import { AddEditAssetComponent } from '../add-edit-asset/add-edit-asset.component';
import { DeleteAssetComponent } from './delete-asset/delete-asset.component';
import { AddLiabilityComponent } from './add-liability/add-liability.component';
import { DeleteLiabilityComponent } from './delete-liability/delete-liability.component';
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
        assetNames: this.service.assetNames(),
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
              model.totalValue * component.percentage!,
              component.percentage!,
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
    this.dialog.open(AddEditAssetComponent, {
      data: {
        addOrEdit: 'Edit',
        assetTypes: this.service.assetTypes(),
        assetNames: this.service.assetNames,
        asset: new AddEditAssetModel(
          asset.name,
          this.isBullion(asset) ? 3 : asset.assetComponents.length > 1 ? null : asset.assetComponents[0].assetTypeId,
          asset.isPercent,
          asset.totalValue,
          asset.assetComponents,
          this.isBullion(asset) ? this.service.bullion() :[])
      }
    })
    .afterClosed()
    .subscribe((result: AddEditAssetModel | null) => {
      if (result !== null) {
        const model = result as AddEditAssetModel;
        const asset = this.service.assets().find((item) => item.name === model.name);
        if (asset !== undefined) {
          if (model.name.trim().toLowerCase() === 'bullion') {
          asset.totalValue =  model.bullion.reduce((sum, item) => {
              const value = Number(item.numOunces);
              return sum + (isNaN(value) ? 0 : value * item.pricePerOunce);
            }, 0);
          this.service.addBullion(model.bullion.map((metal) => new MetalPositionModel(
            metal.preciousMetalId,
            metal.metalName,
            metal.numOunces,
            metal.pricePerOunce,
            metal.numOunces * metal.pricePerOunce)));
          } else if (model.type !== null) {
            asset.totalValue = model.totalValue;
            asset.assetComponents = [new AssetComponentModel(model.type, model.totalValue)];
            asset.isPercent = false;
          } else if (model.isPercent) {
            asset.totalValue = model.totalValue;
            asset.assetComponents = model.components.map((component) => new AssetComponentModel(
              component.assetTypeId,
              model.totalValue * component.percentage!,
              component.percentage!,
              component.name));
            asset.isPercent = model.isPercent;
          } else {
            asset.totalValue =  model.components.reduce((sum, item) => {
              const value = Number(item.value);
              return sum + (isNaN(value) ? 0 : value);
            }, 0)
            asset.assetComponents = model.components.map((component) => new AssetComponentModel(
              component.assetTypeId,
              component.value,
              component.value / model.totalValue,
              component.name));
            asset.isPercent = false;
          }
          this.service.editAsset(asset);
          this.cdr.detectChanges();
        }
      }
     });
  }

  public deleteAsset(asset: AssetModel): void {
    this.dialog.open(DeleteAssetComponent, {
      data: {
        name: asset.name
      }
    })
    .afterClosed()
    .subscribe((result: string | null) => {
      if (result !== null) {
        this.service.deleteAsset(result);
        this.cdr.detectChanges();
      }
    });
  }

  public addLiability(): void {
    this.dialog.open(AddLiabilityComponent, {
        data: {
          liabilityNames: this.service.liabilityNames()
        }
      })
      .afterClosed()
      .subscribe((result: LiabilityModel | null) => {
      if (result !== null) {
        const model = result as LiabilityModel;
        this.service.addLiability(model);
        this.cdr.detectChanges();
      }
     });
  }

  public deleteLiability(liability: LiabilityModel): void {
    this.dialog.open(DeleteLiabilityComponent, {
      data: {
        name: liability.name
      }
    })
    .afterClosed()
    .subscribe((result: string | null) => {
      if (result !== null) {
        this.service.deleteLiability(result);
        this.cdr.detectChanges();
      }
    });;
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
