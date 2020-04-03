using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Locality
    {
        public Locality()
        {
            Person = new HashSet<Person>();
            Shop = new HashSet<Shop>();
        }

        public int LocalityId { get; set; }
        public string Name { get; set; }
        public int ZipCode { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<Shop> Shop { get; set; }
    }
}
