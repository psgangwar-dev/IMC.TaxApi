using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetTaxRateResponse : ErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public TaxRate Rate { get; set; }
    }
}
