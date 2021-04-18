using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using IMC.TaxApi.Core.RestClients;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMC.TaxApi.Core.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly ILogger<TaxRepository> _logger;
        private readonly ITaxJarApiClient _apiClient;
        public TaxRepository(ITaxJarApiClient apiClient, ILogger<TaxRepository> logger)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<GetTaxRateResponse> GetTaxRateResponse(string uri)
        {
            var response = await _apiClient.GetAsync<GetTaxRateResponse>(uri);
            return response;
        }

        public async Task<GetOrderSalesTaxResponse> TaxForOrder(GetOrderSalesTaxRequest apiRequest)
        {
            var response = await _apiClient.PostAsync<GetOrderSalesTaxResponse>(apiRequest.Uri, apiRequest);
            return response;
        }
    }
}
