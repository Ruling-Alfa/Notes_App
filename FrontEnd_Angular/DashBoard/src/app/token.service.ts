import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private token: string = '';
  constructor() { }

  public SetToken(token: string) {
    this.token = token;
  }

  public GetToken() {
    return this.token;
  }
}
