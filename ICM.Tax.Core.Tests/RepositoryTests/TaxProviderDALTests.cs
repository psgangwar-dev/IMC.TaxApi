using ICM.TaxApi.Models.Domain;
using ICM.TaxApi.Models.Entities;
using IMC.TaxApi.Core.Repository;
using IMC.TaxApi.Core.RestClients;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ICM.Tax.Core.Tests
{
    public class TaxProviderDALTests
    {
        private readonly Mock<ILogger<TaxRepository>> _logger;
        private readonly Mock<ITaxJarApiClient> _apiClient;
        private ITaxRepository _taxRepository;

        public TaxProviderDALTests()
        {
            _logger = new Mock<ILogger<TaxRepository>>();
            _apiClient = new Mock<ITaxJarApiClient>();
            _taxRepository = new TaxRepository(_apiClient.Object, _logger.Object);
        }

        [Fact]
        public async void GetTaxRate_Expect_Sucsess()
        {
            // Setup
            var mockResponse = new GetTaxRateResponse()
            {
                Rate = new TaxRate
                {
                    City = "Saint Pete",
                    Zip = "33701",
                    State_rate = "10.99"
                }
            };

            _apiClient.Setup(x => x.GetAsync<GetTaxRateResponse>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act
            var actualResponse = await _taxRepository.GetTaxRateResponse("https://sample.com") as GetTaxRateResponse;

            // Assert
            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(actualResponse.Rate);
            Assert.Equal("Saint Pete", actualResponse.Rate.City);
            Assert.Equal("33701", actualResponse.Rate.Zip);
            Assert.Equal("10.99", actualResponse.Rate.State_rate);
        }

        [Fact]
        public async void GetTaxForOrder_Expect_Sucsess()
        {
            // Setup
            var mockResponse = new GetOrderSalesTaxResponse()
            {
                SalesTax = new SalesTax
                {
                    amount_to_collect = 12.90,
                    Order_total_amount = 124.88,
                    jurisdictions = new Jurisdictions
                    {
                        city = "Saint Pete",
                        country = "US"
                    }
                }
            };

            var fakeRequest = new GetOrderSalesTaxRequest { };
            _apiClient.Setup(x => x.PostAsync<GetOrderSalesTaxResponse>(It.IsAny<string>(), It.IsAny<object>()))
                 .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act
            var actualResponse = await _taxRepository.GetOrderSalesTax(fakeRequest) as GetOrderSalesTaxResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(actualResponse.SalesTax);
            Assert.Equal(12.90, actualResponse.SalesTax.amount_to_collect);
            Assert.Equal(124.88, actualResponse.SalesTax.Order_total_amount);
            Assert.NotNull(actualResponse.SalesTax.jurisdictions);
            Assert.Equal("Saint Pete", actualResponse.SalesTax.jurisdictions.city);
        }
    }
}
