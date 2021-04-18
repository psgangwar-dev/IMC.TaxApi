using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Entities
{
    public class NexusAddress
    {
        /// <summary>
        /// Unique identifier of the given nexus address. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Two-letter ISO country code for the nexus address.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Postal code for the nexus address.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Two-letter ISO state code for the nexus address.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// City for the nexus address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Street address for the nexus address.
        /// </summary>
        public string Street { get; set; }
    }
}
