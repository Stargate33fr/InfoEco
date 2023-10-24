import { ChangeDetectionStrategy, Component, forwardRef, OnDestroy, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { RoutingKeys } from '@core/rounting/routing-keys';

@Component({
  selector: 'pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PagesComponent),
      multi: true,
    },
  ],
})
export class PagesComponent implements OnInit, OnDestroy {
  routingKeys = RoutingKeys;

  constructor() {}

  ngOnDestroy(): void {}

  ngOnInit(): void {}

  onMenuHover() {
    const menuVertical = document.querySelectorAll('.ant-menu-vertical');
    menuVertical.forEach((el) => {
      (el as HTMLElement).style.setProperty('width', '250px');
    });
  }
}
