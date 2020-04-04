using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            ShoppingListItem = new HashSet<ShoppingListItem>();
        }

        public int ShoppingListId { get; set; }
        public string Name { get; set; }
        public bool Delivered { get; set; }
        public int? Shop { get; set; }
        public int? Owner { get; set; }
        public int? Deliverer { get; set; }

        public virtual Person DelivererNavigation { get; set; }
        public virtual Person OwnerNavigation { get; set; }
        public virtual Shop ShopNavigation { get; set; }
        public virtual ICollection<ShoppingListItem> ShoppingListItem { get; set; }
    }
}
