using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_pthoma24.Models
{
    public class Cart
    {
        // Create the list of line items (books)
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        // Create the function to add a new item to the line item list
        public virtual void AddItem (Book book, int qty, double price) // "virtual" allows this method to be overridden when we inherit from it
        {
            CartLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            // If the item doesn't already exist in the new CartLineItem object, add the new item
            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = book,
                    Quantity = qty,
                    Price = price
                });
            }
            // If the item already exists in the new CartLineItem object, update the item quantity
            else
            {
                line.Quantity += qty;
            }
        }

        public virtual void RemoveItem (Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }

        public virtual void ClearCart()
        {
            Items.Clear();
        }

        // Calculate total cost of cart
        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Price);

            return sum;
        }
    }

    public class CartLineItem // Info for each CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
