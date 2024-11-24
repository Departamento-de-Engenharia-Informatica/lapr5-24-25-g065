import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationDetailDialogComponent } from './operation-detail-dialog.component';

describe('OperationDetailDialogComponent', () => {
  let component: OperationDetailDialogComponent;
  let fixture: ComponentFixture<OperationDetailDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OperationDetailDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperationDetailDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
