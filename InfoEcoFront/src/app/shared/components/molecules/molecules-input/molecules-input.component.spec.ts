/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoleculesInputComponent } from './molecules-input.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('MoleculesInputComponent', () => {
  let component: MoleculesInputComponent;
  let fixture: ComponentFixture<MoleculesInputComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [MoleculesInputComponent],
      imports: [SharedTestingModule, FormsModule, ReactiveFormsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
