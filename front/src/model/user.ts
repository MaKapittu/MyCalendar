export interface User {
    id?: number;
    firstname?: string;
    lastname?: string;
    username: string;
    password: string;
    photo?: number;
    email?: string;
    groupsId?: number[];
    birthday?: Date;
}

export interface Group {
    name?: string;
    id?: number;
    usersId?: number[];
    statuses?: {userId: number, status: string}[];
}

export interface State {
    groups: Group[];
}

export interface AuthResponse {
    token: string;
}
export interface ILoginRequest {
    username: string;
    password: string;
}
export interface IRegisterRequest {
    username: string;
    password: string;
    firstname: string;
    lastname: string;
    birthday: Date;
    email: string;
}