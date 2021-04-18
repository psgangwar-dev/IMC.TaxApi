using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetTaxRateRequest
    {
        /// <summary>
        /// Zip Code of the address which is 
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        [Required]
        public string Zip { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
