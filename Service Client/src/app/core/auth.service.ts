import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { UserManager, User, WebStorageStateStore, Log } from 'oidc-client';
import { Constants } from '../constants';
import { Utils } from './utils';
import { AuthContext } from '../model/auth-context';


@Injectable()
export class AuthService {
  private _userManager: UserManager;
  private _user: User;
  authContext: AuthContext;
 private encodedURL = encodeURIComponent('http://localhost:4200/?postLogout=true');

  constructor(private httpClient: HttpClient) {
    Log.logger = console;
    var config = {
      authority: Constants.stsAuthority,
      client_id: Constants.clientId,
      redirect_uri: `${Constants.clientRoot}assets/oidc-login-redirect.html`,
      scope: 'openid books-api profile',
      response_type: 'id_token token',
      post_logout_redirect_uri: `${Constants.clientRoot}?postLogout=true`,
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      automaticSilentRenew: true,
      silent_redirect_uri: `${Constants.clientRoot}assets/silent-redirect.html`,
      /* Extra configuration settings for Auth0
      metadata:{
        //authorization_endpoint: `https://rajeevchopra.auth0.com/authorize`,
        authorization_endpoint: `https://rajeevchopra.auth0.com/authorize?audience=projects-api`,
        issuer: `https://rajeevchopra.auth0.com/`,
        jwks_uri: `https://rajeevchopra.auth0.com/.well-known/jwks.json`,
        end_session_endpoint: `${Constants.stsAuthority}v2/logout?returnTo=http://localhost:4200/?postLogout=true&client_id=${Constants.clientId}`
      }
      //*/
    };
    this._userManager = new UserManager(config);
    this._userManager.getUser().then(user => {
      if (user && !user.expired) {
        this._user = user;
        //this.loadSecurityContext();
      }
    });
    this._userManager.events.addUserLoaded(args => {
      this._userManager.getUser().then(user => {
        this._user = user;
        //this.loadSecurityContext();
      });
    });
  }

  login(): Promise<any> {
    return this._userManager.signinRedirect();
  }

  logout(): Promise<any> {
    return this._userManager.signoutRedirect();
  }

  isLoggedIn(): boolean {
    return this._user && this._user.access_token && !this._user.expired;
  }

  getAccessToken(): string {
    return this._user ? this._user.access_token : '';
  }

  signoutRedirectCallback(): Promise<any> {
    return this._userManager.signoutRedirectCallback();
  }
}
