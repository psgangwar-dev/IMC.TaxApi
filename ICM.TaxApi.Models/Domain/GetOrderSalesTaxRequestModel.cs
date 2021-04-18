using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetOrderSalesTaxRequestModel
    {
        public string From_country { get; set; }
        public string From_zip { get; set; }
        public string From_state { get; set; }
        public string From_city { get; set; }
        public string From_street { get; set; }

        [Required]
        public string To_country { get; set; }
        public string To_zip { get; set; }
        public string To_state { get; set; }
        public string To_city { get; set; }
        public string To_street { get; set; }
        public int Amount { get; set; }

        [Required]
        public double Shipping { get; set; }
        public List<NexusAddress> Nexus_addresses { get; set; }
        public List<OrderLineItem> Line_items { get; set; }
    }
}
