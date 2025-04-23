import { EnvironmentProviders, InjectionToken, makeEnvironmentProviders } from '@angular/core';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export function provideContext(): EnvironmentProviders {
    return makeEnvironmentProviders([
        { provide: API_BASE_URL, useFactory: apiBaseUrlFactory },
    ]);
}
  
export function apiBaseUrlFactory(): string {
    return 'https://localhost:51440/api';
}

  