using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetTaxRateRequest
    {
        /// <summary>
        /// Postal code for given location (5-Digit ZIP or ZIP+4).
        /// </summary>
        [Required]
        public string Zip { get; set; }

        /// <summary>
        /// City for given location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Street address for given location
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Two-letter ISO state code for given location.e.g. FL, AZ, NY
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Two-letter ISO country code for given location. e.g US, AU, IN 
        /// </summary>
        public string Country { get; set; }
    }
}
