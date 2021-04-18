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
        #region Ctro and Class Level variables

        private readonly ILogger<TaxRepository> _logger;
        private readonly ITaxJarApiClient _apiClient;
        public TaxRepository(ITaxJarApiClient apiClient, ILogger<TaxRepository> logger)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This Method inetgrates with Tax Jar api Http Client(GetAsync) to fetch the sales tax rates for a given location.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>GetTaxRateResponse</returns>
        public async Task<GetTaxRateResponse> GetTaxRateResponse(string uri)
        {
            var response = await _apiClient.GetAsync<GetTaxRateResponse>(uri);
            return response;
        }

        /// <summary>
        /// This Method inetgrates with Tax Jar api Http Client(GetAsync) to fetch the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns>GetOrderSalesTaxResponse</returns>
        public async Task<GetOrderSalesTaxResponse> GetOrderSalesTax(GetOrderSalesTaxRequest apiRequest)
        {
            var response = await _apiClient.PostAsync<GetOrderSalesTaxResponse>(apiRequest.Uri, apiRequest);
            return response;
        }

        #endregion
    }
}
