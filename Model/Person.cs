using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Person
    {
        public Person()
        {
            PersonRole = new HashSet<PersonRole>();
            ShoppingList = new HashSet<ShoppingList>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Locality { get; set; }

        public virtual Locality LocalityNavigation { get; set; }
        public virtual ICollection<PersonRole> PersonRole { get; set; }
        public virtual ICollection<ShoppingList> ShoppingList { get; set; }
    }
}
