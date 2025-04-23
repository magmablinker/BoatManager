import { inject, Injectable } from "@angular/core";
import { API_BASE_URL } from "../config/context.config";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BoatModel } from "../models/boat.model";
import { BoatDetailModel } from "../models/boat-detail.model";

@Injectable({
    providedIn: 'root'
})
export class BoatService {
    private readonly apiBaseUrl = inject(API_BASE_URL);
    private readonly httpClient = inject(HttpClient);

    getAll(): Observable<BoatDetailModel[]> {
        return this.httpClient.get<BoatDetailModel[]>(`${this.apiBaseUrl}/v1/boats`);
    }

    getById(id: string): Observable<BoatDetailModel | undefined> {
        return this.httpClient.get<BoatDetailModel | undefined>(`${this.apiBaseUrl}/v1/boats/${id}`);
    }

    create(boat: BoatModel): Observable<Object> {
        return this.httpClient.post(`${this.apiBaseUrl}/v1/boats`, boat);
    }

    update(id: string, boat: BoatModel): Observable<Object> {
        return this.httpClient.put(`${this.apiBaseUrl}/v1/boats/${id}`, boat);
    }

    delete(id: string): Observable<Object> {
        return this.httpClient.delete(`${this.apiBaseUrl}/v1/boats/${id}`);
    }
}