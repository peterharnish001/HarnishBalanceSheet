import { AssetComponentModel } from "./asset-component.model";

export class AssetShareModel {
  constructor(
    public name: string,
    public total: number,
    public assetComponents: AssetComponentModel[]
  ) {}
}
