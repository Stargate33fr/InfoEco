/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TemplatesLocatairesQuittanceDeLoyerComponent } from './templates-locataires-quittance-de-loyer.component';

describe('TemplatesLocatairesQuittanceDeLoyerComponent', () => {
  let component: TemplatesLocatairesQuittanceDeLoyerComponent;
  let fixture: ComponentFixture<TemplatesLocatairesQuittanceDeLoyerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TemplatesLocatairesQuittanceDeLoyerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplatesLocatairesQuittanceDeLoyerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
