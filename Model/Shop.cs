using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Shop
    {
        public Shop()
        {
            Favorite = new HashSet<Favorite>();
            Item = new HashSet<Item>();
            ShoppingList = new HashSet<ShoppingList>();
        }

        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
        public string Description { get; set; }
        public int Locality { get; set; }

        public virtual Locality LocalityNavigation { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<ShoppingList> ShoppingList { get; set; }
    }
}
