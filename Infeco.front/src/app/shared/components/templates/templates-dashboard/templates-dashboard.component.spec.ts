/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { TemplatesDashboardComponent } from './templates-dashboard.component';
import { User } from '@models/user';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

describe('TemplatesDashboardComponent', () => {
  let component: TemplatesDashboardComponent;
  let fixture: ComponentFixture<TemplatesDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TemplatesDashboardComponent],
      imports: [SharedTestingModule, RouterTestingModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplatesDashboardComponent);
    component = fixture.componentInstance;
    const user = new User();
    user.id = 1;
    user.nom = 'Doe';
    user.prenom = 'John';
    user.courriel = 'john.doe@test.com';
    component.utilisateur$ = of(user);
    fixture.detectChanges();
  });
});
