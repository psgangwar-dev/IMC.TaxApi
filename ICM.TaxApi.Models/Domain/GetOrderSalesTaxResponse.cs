using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICM.TaxApi.Models.Domain
{
    public class GetOrderSalesTaxResponse : ErrorInfo
    {
        public SalesTax SalesTax { get; set; }
    }
}
