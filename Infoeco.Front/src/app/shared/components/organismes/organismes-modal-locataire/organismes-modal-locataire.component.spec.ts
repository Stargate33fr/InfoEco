/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OrganismesModalLocataireComponent } from './organismes-modal-locataire.component';

describe('OrganismesModalLocataireComponent', () => {
  let component: OrganismesModalLocataireComponent;
  let fixture: ComponentFixture<OrganismesModalLocataireComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrganismesModalLocataireComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganismesModalLocataireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
