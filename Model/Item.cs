using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Item
    {
        public Item()
        {
            ShoppingListItem = new HashSet<ShoppingListItem>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public string Unit { get; set; }
        public int Shop { get; set; }
        public double Price { get; set; }

        public virtual Shop ShopNavigation { get; set; }
        public virtual ICollection<ShoppingListItem> ShoppingListItem { get; set; }
    }
}
