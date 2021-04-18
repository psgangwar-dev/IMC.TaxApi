using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class OrderLineItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string Product_tax_code { get; set; }
        public int Unit_price { get; set; }
        public int Discount { get; set; }
    }
}
