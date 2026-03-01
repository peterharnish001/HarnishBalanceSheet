import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { AssetModel } from './models/asset.model';
import { BalanceSheetModel } from './models/balancesheet.model';
import { LiabilityModel } from './models/liability.model';
import { AssetTypeModel } from './models/assettype.model';

@Injectable({
  providedIn: 'root',
})
export class CreateEditService {
  private readonly _balanceSheet = signal<BalanceSheetModel | null>(null);
  private readonly _assets = signal<AssetModel[]>([]);
  private readonly _liabilities = signal<LiabilityModel[]>([]);
  private readonly _assetTypes = signal<AssetTypeModel[]>([]);

  public readonly balanceSheet = this._balanceSheet.asReadonly();
  public readonly assets = this._assets.asReadonly();
  public readonly liabilities = this._liabilities.asReadonly();
  public readonly assetTypes = this._assetTypes.asReadonly();
  public readonly assetNames = this._assets().map(asset => asset.name);

  constructor(private http: HttpClient) {}

  public getCurrent(): void {
    this.http.get<BalanceSheetModel>(environment.apiUrl + 'balance-sheet/create')
      .subscribe((result: BalanceSheetModel) => {
        this._balanceSheet.set(result);
        result.assets.forEach((asset) => {
          asset.totalValue = asset.assetComponents.reduce((sum, item) => {
            const value = Number(item.value);
            return sum + (isNaN(value) ? 0 : value);
          }, 0);
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
      });
  }
}
