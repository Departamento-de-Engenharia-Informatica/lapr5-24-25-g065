import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PatientDashboardComponent } from './patient-dashboard.component';
import { PatientService } from '../../Services/patient.service'; 
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('PatientDashboardComponent', () => {
  let component: PatientDashboardComponent;
  let fixture: ComponentFixture<PatientDashboardComponent>;
  let patientService: jasmine.SpyObj<PatientService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    patientService = jasmine.createSpyObj('PatientService', ['getPatientProfile', 'deletePatientProfile']);
    router = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [ PatientDashboardComponent ],
      providers: [
        { provide: PatientService, useValue: patientService },
        { provide: Router, useValue: router }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should load patient data on init', () => {
    const patientData = { firstname: 'John', lastname: 'Doe', email: 'john.doe@example.com' };
    patientService.getPatientProfile.and.returnValue(of(patientData));

    component.ngOnInit();

    expect(component.patient).toEqual(patientData);
  });

  it('should call updateProfile on button click', () => {
    spyOn(component, 'updateProfile');
    const button = fixture.nativeElement.querySelector('button');
    button.click();
    expect(component.updateProfile).toHaveBeenCalled();
  });

  it('should call deleteProfile on button click', () => {
    spyOn(component, 'deleteProfile');
    const button = fixture.nativeElement.querySelectorAll('button')[1];
    button.click();
    expect(component.deleteProfile).toHaveBeenCalled();
  });
});
