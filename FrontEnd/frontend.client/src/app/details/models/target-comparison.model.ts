export class TargetComparisonModel {
  constructor(
    public name: string,
    public target: number,
    public actual: number,
    public difference: number
  ) {}
}
