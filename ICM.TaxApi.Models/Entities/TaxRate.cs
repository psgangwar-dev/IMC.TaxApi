using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class TaxRate
    {
        /// <summary>
        /// Postal code for given location
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Postal abbreviated state name for given location.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// State sales tax rate for given location.
        /// </summary>
        public string State_rate { get; set; }

        /// <summary>
        /// County name for given location.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// County sales tax rate for given location.
        /// </summary>
        public string County_rate { get; set; }

        /// <summary>
        /// City name for given location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// City sales tax rate for given location.
        /// </summary>
        public string City_rate { get; set; }

        /// <summary>
        /// Aggregate rate for all city and county sales tax districts effective at the location.
        /// </summary>
        public string Combined_district_rate { get; set; }

        /// <summary>
        /// Overall sales tax rate which includes state, county, city and district tax. 
        /// This rate should be used to determine how much sales tax to collect for an order.
        /// </summary>
        public string Combined_rate { get; set; }

        /// <summary>
        /// Freight taxability for given location.
        /// </summary>
        public bool Freight_taxable { get; set; }
    }
}
