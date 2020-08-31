import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Constants } from '../constants';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';
import { Book } from '../model/Book';

@Injectable()
export class BookService {
    constructor(private httpClient: HttpClient, private _authService: AuthService) { }

    public getBooks(): Observable<Book[]> {
        var accessToken = this._authService.getAccessToken();
        var headers = new HttpHeaders().set('Authorization', `Bearer ${accessToken}`);
        return this.httpClient.get<Book[]>(Constants.booksApiRoot + 'book', { headers: headers });
    }

    getBookById(bookId: number): Observable<Book> {
        return this.httpClient.get<Book>(Constants.booksApiRoot + 'book/' + bookId);
    }
}
