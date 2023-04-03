export interface Friend {
  friendsListId: number;
  friendsId: number;
  userId: number;
  roles: string;
  friendsUsername: string;
  friendsFirstname: string;
  friendsLastname: string;
  friendsPhoto: number;
}

export interface State {
  friends: Friend[];
}
