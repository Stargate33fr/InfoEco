/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MoleculesRechercheAppartementComponent } from './molecules-recherche-appartement.component';

describe('MoleculesRechercheAppartementComponent', () => {
  let component: MoleculesRechercheAppartementComponent;
  let fixture: ComponentFixture<MoleculesRechercheAppartementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoleculesRechercheAppartementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoleculesRechercheAppartementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
