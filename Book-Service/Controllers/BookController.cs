using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookService;
using BookService.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookService.Controllers
{
    [Authorize]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        // GET book
        [HttpGet]
        public Book[] Get()
        {
            Books obj = new Books();
            return obj.GetBooks();
        }

        // GET /book/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            Books obj = new Books();
            Book b = obj.GetBookByID(id);

            if (b.bookName.Length > 0)
            {
                return b;
            }
            else
            {
                return null;
            }
        }

        // PUT /book/UpdateAvailability/{bookId}/{incremental_count}
        [HttpPut("UpdateAvailability/{bookId}/{incremental_count}")]
        public Book UpdateAvailability(int bookId, int incremental_count)
        {
            Books obj = new Books();
            bool result = obj.UpdateAvailability(bookId, incremental_count);

            if (result)
            {
                return obj.GetBookByID(bookId);
            }
            else
            {
                return null;
            }
        }
    }
}
