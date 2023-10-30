import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { OrganismesHeaderComponent } from './organismes-header.component';

describe('OrganismesHeaderComponent', () => {
  let component: OrganismesHeaderComponent;
  let fixture: ComponentFixture<OrganismesHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule, ReactiveFormsModule, FormsModule, WithLoadingPipeModule],
      declarations: [OrganismesHeaderComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganismesHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
