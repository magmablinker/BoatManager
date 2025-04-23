import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { translate, TranslocoDirective } from '@jsverse/transloco';
import { BoatDetailModel } from 'src/app/shared/models/boat-detail.model';
import { BoatService } from 'src/app/shared/services/boat-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-boat-edit',
  imports: [
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogTitle,
    MatDialogClose,
    TranslocoDirective,
  ],
  templateUrl: './boat-edit.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BoatEditComponent {

  private readonly dialogRef = inject(MatDialogRef<BoatEditComponent>);
  private readonly data = inject<BoatEditDialogData>(MAT_DIALOG_DATA);

  private readonly boatService = inject(BoatService);
  private readonly snackBar = inject(MatSnackBar);
  private readonly fb = inject(NonNullableFormBuilder);

  private readonly existingBoat: BoatDetailModel | undefined;

  protected readonly formGroup;

  constructor() {
    this.formGroup = this.fb.group({
      name: this.fb.control<string>('', [Validators.required, Validators.maxLength(64)]),
      description: this.fb.control<string>('', [Validators.required, Validators.maxLength(500)])
    });

    this.existingBoat = this.data.boat;
    if(this.existingBoat) {
      this.formGroup.patchValue(this.existingBoat, { emitEvent: false });
    }
  }

  protected async submit(): Promise<void> {
    this.formGroup.markAllAsTouched();

    if (!this.formGroup.valid) {
      return;
    }

    const formValue = this.formGroup.getRawValue();

    const updateDto = {
      ...this.existingBoat,
      ...formValue,
    };

    await firstValueFrom(
      this.existingBoat
        ? this.boatService.update(this.existingBoat.id, updateDto)
        : this.boatService.create(updateDto),
    );

    this.snackBar.open(translate('Boat.Message.Saved'));
    this.dialogRef.close(true);
  }

  protected get isNew(): boolean {
    return !this.existingBoat;
  }
}

export interface BoatEditDialogData {
  boat: BoatDetailModel | undefined; 
}
