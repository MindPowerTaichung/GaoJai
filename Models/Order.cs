//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MPERP2015.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int Customer_Id { get; set; }
        public byte[] Timestamp { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
