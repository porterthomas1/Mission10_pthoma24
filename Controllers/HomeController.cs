using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission9_pthoma24.Models;
using Mission9_pthoma24.Models.ViewModels;

namespace Mission9_pthoma24.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookCategory || bookCategory == null) // "bookCategory == null" allows the home page to display all the books, otherwise none would be displayed on the home link
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize) // For each page, skip the number of results allowed per page * the previous page number
                .Take(pageSize), // Show only 10 results

                PageInfo = new PageInfo
                {
                    TotalNumBooks = 
                        (bookCategory == null // If the category is null...
                            ? repo.Books.Count() // Count all the books
                            : repo.Books.Where(x => x.Category == bookCategory).Count()), // Otherwise, count all the books only where the category is equal to the one selected
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
