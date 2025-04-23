import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslocoDirective } from '@jsverse/transloco';

@Component({
  selector: 'app-not-found',
  imports: [CommonModule, TranslocoDirective],
  templateUrl: './not-found.component.html',
  standalone: true,
})
export class NotFoundComponent {}
