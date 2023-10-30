/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TemplatesLocataireEtatDesLieuxComponent } from './templates-locataire-etat-des-lieux.component';

describe('TemplatesLocataireEtatDesLieuxComponent', () => {
  let component: TemplatesLocataireEtatDesLieuxComponent;
  let fixture: ComponentFixture<TemplatesLocataireEtatDesLieuxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TemplatesLocataireEtatDesLieuxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplatesLocataireEtatDesLieuxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
