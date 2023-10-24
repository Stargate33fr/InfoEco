/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoleculesExportExcelComponent } from './molecules-export-excel.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { DatePipe } from '@angular/common';
import { CustomCurrencyPipeModule } from '@core/pipes/customCurrency.pipe.module';
import { MypercentPipeModule } from '@core/pipes/custom.percent.pipe.module';

describe('MoleculesExportExcelComponent', () => {
  let component: MoleculesExportExcelComponent;
  let fixture: ComponentFixture<MoleculesExportExcelComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [MoleculesExportExcelComponent],
      imports: [SharedTestingModule, CustomCurrencyPipeModule, MypercentPipeModule],
      providers: [DatePipe],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesExportExcelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
