/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { TemplatesDashboardComponent } from './templates-dashboard.component';
import { User } from '@models/user';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

describe('TemplatesDashboardComponent', () => {
  let component: TemplatesDashboardComponent;
  let fixture: ComponentFixture<TemplatesDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TemplatesDashboardComponent],
      imports: [SharedTestingModule, RouterTestingModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplatesDashboardComponent);
    component = fixture.componentInstance;
    const user = new User();
    user.id = 1;
    user.nom = 'Doe';
    user.prenom = 'John';
    user.courriel = 'john.doe@test.com';
    component.utilisateur$ = of(user);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it("quand j'appelle la fonction openChoixCalculatrices sur le premier bouton", () => {
    spyOn(component, 'ouvrirChoixCalculatrices');
    const buttons = fixture.debugElement.queryAll(By.css('.button-styled-green'));
    buttons[0].nativeElement.click();
    expect(component.ouvrirChoixCalculatrices).toHaveBeenCalled();
  });

  it("quand j'appelle la fonction openChoixSimulations sur le deuxième bouton", () => {
    fixture.detectChanges();
    spyOn(component, 'ouvrirChoixSimulations');
    const buttons = fixture.debugElement.queryAll(By.css('.button-styled-green'));
    buttons[1].nativeElement.click();
    expect(component.ouvrirChoixSimulations).toHaveBeenCalled();
  });

  it("Devrait afficher bonjour prenom quand l'utilisateur est défini", () => {
    // Vérifier que le message de réussite est affiché
    const titleElement = fixture.nativeElement.querySelector('.ant-result-title');
    expect(titleElement.textContent).toContain(`Bonjour John`);
  });
});
