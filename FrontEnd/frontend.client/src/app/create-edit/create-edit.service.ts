import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { AssetModel } from './models/asset.model';
import { BalanceSheetModel } from './models/balancesheet.model';
import { LiabilityModel } from './models/liability.model';
import { AssetTypeModel } from './models/assettype.model';
import { PreciousMetalModel } from './models/preciousmetal.model';

@Injectable({
  providedIn: 'root',
})
export class CreateEditService {
  private readonly _balanceSheet = signal<BalanceSheetModel | null>(null);
  private readonly _assets = signal<AssetModel[]>([]);
  private readonly _liabilities = signal<LiabilityModel[]>([]);
  private readonly _assetTypes = signal<AssetTypeModel[]>([]);
  private readonly _metals = signal<PreciousMetalModel[]>([]);
  private readonly _isLoading = signal<boolean>(true);

  public readonly balanceSheet = this._balanceSheet.asReadonly();
  public readonly assets = this._assets.asReadonly();
  public readonly liabilities = this._liabilities.asReadonly();
  public readonly assetTypes = this._assetTypes.asReadonly();
  public readonly assetNames = this._assets().map(asset => asset.name);
  public readonly metals = this._metals.asReadonly();
  public readonly isLoading = this._isLoading.asReadonly();


  constructor(private http: HttpClient) {}

  public getCurrent(): void {
    this.http.get<BalanceSheetModel>(environment.apiUrl + 'balance-sheet/create')
      .subscribe((result: BalanceSheetModel) => {
        this._isLoading.set(false);
        this._balanceSheet.set(result);
        result.assets.forEach((asset) => {
          asset.totalValue = asset.assetComponents.reduce((sum, item) => {
            const value = Number(item.value);
            return sum + (isNaN(value) ? 0 : value);
          }, 0);
          asset.assetComponents.forEach((component) => {
            component.percentage = component.value / asset.totalValue;
          });
        });
        if (result.bullion.length > 0) {
           result.assets.push(new AssetModel(
            'Bullion',
            result.bullion.reduce((sum, item) => {
                return sum + (item.numOunces * item.pricePerOunce);
            }, 0),
            []
           ));
        }
        this._assets.set(result.assets);
        this._liabilities.set(result.liabilities);
        this._assetTypes.set(result.assetTypes);
        this._metals.set(result.preciousMetals);
      });
  }

  public addAsset(asset: AssetModel): void {
    this._assets().push(asset);
  }
}
