import {Routes} from "@angular/router";
import {HomepageComponent} from "./features/pages/homepage/homepage.component";
import {UserOverviewComponent} from "./features/pages/user-page/user-overview/user-overview.component";
import {TrackComponent} from "./features/pages/user-page/track/track.component";
import {LoginComponent} from "./features/pages/account/login/login.component";
import {RegisterComponent} from "./features/pages/account/register/register.component";
import {NotFoundComponent} from "./features/error/not-found/not-found.component";
import {UserProfileComponent} from "./features/pages/user-profile/user-profile.component";
import {ServerErrorComponent} from "./features/error/server-error/server-error.component";
import {authGuard} from "./core/guards/auth.guard";
import {AlbumUploadComponent} from "./features/pages/user-page/album-upload/album-upload.component";

export const routes: Routes = [
    {path: "", component: HomepageComponent,},
    {path: "user/:id", component: UserOverviewComponent, canActivate: [authGuard]},
    {path: "track/:id", component: TrackComponent, canActivate: [authGuard]},
    {path: "account/login", component: LoginComponent},
    {path: "account/register", component: RegisterComponent},
    {path: "user/profile/:id", component: UserProfileComponent, canActivate: [authGuard]},
    {path: "upload", component: AlbumUploadComponent, canActivate: [authGuard]},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'},
  ];
