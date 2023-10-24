import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RoutingKeys } from '@core/rounting/routing-keys';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit {
  public routingKey = RoutingKeys;
  title = 'Info Eco';

  constructor() {}

  ngOnInit(): void {}
}
