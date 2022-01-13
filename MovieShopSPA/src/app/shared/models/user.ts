export interface User{
    nameId: number;
    family_name: string;
    given_name: string;
    emial: string;
    role: Array<string>;
    exp: string;
    alias: string
    isAdmin: boolean;
    birthDate:Date;
}