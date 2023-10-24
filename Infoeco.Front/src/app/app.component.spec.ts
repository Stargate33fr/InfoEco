import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { PagesComponent } from '@pages/pages.component';
import { AppComponent } from './app.component';
import { appRoutes } from './app.routes';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent, PagesComponent],
      imports: [SharedTestingModule, RouterModule.forRoot(appRoutes)],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'TopInvestV6'`, () => {
    fixture = TestBed.createComponent(AppComponent);
    expect(component.title).toEqual('TopInvestV6');
  });
});
