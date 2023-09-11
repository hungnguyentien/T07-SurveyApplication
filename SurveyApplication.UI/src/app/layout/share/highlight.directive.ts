import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
})
export class HighlightDirective {
  constructor(private el: ElementRef) {}

  @HostListener('mouseenter') onMouseEnter() {
    this.highlight('#005b8a');
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highlight(null);
  }

  // @HostListener('click') click() {
  //   const navItems = document.querySelectorAll('.nav-item');
  //   navItems.forEach((navItem) => {
  //     navItem.classList.remove('active');
  //   });
  //   this.el.nativeElement.classList.add('active');
  // }

  private highlight(color: string | null) {
    this.el.nativeElement.style.backgroundColor = color;
  }
}
