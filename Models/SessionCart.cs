using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission9_pthoma24.Infrastructure;

// Keeps track of all the information for each session

namespace Mission9_pthoma24.Models
{
    public class SessionCart : Cart // Inherit from the "Cart" class
    {
        public static Cart GetCart (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart(); // If a session has already been started, is it there? If so, return old session cart (left). Else, return new session cart (right)

            cart.Session = session;

            return cart;
        }

        [JsonIgnore] // Prevents a property from being serialized/deserialized
        public ISession Session { get; set; }

        public override void AddItem(Book book, int qty, double price)
        {
            base.AddItem(book, qty, price);
            Session.SetJson("Cart", this); // "this" is a keyword that refers to the current instance of a class, also a modifier of the first parameter of an extension method
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Cart", this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }
    }
}
