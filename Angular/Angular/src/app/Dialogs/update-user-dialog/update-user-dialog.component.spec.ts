import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdateUserDialogComponent } from './update-user-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../Services/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';

describe('UpdateUserDialogComponent', () => {
  let component: UpdateUserDialogComponent;
  let fixture: ComponentFixture<UpdateUserDialogComponent>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<UpdateUserDialogComponent>>;
  let userService: jasmine.SpyObj<UserService>;

  beforeEach(() => {
    mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);
    userService = jasmine.createSpyObj('UserService', ['updateUser']);

    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule
      ],
      declarations: [UpdateUserDialogComponent],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: { id: 1, userName: 'JohnDoe', email: 'john.doe@example.com', role: 'Admin' } },
        { provide: MatDialogRef, useValue: mockDialogRef },
        { provide: UserService, useValue: userService },
        FormBuilder
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UpdateUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should pre-fill the form with user data', () => {
    expect(component.updateForm.controls['userName'].value).toBe('JohnDoe');
    expect(component.updateForm.controls['email'].value).toBe('john.doe@example.com');
    expect(component.updateForm.controls['role'].value).toBe('Admin');
  });

  it('should call updateUser on form submit', () => {
    const userData = { userName: 'UpdatedUser', email: 'updated.email@example.com', role: 'Doctor' };
    component.updateForm.setValue(userData);
    userService.updateUser.and.returnValue(of(userData)); // Simulate successful update response

    component.onSubmit();

    expect(userService.updateUser).toHaveBeenCalledWith({ 
      ...component.userToUpdate,
      ...userData 
    });
    expect(mockDialogRef.close).toHaveBeenCalled();
  });

  it('should close the dialog on cancel', () => {
    component.onCancel();
    expect(mockDialogRef.close).toHaveBeenCalled();
  });
});
