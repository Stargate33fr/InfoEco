/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoleculesSelectComponent } from './molecules-select.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';

describe('MoleculesSelectComponent', () => {
  let component: MoleculesSelectComponent;
  let fixture: ComponentFixture<MoleculesSelectComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [MoleculesSelectComponent],
      imports: [SharedTestingModule, WithLoadingPipeModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
