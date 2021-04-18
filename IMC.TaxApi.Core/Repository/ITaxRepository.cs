using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMC.TaxApi.Core.Repository
{
    public interface ITaxRepository
    {
        Task<GetTaxRateResponse> GetTaxRateResponse(string uri);

        Task<GetOrderSalesTaxResponse> TaxForOrder(GetOrderSalesTaxRequest apiRequest);
    }
}
