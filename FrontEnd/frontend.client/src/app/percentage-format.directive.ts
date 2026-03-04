import { Directive, HostListener, ElementRef, AfterViewInit, Renderer2 } from '@angular/core';
import { PercentPipe } from '@angular/common';

@Directive({
  selector: '[appPercentFormat]',
  standalone: true,
  providers: [PercentPipe]
})
export class PercentFormatDirective implements AfterViewInit {
  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
    private percentPipe: PercentPipe
  ) {

  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.formatValue(this.el.nativeElement.value);
    }, 0);
  }

  @HostListener('focus', ['$event.target'])
  onFocus(element: any) {
    this.renderer.setProperty(this.el.nativeElement, 'value', this.parseNumber(element.value));
  }

  @HostListener('blur', ['$event.target'])
  onBlur(element: any) {
    const value = element.value;
    var rawValue = value ? this.parseNumber(value) : this.el.nativeElement.value;
    if (!isNaN(rawValue)) {
      rawValue = Number(rawValue) / 100;
    }
    const formattedValue = this.percentPipe.transform(rawValue);
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }

  private formatValue(value?: string) {
    const rawValue = value ? this.parseNumber(value) : this.el.nativeElement.value;
    const formattedValue = this.percentPipe.transform(rawValue);
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }

  private parseNumber(value: string): string {
    return value.replace(/[^\d.]/g, '');
  }
}
