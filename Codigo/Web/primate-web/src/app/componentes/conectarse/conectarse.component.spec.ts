import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConectarseComponent } from './conectarse.component';

describe('ConectarseComponent', () => {
  let component: ConectarseComponent;
  let fixture: ComponentFixture<ConectarseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConectarseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConectarseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
