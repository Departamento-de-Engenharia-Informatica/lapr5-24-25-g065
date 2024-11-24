import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationRequestDialogComponent } from './operation-request-dialog.component';

describe('OperationRequestDialogComponent', () => {
  let component: OperationRequestDialogComponent;
  let fixture: ComponentFixture<OperationRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OperationRequestDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperationRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
