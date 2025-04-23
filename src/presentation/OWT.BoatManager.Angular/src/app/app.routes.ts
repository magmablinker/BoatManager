import { Route } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';

export const appRoutes: Route[] = [
    {
        path: '',
        loadComponent: () => import('./components/boats/boat-list/boat-list.component').then(m => m.BoatListComponent),
        canActivate: [AuthGuard]
    },
    {
        path: 'boat/:id',
        loadComponent: () => import('./components/boats/boat-detail/boat-detail.component').then(m => m.BoatDetailComponent),
        canActivate: [AuthGuard]
    },
    {
        path: 'not-found',
        loadComponent: () => import('./shared/components/not-found/not-found.component').then(m => m.NotFoundComponent),
    }
];
