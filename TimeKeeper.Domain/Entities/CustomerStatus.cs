using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class CustomerStatus: BaseStatus
    {
        public CustomerStatus() 
        {
            Customers = new List<Customer>();
        }
        public virtual IList<Customer> Customers { get; set; }
    }
}
