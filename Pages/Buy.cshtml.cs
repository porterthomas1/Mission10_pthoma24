using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission9_pthoma24.Infrastructure;
using Mission9_pthoma24.Models;

namespace Mission9_pthoma24.Pages
{
    public class BuyModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public BuyModel (IBookstoreRepository temp, Cart c) // Initialize the instance of a cart
        {
            repo = temp;
            cart = c;
        }

        public void OnGet(string returnUrl) // Enable return url
        {
            ReturnUrl = returnUrl ?? "/";
        }

        // User posts data
        public IActionResult OnPost(int bookId, string returnUrl) // Enable return url
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // Add the item to the cart
            cart.AddItem(b, 1, b.Price);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        // User removes data
        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(x => x.Book.BookId == bookId).Book); // Find the BookIds that match first and then the book accosiated with each Id

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
