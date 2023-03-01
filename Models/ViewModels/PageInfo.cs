using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_pthoma24.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBooks { get; set; } // Keep track of total number of books
        public int BooksPerPage { get; set; } // Keep track of total number of books per page
        public int CurrentPage { get; set; } // Keep track of current page number
        public int TotalPages => (int) Math.Ceiling((double) TotalNumBooks / BooksPerPage); // Keep track of total number of pages, "(int)" casts the next expression to type "int"
    }
}
