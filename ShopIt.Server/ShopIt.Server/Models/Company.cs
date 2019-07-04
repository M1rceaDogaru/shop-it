using System;
using System.Collections.Generic;

namespace ShopIt.Server.Models
{
    public partial class Company
    {
        public Company()
        {
            Advert = new HashSet<Advert>();
        }

        public Guid CompanyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Advert> Advert { get; set; }
    }
}
