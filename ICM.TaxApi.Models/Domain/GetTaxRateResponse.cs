using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetTaxRateResponse : ErrorInfo
    {
        /// <summary>
        ///  Rate object with rates for a given location broken down by state, county, city, and district. 
        ///  For international requests, returns standard and reduced rates.
        /// </summary>
        public TaxRate Rate { get; set; }
    }
}
