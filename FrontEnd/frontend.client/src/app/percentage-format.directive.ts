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
    this.renderer.setProperty(this.el.nativeElement, 'value', this.parsePercent(element.value));
  }

  @HostListener('blur', ['$event.target'])
  onBlur(element: any) {
    this.formatValue(element.value);
  }

  private formatValue(value?: string) {
    const rawValue = value ? this.parsePercent(value) : this.el.nativeElement.value;
    const num = Number(rawValue);
    const formattedValue = this.percentPipe.transform(num / 100);
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }

  private parsePercent(value: string): string {
    return value.replace(/[^\d.-]/g, '');
  }
}
