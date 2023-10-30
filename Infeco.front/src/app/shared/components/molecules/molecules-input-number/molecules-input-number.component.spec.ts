/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoleculesInputNumberComponent } from './molecules-input-number.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';

describe('MoleculesInputNumberComponent', () => {
  let component: MoleculesInputNumberComponent;
  let fixture: ComponentFixture<MoleculesInputNumberComponent>;

  beforeEach(async () => {
    // Création du mock pour NzNotificationService
    await TestBed.configureTestingModule({
      declarations: [MoleculesInputNumberComponent],
      imports: [SharedTestingModule, ReactiveFormsModule, FormsModule, NumberFormatPipeModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesInputNumberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
