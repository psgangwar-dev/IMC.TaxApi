using ICM.TaxApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;


namespace IMC.TaxApi.Core.RestClients
{
    public class TaxJarApiClient : ITaxJarApiClient
    {
        private readonly ILogger<TaxJarApiClient> _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IOptions<ConsumerKeyPartnerConfiguration> _consumerKeyPartnerConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string MediaType = "application/json";

        public JsonMediaTypeFormatter JsonFormatter { get; set; } = new JsonMediaTypeFormatter()
        {
            SerializerSettings =
             {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
             }
        };

        public TaxJarApiClient(IHttpClientFactory httpFactory, ILogger<TaxJarApiClient> logger
                               , IOptions<ConsumerKeyPartnerConfiguration> consumerKeyPartnerConfiguration
                               , IHttpContextAccessor httpContextAccessor)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _consumerKeyPartnerConfiguration = consumerKeyPartnerConfiguration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                HttpResponseMessage response = null;

                var httpclient = _httpFactory.CreateClient("TaxJarApi");

                using (var request = CreateJsonRequest(HttpMethod.Get, httpclient.BaseAddress, uri))
                {
                    response = await httpclient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        T value = await response.Content.ReadAsAsync<T>();
                        return value;
                    }
                }

                _logger.LogWarning($"ApiCall: StatusCode={response.StatusCode}");

                return default(T);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ApiCall: Exception={ex}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> PostAsync<T>(string uri, object body = null)
        {
            try
            {
                HttpResponseMessage response = null;

                var httpclient = _httpFactory.CreateClient("TaxJarApi");

                using (var request = CreateJsonRequest(HttpMethod.Post, httpclient.BaseAddress, uri, body))
                {
                    response = await httpclient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        T value = await response.Content.ReadAsAsync<T>();
                        return value;
                    }
                }

                _logger.LogWarning($"ApiCall: StatusCode={response.StatusCode}");

                return default(T);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ApiCall: Exception={ex}");
                throw new Exception(ex.Message);
            }
        }

        private HttpRequestMessage CreateJsonRequest(HttpMethod httpMethod, Uri BaseAddress, string uri, object spec = null)
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("ConsumerKey", out var consumerKey))
                throw new InvalidOperationException("Invalid.ConsumerKey");

            var consumerPartnerConfig = _consumerKeyPartnerConfiguration.Value.PartnerConfigurations.Find(x => x.ConsumerKey.Equals(consumerKey, StringComparison.CurrentCultureIgnoreCase));

            if (consumerPartnerConfig == null)
                throw new InvalidOperationException("NotFound.ParterConfig");


            var request = new HttpRequestMessage(httpMethod, new Uri(new Uri(uri = consumerPartnerConfig.PartnerBaseEndpoint), uri));

            if (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Post)
            {
                var data = JsonConvert.SerializeObject(spec, Formatting.None, JsonFormatter.SerializerSettings);
                request.Content = new StringContent(data, Encoding.UTF8, MediaType);
            }

            request.Headers.Add("Authorization", "Bearer " + consumerPartnerConfig.AuthToken);

            return request;
        }
    }
}