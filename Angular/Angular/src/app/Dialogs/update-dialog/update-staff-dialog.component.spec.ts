import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateStaffDialogComponent } from './update-staff-dialog.component';

describe('UpdateDialogComponent', () => {
  let component: UpdateStaffDialogComponent;
  let fixture: ComponentFixture<UpdateStaffDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateStaffDialogComponent]
    });
    fixture = TestBed.createComponent(UpdateStaffDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
