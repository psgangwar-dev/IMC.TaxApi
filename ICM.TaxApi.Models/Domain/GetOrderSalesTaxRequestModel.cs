using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetOrderSalesTaxRequestModel
    {
        /// <summary>
        /// Two-letter ISO country code of the country where the order shipped to.
        /// </summary>
        [Required]
        public string To_country { get; set; }

        /// <summary>
        /// Total amount of shipping for the order.
        /// </summary>
        [Required]
        public double Shipping { get; set; }

        /// <summary>
        /// Two-letter ISO country code of the country where the order shipped from.
        /// </summary>
        public string From_country { get; set; }

        /// <summary>
        /// Postal code where the order shipped from (5-Digit ZIP or ZIP+4).
        /// </summary>
        public string From_zip { get; set; }

        /// <summary>
        /// Two-letter ISO state code where the order shipped from.
        /// </summary>
        public string From_state { get; set; }

        /// <summary>
        /// City where the order shipped from.
        /// </summary>
        public string From_city { get; set; }

        /// <summary>
        /// Street address where the order shipped from.
        /// </summary>
        public string From_street { get; set; }

        /// <summary>
        /// Postal code where the order shipped to (5-Digit ZIP or ZIP+4).
        /// </summary>
        public string To_zip { get; set; }

        /// <summary>
        /// Two-letter ISO state code where the order shipped to.
        /// </summary>
        public string To_state { get; set; }

        /// <summary>
        /// City where the order shipped to.
        /// </summary>
        public string To_city { get; set; }

        /// <summary>
        /// Street address where the order shipped to. 
        /// </summary>
        public string To_street { get; set; }

        /// <summary>
        /// Total amount of the order, excluding shipping.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Nexus Addresses
        /// </summary>
        public List<NexusAddress> Nexus_addresses { get; set; }

        /// <summary>
        /// Order Line items
        /// </summary>
        public List<OrderLineItem> Line_items { get; set; }
    }
}
