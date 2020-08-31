import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants';
import { Observable } from 'rxjs/Observable';
import { catchError, map } from "rxjs/operators";
import { Subscription } from './../model/Subscription';
@Injectable()
export class SubscriptionService {
    constructor(private _httpClient: HttpClient) { }

    getSubscriptions(): Observable<Subscription[]> {
        return this._httpClient.get<Subscription[]>(Constants.subscriptionApiRoot + 'subscription');
    }

    getSubscriptionById(user: string): Observable<Subscription> {
      return this._httpClient.get<Subscription>(Constants.subscriptionApiRoot + 'subscription/' + user);
  }
}
