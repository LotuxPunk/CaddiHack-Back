using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Role
    {
        public Role()
        {
            PersonRole = new HashSet<PersonRole>();
        }

        public string Name { get; set; }

        public virtual ICollection<PersonRole> PersonRole { get; set; }
    }
}
