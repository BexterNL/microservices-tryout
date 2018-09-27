export default class FriendDto {
  id: string;
  friendUserId: string;
  name: string;
  requestExpiresOn: string;
  isAccepted: string;

  constructor() {}
}

export class SearchResultDto {
  id: string;
  name: string;

  constructor() {}
}

export class InviteDto {
  id: string;
  name: string;
  expiresOn: Date;
}
