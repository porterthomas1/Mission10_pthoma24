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

        public BuyModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl) // Enable return url (start)
        {
            ReturnUrl = returnUrl ?? "/";  // Enable return url (end)
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); // "??" means that if the cart is not null, do the left side, otherwise do the right side
        }

        // User posts data
        public IActionResult OnPost(int bookId, string returnUrl) // enable return url (start)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // Search for session. If no session is found (is null), then create a new cart
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            // Add the item to the cart
            cart.AddItem(b, 1, b.Price);

            // Set the json file so that cart is retained from page to page
            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl }); // Enable return url (end) 
        }
    }
}
