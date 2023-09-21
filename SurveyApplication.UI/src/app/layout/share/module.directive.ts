import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appModule]',
})
export class ModuleDirective {
  constructor(private el: ElementRef) {}

  @HostListener('click') click() {
    const navItems = document.querySelectorAll('a.collapse-item');
    navItems.forEach((navItem) => {
      navItem.classList.remove('active');
    });
    this.el.nativeElement.classList.add('active');
  }
}
