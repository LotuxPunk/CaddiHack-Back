using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int ShoppingList { get; set; }

        public virtual ShoppingList ShoppingListNavigation { get; set; }
    }
}
