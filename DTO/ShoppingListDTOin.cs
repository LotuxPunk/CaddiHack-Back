using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ShoppingListDTOin
    {
        public ShoppingList ShoppingList { get; set; }

        public List<ShoppingListItem> ShoppingListItem { get; set; }
    }
}
