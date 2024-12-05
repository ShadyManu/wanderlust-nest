import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private userEndpoint = environment.apiUrl;

  public register(email: string, password: string) {
    return this.http.post(`${this.userEndpoint}/register`, { email, password });
  }

  public login(email: string, password: string) {
    return this.http.post(`${this.userEndpoint}/login`, { email, password });
  }

  public refreshToken(refreshToken: string) {
    return this.http.post(`${this.userEndpoint}/refresh`, { refreshToken });
  }
}
