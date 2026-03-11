import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Weathers } from './weathers';

describe('Weathers', () => {
  let component: Weathers;
  let fixture: ComponentFixture<Weathers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Weathers],
    }).compileComponents();

    fixture = TestBed.createComponent(Weathers);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
