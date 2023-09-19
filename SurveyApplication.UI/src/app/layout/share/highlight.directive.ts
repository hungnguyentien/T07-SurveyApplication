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

  @HostListener('click') click() {
    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach((navItem) => {
      navItem.classList.remove('active');
      navItem.children[0].classList.add('collapsed');
      let div = navItem.children[1];
      div && div.classList.remove('show');
    });
    this.el.nativeElement.classList.add('active');
    this.el.nativeElement.children[0].classList.remove('collapsed');
    let div = this.el.nativeElement.children[1];
    div && div.classList.add('show');
  }

  private highlight(color: string | null) {
    this.el.nativeElement.style.backgroundColor = color;
  }
}
