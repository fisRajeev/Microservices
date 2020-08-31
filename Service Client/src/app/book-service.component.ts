import { Component, OnInit } from '@angular/core';
import { BookService } from './../core/book.service';
import { Book } from './../model/Book';

@Component({
  selector: 'app-book-service',
  templateUrl: './book-service.component.html',
  styleUrls: ['./book-service.component.css']
})
export class BookServiceComponent implements OnInit {

  books: Book[] = [];
  constructor(private bookService: BookService) {

  }

  ngOnInit() {
    this.bookService.getBooks().subscribe(b =>
      this.books = b
    )};
}
