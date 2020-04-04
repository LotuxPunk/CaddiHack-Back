using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class ShoppingListItem
    {
        public int ShoppingList { get; set; }
        public int Item { get; set; }
        public int Quantity { get; set; }

        public virtual Item ItemNavigation { get; set; }
        public virtual ShoppingList ShoppingListNavigation { get; set; }
    }
}
