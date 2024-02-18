import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimatesComponent } from './primates.component';

describe('PrimatesComponent', () => {
  let component: PrimatesComponent;
  let fixture: ComponentFixture<PrimatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrimatesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrimatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
