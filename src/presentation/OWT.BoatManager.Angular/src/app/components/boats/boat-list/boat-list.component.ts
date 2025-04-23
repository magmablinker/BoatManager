import { ChangeDetectionStrategy, Component, DestroyRef, effect, inject, signal } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BoatService } from 'src/app/shared/services/boat-service';
import { BoatDetailModel } from 'src/app/shared/models/boat-detail.model';
import { translate, TranslocoDirective } from '@jsverse/transloco';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { HasScopeDirective } from 'src/app/shared/directives/has-scope.directive';
import { Scopes } from 'src/app/shared/models/scopes.model';
import { BoatEditComponent, BoatEditDialogData } from '../boat-edit/boat-edit.component';
import { filter, firstValueFrom, take } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-boat-list',
  imports: [
    CommonModule,
    MatTableModule,
    TranslocoDirective,
    MatButtonModule,
    MatIconModule,
    HasScopeDirective,
    RouterLink
  ],
  templateUrl: './boat-list.component.html',
  styleUrl: './boat-list.component.css',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BoatListComponent {

  private readonly boatService = inject(BoatService);
  private readonly dialog = inject(MatDialog);
  private readonly destroyRef = inject(DestroyRef);
  private readonly snackBar = inject(MatSnackBar);
  private readonly router = inject(Router);

  protected readonly dataSource = new MatTableDataSource<BoatDetailModel>();
  protected readonly selection = new SelectionModel<BoatDetailModel>(false, []);
  protected readonly boats = signal<BoatDetailModel[]>([]);
  protected readonly displayedColumns: string[] = [
    'name' satisfies keyof BoatDetailModel, 
    'description' satisfies keyof BoatDetailModel
  ];
  protected readonly Scopes = Scopes;

  constructor() {
    effect(() => {
      const boats = this.boats();
      this.dataSource.data = boats;
    });

    this.load();
  }

  protected async openEditDialog(id?: string): Promise<void> {
    let data: BoatEditDialogData = {
      boat: undefined,
    };
    if(id) {
      data.boat = await firstValueFrom(this.boatService.getById(id));
    }

    const dialogRef = this.dialog.open(BoatEditComponent, {
      data: data,
    });

    dialogRef
      .afterClosed()
      .pipe(
        take(1),
        filter((result: boolean | undefined) => result !== undefined && result),
      )
      .subscribe(() => this.load());
  }

  protected async delete(id: string): Promise<void> {
    if(!confirm(translate('Common.ConfirmDelete'))) return;

    await firstValueFrom(this.boatService.delete(id));

    this.selection.clear();
    this.load();

    this.snackBar.open(translate('Boat.Message.Deleted'));
  }

  protected load(): void {
    this.boatService.getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(boats => this.boats.set(boats));
  }

  protected openDetails(id: string) {
    this.router.navigate(['boat', id]);
  }

}
