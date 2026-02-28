import { AssetModel } from "./asset.model";
import { AssetTypeModel } from "./assettype.model";
import { LiabilityModel } from "./liability.model";
import { MetalPositionModel } from "./metalposition.model";
import { PreciousMetalModel } from "./preciousmetal.model";

export class BalanceSheetModel {
  constructor(
    public date: Date,
    public assets: AssetModel[],
    public lLiabilities: LiabilityModel[],
    public bullion: MetalPositionModel[],
    public assetTypes: AssetTypeModel[],
    public preciousMetals: PreciousMetalModel[]
  ) {}
}
