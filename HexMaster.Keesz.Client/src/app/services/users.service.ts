import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import UserInfoDto from '../models/user.info.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private httpClient: HttpClient) {}

  public GetUserInfo(): Observable<UserInfoDto> {
    const url = `${environment.gatewayApi}/users`;
    return this.httpClient.get<UserInfoDto>(url);
  }
}
