using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.TaxApi.Core.Mappers
{
    public interface ITaxProviderMapper
    {
        string MapGetTaxRatesUri(GetTaxRateRequest request);

        GetOrderSalesTaxRequest MapGetOrderSalesTaxRequest(GetOrderSalesTaxRequestModel request);
    }
}
