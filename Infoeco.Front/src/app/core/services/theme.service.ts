import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable()
export class ThemeService {
  linkRef: HTMLLinkElement | undefined;
  // TODO test theme
  darkTheme: Record<string, string> = {
    name: 'Dark',
    href: 'assets/themes/dark.css',
  };

  constructor(@Inject(DOCUMENT) private document: Document) {}

  setTheme(themeName: string) {
    const headElement: HTMLHeadElement = this.document.querySelector('head') as HTMLHeadElement;

    if (this.darkTheme['name'] === themeName) {
      this.linkRef = this.document.createElement('link');
      this.linkRef.rel = 'stylesheet';
      this.linkRef.href = this.darkTheme['href'];
      headElement.appendChild(this.linkRef);
    } else {
      const link = headElement.querySelectorAll(`link[href='${this.darkTheme['href']}']`)[0];
      if (link && link.parentNode) {
        link.parentNode.removeChild(link);
      }
    }
  }
}
