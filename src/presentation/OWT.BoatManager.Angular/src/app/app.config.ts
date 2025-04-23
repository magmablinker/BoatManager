import { ApplicationConfig, isDevMode, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { authHttpInterceptorFn, provideAuth0 } from '@auth0/auth0-angular';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideContext } from './shared/config/context.config';
import { provideTransloco } from '@jsverse/transloco';
import { TranslocoLoaderService } from './shared/services/transloco-loader.service';
import { provideMaterialOptions } from './shared/config/material-options.config';

export const appConfig: ApplicationConfig = {
    providers: [
        provideHttpClient(withInterceptors([authHttpInterceptorFn])),
        provideZoneChangeDetection({ eventCoalescing: true }),
        provideRouter(appRoutes),
        provideContext(),
        // TODO: Env file
        provideAuth0({
            clientId: 'SQ8BQw4RhudMFDbcoSUxu6wXBCj0oDjm',
            domain: 'dev-zpkrmg6d0sxk4sdx.eu.auth0.com',
            authorizationParams: {
                audience: 'https://boatmanager/api',
                redirect_uri: 'http://localhost:4200',
                scope: 'openid profile boat:read boat:write'
            },
            httpInterceptor: {
                allowedList: [
                  {
                    uri: 'https://localhost:51440/api/v1/*',
                    tokenOptions: {
                      authorizationParams: {
                        audience: 'https://boatmanager/api',
                        scope: 'boat:read boat:write'
                      }
                    }
                  }
                ]
              }
        }),
        provideTransloco({
            config: {
                availableLangs: ['en'],
                defaultLang: 'en',
                reRenderOnLangChange: true,
                prodMode: !isDevMode(),
            },
            loader: TranslocoLoaderService,
        }),
        provideMaterialOptions(),
    ],
};
