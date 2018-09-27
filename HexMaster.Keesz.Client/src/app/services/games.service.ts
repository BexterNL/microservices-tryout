import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import GameStatsDto from '../models/games.model';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  constructor(private httpClient: HttpClient) {}

  public GetUserStatistics(): Observable<GameStatsDto> {
    const url = `${environment.gatewayApi}/games/stats`;
    return this.httpClient.get<GameStatsDto>(url);
  }
}
