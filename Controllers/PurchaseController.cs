using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission9_pthoma24.Models;

namespace Mission9_pthoma24.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        private Cart cart { get; set; }
        public PurchaseController (IPurchaseRepository temp, Cart c) // Create an instance of the cart, initialized to the private variable "Cart" object known as "cart"
        {
            repo = temp;
            cart = c;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }
        [HttpPost]
        public IActionResult Checkout(Purchase purchase) // Create a new purchase object
        {
            if (cart.Items.Count() == 0) // If number of items in basket is 0, or simply if there's nothing in the basket
            {
                ModelState.AddModelError("", "Your cart is empty"); // Return empty cart message
            }

            if (ModelState.IsValid) // If everything for the model has been entered correctly
            {
                purchase.Lines = cart.Items.ToArray(); // Add the items from the cart that was created to the "Lines" of the "purchase" object
                repo.SavePurchase(purchase); // Save the "purchase" object
                cart.ClearCart(); // Clear the basket

                return RedirectToPage("/PurchaseCompleted");
            }

            else
            {
                return View();
            }
        }
    }
}
