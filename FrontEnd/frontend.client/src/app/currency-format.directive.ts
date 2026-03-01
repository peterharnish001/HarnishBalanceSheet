import { Directive, HostListener, ElementRef, AfterViewInit, Renderer2 } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Directive({
  selector: '[appCurrencyFormat]',
  standalone: true,
  providers: [CurrencyPipe]
})
export class CurrencyFormatDirective implements AfterViewInit {
  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
    private currencyPipe: CurrencyPipe
  ) {

  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.formatValue(this.el.nativeElement.value);
    }, 0);
  }

  @HostListener('focus', ['$event.target'])
  onFocus(element: any) {
    this.renderer.setProperty(this.el.nativeElement, 'value', this.parseCurrency(element.value));
  }

  @HostListener('blur', ['$event.target'])
  onBlur(element: any) {
    this.formatValue(element.value);
  }

  private formatValue(value?: string) {
    const rawValue = value ? this.parseCurrency(value) : this.el.nativeElement.value;
    const formattedValue = this.currencyPipe.transform(rawValue, 'USD', 'symbol', '1.2-2');
    this.renderer.setProperty(this.el.nativeElement, 'value', formattedValue);
  }

  private parseCurrency(value: string): string {
    return value.replace(/[^\d.-]/g, '');
  }
}
