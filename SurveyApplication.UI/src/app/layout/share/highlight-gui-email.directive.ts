import { Directive, ElementRef, Input } from '@angular/core';
import { GuiEmailTrangThai } from '@app/enums';

@Directive({
  selector: '[appHighlightGuiEmail]',
})
export class HighlightGuiEmailDirective {
  @Input('appHighlightGuiEmail') trangThai: number = 0;
  constructor(private el: ElementRef) {
    this.highlight();
  }

  private highlight() {
    this.el.nativeElement.style.fontWeight = 600;
    if (this.trangThai == GuiEmailTrangThai.ThanhCong)
      this.el.nativeElement.style.color = 'rgba(36, 119, 69, 1)';
    else if (this.trangThai == GuiEmailTrangThai.GuiLoi)
      this.el.nativeElement.style.color = 'rgba(229, 13, 13, 1)';
    else this.el.nativeElement.style.color = 'rgba(65, 76, 82, 1)';
  }
}
