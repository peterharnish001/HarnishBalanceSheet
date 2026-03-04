import { Directive, HostListener, ElementRef, AfterViewInit, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appNumberOfOuncesFormat]',
  standalone: true
})
export class NumberOfOuncesFormatDirective implements AfterViewInit {
  constructor(
    private el: ElementRef,
    private renderer: Renderer2
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
    this.formatValue(element.value);
  }

  private formatValue(value?: string) {
    const rawValue = value ? this.parseNumber(value) : this.el.nativeElement.value;
    const num = Number(rawValue);
    const formattedValue = num.toFixed(2)
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }

  private parseNumber(value: string): string {
    return value.replace(/[^\d.]/g, '');
  }
}
