import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { BoatService } from 'src/app/shared/services/boat-service';
import { BoatDetailModel } from 'src/app/shared/models/boat-detail.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { catchError, of, switchMap, throwError } from 'rxjs';
import {MatCardContent, MatCardModule, MatCardTitle} from '@angular/material/card';
import { TranslocoDirective } from '@jsverse/transloco';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-boat-detail',
  imports: [
    CommonModule,
    MatCardModule,
    MatCardTitle,
    MatCardContent,
    TranslocoDirective,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './boat-detail.component.html',
  standalone: true,
})
export class BoatDetailComponent {
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly boatService = inject(BoatService);

  protected readonly router = inject(Router);
  protected readonly boat = signal<BoatDetailModel | undefined>(undefined);

  constructor() {
    this.activatedRoute.paramMap
      .pipe(takeUntilDestroyed(),
        switchMap(paramMap => {
          const id = paramMap.get("id");
          if(!id) {
            return of(undefined);
          }

          return this.boatService.getById(id);
        }),
        catchError(error => {
          if (error.status === 404) {
            this.router.navigateByUrl("not-found");
            return of(undefined);
          }

          return throwError(() => error);
        }))
      .subscribe(boat => this.boat.set(boat));
  }
}
