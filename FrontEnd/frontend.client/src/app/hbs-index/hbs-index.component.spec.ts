import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HbsIndex } from './hbs-index';

describe('HbsIndex', () => {
  let component: HbsIndex;
  let fixture: ComponentFixture<HbsIndex>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HbsIndex]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HbsIndex);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
