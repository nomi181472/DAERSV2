import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { UserListComponent } from "./users/user-list/user-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ListsComponent } from "./lists/lists.component";
import { AuthGuard } from "./_guards/auth.guard";
import { UserDetailComponent } from "./users/user-detail/user-detail.component";
import { UserDetailResolver } from "./_resolvers/user-detail.resolver";
import { UserListResolver } from "./_resolvers/user-list.resolver";
import { UserEditComponent } from "./users/user-edit/user-edit.component";
import { UserEditResolver } from "./_resolvers/user-edit.resolver";
import { PreventUnsavedChanges } from "./_guards/prevent-unsaved-changes.guard";

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent},
    { path: 'users', component: UserListComponent, resolve: {users: UserListResolver}, canActivate: [AuthGuard]},
    { path: 'users/:id', component: UserDetailComponent, resolve: {user: UserDetailResolver}, canActivate: [AuthGuard]},
    { path: "user/edit", component: UserEditComponent, resolve: {user: UserEditResolver}, canDeactivate: [PreventUnsavedChanges]},
    { path: 'messages', component: MessagesComponent, canActivate: [AuthGuard]},
    { path: 'lists', component: ListsComponent, canActivate: [AuthGuard]},
    { path: '**', redirectTo: 'home', pathMatch: 'full'}
];
