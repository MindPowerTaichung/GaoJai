using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPERP2015.Reports.Models
{
    public class CustomerLabel
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }

        public List<CustomerLabel> GetSampleData()
        {
            List<CustomerLabel> customers = new List<CustomerLabel>();
            customers.Add(new CustomerLabel {  CustomerName="心力資訊有限公司", Address="408 台中市南屯區大墩十四街188號3F", Tel="04-23251415" });
            customers.Add(new CustomerLabel { CustomerName = "元力有限公司", Address = "408 台中市南屯區大墩十四街190號3F", Tel = "04-23251416" });
            customers.Add(new CustomerLabel { CustomerName = "佳仕徳工坊", Address = "408 台中市南屯區公益路二段33號", Tel = "04-23285512" });
            return customers;
        }
    }
}