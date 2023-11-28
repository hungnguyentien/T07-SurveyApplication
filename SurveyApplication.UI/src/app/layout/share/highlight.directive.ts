import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
})
export class HighlightDirective {
  constructor(private el: ElementRef) {}

  @HostListener('mouseenter') onMouseEnter() {
    this.highlight('rgba(184, 222, 238, 1)');
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highlight(null);
  }

  @HostListener('click') click() {
    const checkActive =
      this.el.nativeElement.classList.value.indexOf('active') >= 0;
    const checkActiveChil =
      this.el.nativeElement.classList.value.indexOf('collapse-item') >= 0;
    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach((navItem) => {
      navItem.classList.remove('active');
      navItem.children[0].classList.add('collapsed');
      let div = navItem.children[1];
      div && div.classList.remove('show');
    });

    if (checkActiveChil) {
      const navItemsChil = document.querySelectorAll('a.collapse-item');
      navItemsChil.forEach((navItem) => {
        navItem.classList.remove('active');
      });
    }

    if (!checkActive) {
      this.el.nativeElement.classList.add('active');
      if (this.el.nativeElement.children.length > 0) {
        this.el.nativeElement.children[0].classList.remove('collapsed');
        let div = this.el.nativeElement.children[1];
        div && div.classList.add('show');
      }
    }
  }

  private highlight(color: string | null) {
    this.el.nativeElement.style.backgroundColor = color;
  }
}
