import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import FriendDto, { SearchResultDto, InviteDto } from '../models/friend.model';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {
  constructor(private httpClient: HttpClient) {}

  public GetFriends(): Observable<Array<FriendDto>> {
    const url = `${environment.gatewayApi}/friends`;
    return this.httpClient.get<Array<FriendDto>>(url);
  }
  public GetInvites(): Observable<Array<InviteDto>> {
    const url = `${environment.gatewayApi}/friends/invites`;
    return this.httpClient.get<Array<InviteDto>>(url);
  }

  public Search(query: string): Observable<Array<SearchResultDto>> {
    const url = `${environment.gatewayApi}/friends/search?q=${query}`;
    return this.httpClient.get<Array<SearchResultDto>>(url);
  }
  public Invite(userId: string): Observable<FriendDto> {
    const url = `${environment.gatewayApi}/friends/invite/${userId}`;
    return this.httpClient.get<FriendDto>(url);
  }
  public AcceptInvitation(invitationId: string): Observable<FriendDto> {
    const url = `${environment.gatewayApi}/invite/${invitationId}/accept`;
    return this.httpClient.get<FriendDto>(url);
  }
}
