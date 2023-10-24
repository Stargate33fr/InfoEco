/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoleculesDatePickerComponent } from './molecules-datePicker.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('MoleculesDatePickerComponent', () => {
  let component: MoleculesDatePickerComponent;
  let fixture: ComponentFixture<MoleculesDatePickerComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [MoleculesDatePickerComponent],
      imports: [SharedTestingModule, FormsModule, ReactiveFormsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesDatePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
