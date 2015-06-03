using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MPERP2015.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TimestampString { get; set; }
    }

    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int Customer_Id { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public string TimestampString { get; set; }
    }

    public class OrderDetailViewModel
    {
        public int Order_Id { get; set; }
        public int Quantity { get; set; }
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int Category_Id { get; set; }

        public string TimestampString { get; set; }
    }
}

