import { NgModule } from '@angular/core';
import { BookService } from './book.service';
import { AuthService } from './auth.service';
import { AuthInterceptor } from './auth.interceptor';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import { SubscriptionService } from './subscription.service';

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        BookService,
        SubscriptionService,
        AuthService
    ],
})
export class CoreModule { }
