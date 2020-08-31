using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Models
{
    public class Book
    {
        public int bookID { get; set; }
        public string bookName { get; set; }
 
        public string author { get; set; }

        public int available_copies { get; set; }

        public int total_copies { get; set; }
    }
}
