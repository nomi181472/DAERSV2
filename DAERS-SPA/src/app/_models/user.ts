import { Photo } from "./photo";

export interface User {
    id: number;
    username: string;
    age: number;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    photos?: Photo[];
}
