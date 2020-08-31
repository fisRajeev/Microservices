import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './home/unauthorized.component';
import { LogOutComponent } from './log-out/log-out.component';
import { BookServiceComponent } from './book-service/book-service.component';
import { SubscriptionServiceComponent } from './subscription-service/subscription-service.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'book', component: BookServiceComponent},
    { path: 'subscription', component: SubscriptionServiceComponent },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: 'logout', component: LogOutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
exports: [RouterModule]
})
export class AppRoutingModule { }
