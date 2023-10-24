/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { TemplatesLoginComponent } from './templates-login.component';

describe('TemplatesLoginComponent', () => {
  let component: TemplatesLoginComponent;
  let fixture: ComponentFixture<TemplatesLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TemplatesLoginComponent],
      imports: [SharedTestingModule, ReactiveFormsModule],
    }).compileComponents();
  });

  beforeEach(async () => {
    fixture = TestBed.createComponent(TemplatesLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
