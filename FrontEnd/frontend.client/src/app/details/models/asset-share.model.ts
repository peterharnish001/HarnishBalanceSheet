import { AssetComponentModel } from "../../create-edit/models/assetcomponent.model";

export class AssetShareModel {
  constructor(
    public name: string,
    public total: number,
    public assetComponents: AssetComponentModel[]
  ) {}
}
