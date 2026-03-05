import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { AssetModel } from './models/asset.model';
import { BalanceSheetModel } from './models/balancesheet.model';
import { LiabilityModel } from './models/liability.model';
import { AssetTypeModel } from './models/assettype.model';
import { PreciousMetalModel } from './models/preciousmetal.model';
import { MetalPositionModel } from './models/metalposition.model';

@Injectable({
  providedIn: 'root',
})
export class CreateEditService {
  private readonly _balanceSheet = signal<BalanceSheetModel | null>(null);
  private readonly _assets = signal<AssetModel[]>([]);
  private readonly _liabilities = signal<LiabilityModel[]>([]);
  private readonly _assetTypes = signal<AssetTypeModel[]>([]);
  private readonly _metals = signal<PreciousMetalModel[]>([]);
  private readonly _bullion = signal<MetalPositionModel[]>([]);
  private readonly _isLoading = signal<boolean>(true);
  private readonly _assetNames = signal<string[]>([]);
  private readonly _liabilityNames = signal<string[]>([]);

  public readonly balanceSheet = this._balanceSheet.asReadonly();
  public readonly assets = this._assets.asReadonly();
  public readonly liabilities = this._liabilities.asReadonly();
  public readonly assetTypes = this._assetTypes.asReadonly();
  public readonly assetNames = this._assetNames.asReadonly();
  public readonly metals = this._metals.asReadonly();
  public readonly bullion = this._bullion.asReadonly();
  public readonly isLoading = this._isLoading.asReadonly();
  public readonly liabilityNames = this._liabilityNames.asReadonly();


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
           this._bullion.set(result.bullion);
        }
        this._assets.set(result.assets);
        this._assetNames.set(result.assets.map((asset) => asset.name));
        this._liabilities.set(result.liabilities);
        this._assetTypes.set(result.assetTypes);
        this._metals.set(result.preciousMetals);
        this._liabilityNames.set(result.liabilities.map((liability) => liability.name));
      });
  }

  public createBalanceSheet(): Observable<HttpResponse<any>> {
    const balanceSheet = new BalanceSheetModel(
      this._assets(),
      this._liabilities(),
      this._bullion()
    )
    return this.http.post<number>(environment.apiUrl + 'balance-sheet/create', balanceSheet, { observe: 'response' });
  }

  public addAsset(asset: AssetModel): void {
    this._assets().push(asset);
    this._assetNames.set(this._assets().map((asset) => asset.name));
  }

  public deleteAsset(name: string): void {
    const index = this._assets().findIndex(item => item.name === name);
    if (index !== -1) {
      this._assets().splice(index, 1);
      this._assetNames.set(this._assets().map((asset) => asset.name));
    }
  }

  public editAsset(asset: AssetModel): void {
    const index = this._assets().findIndex(item => item.name === asset.name);
    if (index !== -1) {
      this._assets()[index] = asset;
    }
  }

  public addBullion(bullion: MetalPositionModel[]): void {
    this._bullion.set(bullion);
  }

  public addLiability(liability: LiabilityModel): void {
    this._liabilities().push(liability);
    this._liabilityNames.set(this._liabilities().map((liability) => liability.name));
  }

  public deleteLiability(name: string): void {
    const index = this._liabilities().findIndex(item => item.name === name);
    if (index !== -1) {
      this._liabilities().splice(index, 1);
      this._liabilityNames.set(this._liabilities().map((liability) => liability.name));
    }
  }
}
