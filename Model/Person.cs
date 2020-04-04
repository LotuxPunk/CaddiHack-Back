using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Person
    {
        public Person()
        {
            Favorite = new HashSet<Favorite>();
            ShoppingListDelivererNavigation = new HashSet<ShoppingList>();
            ShoppingListOwnerNavigation = new HashSet<ShoppingList>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public int Locality { get; set; }

        public virtual Locality LocalityNavigation { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
        public virtual ICollection<ShoppingList> ShoppingListDelivererNavigation { get; set; }
        public virtual ICollection<ShoppingList> ShoppingListOwnerNavigation { get; set; }
    }
}
