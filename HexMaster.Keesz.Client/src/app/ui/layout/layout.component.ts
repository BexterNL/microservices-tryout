import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngrx/store';
import { AppStore } from '../../store/app.store';
import UserInfoDto from '../../models/user.info.model';
import { GetUserInfo } from '../../store/users/user.actions';
import { GetFriends, GetInvites } from '../../store/friends/friends.actions';
@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  public userInfo: UserInfoDto;
  private notificationHubConnection: HubConnection | undefined;
  private updatesHubConnection: HubConnection | undefined;

  constructor(private toastr: ToastrService, private store: Store<AppStore>) {
    const self = this;
    this.store
      .select(state => state.userState.userInfo)
      .filter(value => value != null)
      .subscribe(value => {
        self.userInfo = value;
        self.registerSignalRListener(this.userInfo.id);
      });
  }

  registerSignalRListener(userId: string) {
    this.notificationHubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://live.keesz.int:54604/notification')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.notificationHubConnection
      .start()
      .then(() => {
        this.notificationHubConnection.on('notificationReceived', (headline: string, message: string) => {
          console.log(`Data received over SignalR: ${headline}`);
          this.toastr.success(message, headline);
        });
        this.notificationHubConnection.invoke('RegisterUserId', userId);
      })
      .catch(err => console.error(err.toString()));

    this.updatesHubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://live.keesz.int:54604/updates')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.updatesHubConnection
      .start()
      .then(() => {
        this.updatesHubConnection.on('updateFriends', () => {
          this.store.dispatch(new GetFriends());
        });
        this.updatesHubConnection.on('updateFriendRequests', () => {
          this.store.dispatch(new GetInvites());
        });
        this.updatesHubConnection.invoke('RegisterUserId', userId);
      })
      .catch(err => console.error(err.toString()));
  }

  ngOnInit() {
    if (this.userInfo == null) {
      this.store.dispatch(new GetUserInfo());
    }
  }
}
