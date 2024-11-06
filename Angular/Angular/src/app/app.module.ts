import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { UpdateStaffDialogComponent } from './Dialogs/update-staff-dialog/update-staff-dialog.component';
import { DeleteStaffDialogComponent } from './Dialogs/delete-staff-dialog/delete-staff-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    UpdateStaffDialogComponent,
    DeleteStaffDialogComponent
  ],
  imports: [
    BrowserModule,
    MatDialogModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
