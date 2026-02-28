import { AssetComponentModel } from "./assetcomponent.model";

export class AssetModel {
  constructor(
    public assetId: number,
    public name: string,
    public isPercent: boolean,
    public assetComponents: AssetComponentModel[]
  ) {}
}
