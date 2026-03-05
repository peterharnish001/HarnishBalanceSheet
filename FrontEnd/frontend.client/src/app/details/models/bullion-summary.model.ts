import { MetalModel } from "./metal.model";

export class BullionSummaryModel {
  constructor(
    public bullion: MetalModel[],
    public total: number
  ) {}
}
