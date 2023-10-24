import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
