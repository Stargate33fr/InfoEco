/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MoleculesVilleComponent } from './molecules-ville.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { FormsModule } from '@angular/forms';

describe('MoleculesVilleComponent', () => {
  let component: MoleculesVilleComponent;
  let fixture: ComponentFixture<MoleculesVilleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MoleculesVilleComponent],
      imports: [SharedTestingModule, FormsModule],
    });
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesVilleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
