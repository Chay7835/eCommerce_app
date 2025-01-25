using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class MyCartVM
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public int Discount { get; set; }
        public double DiscountedAmount { get; set; }
        public DateTime CartDate { get; set; }
        public string Picture { get; set; } = string.Empty;
    }
}