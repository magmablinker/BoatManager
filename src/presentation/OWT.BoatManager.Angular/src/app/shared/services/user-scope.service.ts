import { inject, Injectable } from "@angular/core";
import { AuthService } from "@auth0/auth0-angular";
import { jwtDecode } from "jwt-decode";
import { firstValueFrom } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class UserScopeService {
    private scopes: string[] | undefined;
    private token: string | undefined;
    private readonly authService = inject(AuthService);

    async hasScope(scope: string): Promise<boolean> {
        try {
            if(!this.scopes) {
                const token = await firstValueFrom(this.authService.getAccessTokenSilently());
                const decoded = jwtDecode<any>(token);
                this.scopes = decoded.permissions as string[];
            }

            return this.scopes.includes(scope);
        } catch (error) {
            console.error('Error checking scope:', error);
            return false;
        }
    }
}

  