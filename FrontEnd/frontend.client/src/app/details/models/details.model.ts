import { AssetModel } from "../../create-edit/models/asset.model";
import { LiabilityModel } from "../../create-edit/models/liability.model";
import { AssetShareModel } from "./asset-share.model";
import { BullionSummaryModel } from "./bullion-summary.model";
import { TargetComparisonModel } from "./target-comparison.model";

export class DetailsModel {
  constructor(
    public date: Date,
    public assets: AssetModel[],
    public liabilities: LiabilityModel[],
    public assetShares: AssetShareModel[],
    public bullionSummary: BullionSummaryModel,
    public targetComparisons: TargetComparisonModel[],
    public totalAssets: number,
    public totalLiabilities: number,
    public netWorth: number
  ) {}
}
