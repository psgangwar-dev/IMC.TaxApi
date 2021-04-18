using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class SalesTax
    {
        public double Order_total_amount { get; set; }
        public double shipping { get; set; }
        public int taxable_amount { get; set; }
        public double amount_to_collect { get; set; }
        public double rate { get; set; }
        public bool has_nexus { get; set; }
        public bool freight_taxable { get; set; }
        public string tax_source { get; set; }
        public Jurisdictions jurisdictions { get; set; }
        public Breakdown breakdown { get; set; }
    }
}
