import { Component, OnInit } from '@angular/core';
import { AppStore } from '../../store/app.store';
import { Store, select } from '@ngrx/store';
import FriendDto, { SearchResultDto, InviteDto } from '../../models/friend.model';
import { GetFriends, SearchFriends, InviteFriend, GetInvites, AcceptInvitation } from '../../store/friends/friends.actions';
import { ModalService } from '../../controls/modal/modal.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit {
  public loading = false;
  public searchString: string;

  public isSearching = false;
  public searchResults: Array<SearchResultDto>;
  public friends: Array<FriendDto>;
  public invitedFriends: Array<FriendDto>;
  public requestedFriends: Array<InviteDto>;

  constructor(private store: Store<AppStore>, private modalService: ModalService) {
    const self = this;

    this.store.select(state => state.friendsState.searching).subscribe(value => (self.isSearching = value));
    this.store.select(state => state.friendsState.searchResult).subscribe(value => (self.searchResults = value));
    this.store.select(state => state.friendsState.friends).subscribe(value => (self.friends = value));
    this.store.select(state => state.friendsState.friendsInvited).subscribe(value => (self.invitedFriends = value));
    this.store.select(state => state.friendsState.friendRequests).subscribe(value => (self.requestedFriends = value));
  }

  modalPopup(name: string) {
    this.modalService.open(name);
  }
  closePopup(name: string) {
    this.modalService.close(name);
  }
  inviteUser(userId: string, popupName: string) {
    this.store.dispatch(new InviteFriend(userId));
    this.closePopup(popupName);
  }
  acceptInvitation(invite: InviteDto) {
    this.store.dispatch(new AcceptInvitation(invite));
  }
  searchStringChanged(text: string) {
    this.store.dispatch(new SearchFriends(text));
  }

  ngOnInit() {
    if (this.friends == null) {
      this.store.dispatch(new GetFriends());
    }
    if (this.requestedFriends == null) {
      this.store.dispatch(new GetInvites());
    }
  }
}
