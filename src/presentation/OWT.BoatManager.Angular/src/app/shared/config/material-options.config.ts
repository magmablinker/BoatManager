import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogConfig } from "@angular/material/dialog";
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarConfig } from "@angular/material/snack-bar";

export function provideMaterialOptions(): EnvironmentProviders {
    return makeEnvironmentProviders([
      {
        provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
        useValue: { duration: 3000 } satisfies MatSnackBarConfig,
      },
      {
        provide: MAT_DIALOG_DEFAULT_OPTIONS,
        useValue: { minWidth: '450px' } satisfies MatDialogConfig,
      },
    ]);
  }
  