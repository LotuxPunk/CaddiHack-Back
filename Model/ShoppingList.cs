using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            Item = new HashSet<Item>();
        }

        public int ShoppingListId { get; set; }
        public string Name { get; set; }
        public bool Delivery { get; set; }
        public int Person { get; set; }
        public int Shop { get; set; }

        public virtual Person PersonNavigation { get; set; }
        public virtual Shop ShopNavigation { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }
}
