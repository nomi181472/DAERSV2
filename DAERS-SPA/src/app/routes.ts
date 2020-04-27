import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { UserListComponent } from "./user-list/user-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ListsComponent } from "./lists/lists.component";
import { AuthGuard } from "./_guards/auth.guard";

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent},
    { path: 'users', component: UserListComponent, canActivate: [AuthGuard]},
    { path: 'messages', component: MessagesComponent, canActivate: [AuthGuard]},
    { path: 'lists', component: ListsComponent, canActivate: [AuthGuard]},
    { path: '**', redirectTo: 'home', pathMatch: 'full'}
];
