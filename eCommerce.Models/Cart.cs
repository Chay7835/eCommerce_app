using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Cart
    {
    /*
       public int CartId { get; set; }

       public DateTime CartDate { get; set; } = DateTime.Now;

       public int CutomerId { get; set; } // Foreign Key
         
       public virtual Customer customer { get; set; } // Navigation Property (One to Many)
                                                      // Between Customer & Cart
                                                      // Entity Relation is Achieved by this method */

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CartDate { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
