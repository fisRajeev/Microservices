import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
  MatButtonModule,
  MatToolbarModule,
  MatDialogModule,
  MatTableModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule
} from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CoreModule } from './core/core.module';
import { UnauthorizedComponent } from './home/unauthorized.component';
import { LogOutComponent } from './log-out/log-out.component';
import { BookServiceComponent } from './book-service/book-service.component';
import { SubscriptionServiceComponent } from './subscription-service/subscription-service.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UnauthorizedComponent,
    LogOutComponent,
    BookServiceComponent,
    SubscriptionServiceComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatDialogModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    CoreModule,
    AppRoutingModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
  entryComponents:[
    HomeComponent
  ]
})
export class AppModule { }
