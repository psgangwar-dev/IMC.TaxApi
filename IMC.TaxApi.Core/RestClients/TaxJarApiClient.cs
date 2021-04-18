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
        #region Ctro and Class Level variables

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

        #endregion

        /// <summary>
        /// GetAsync Method to Intergrate with the required Partner Endpoint based on the consumer key sent in the header
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                HttpResponseMessage response = null;
                // Create HTTP Client
                var httpclient = _httpFactory.CreateClient();

                // Exceute HTTP request message
                using (var request = GenerateHttpRequest(HttpMethod.Get, uri))
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

        /// <summary>
        /// POST Asnyc Method to Intergrate with the required Partner Endpoint based on the consumer key sent in the header
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string uri, object body)
        {
            try
            {
                HttpResponseMessage response = null;
                // Create HTTP Client
                var httpclient = _httpFactory.CreateClient();

                // Exceute HTTP request message
                using (var request = GenerateHttpRequest(HttpMethod.Post, uri, body))
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

        /// <summary>
        /// Prepares Http Request for a given a HTTP method and body
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="uri"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        private HttpRequestMessage GenerateHttpRequest(HttpMethod httpMethod, string uri, object spec = null)
        {
            // Verify is ConsumerKey is available in the Request Headers, 
            // If not, throw error
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("ConsumerKey", out var consumerKey))
                throw new InvalidOperationException("Invalid.ConsumerKey");

            // Fetch PartnerConfigurations (URL, SecretKey) to setup the http client
            var consumerPartnerConfig = _consumerKeyPartnerConfiguration.Value.PartnerConfigurations.Find(x => x.ConsumerKey.Equals(consumerKey, StringComparison.CurrentCultureIgnoreCase));

            // If PartnerConfigurations not found for a given consumer then return an error
            if (consumerPartnerConfig == null)
                throw new InvalidOperationException("NotFound.ParterConfig");

            // Set clinet URL
            var request = new HttpRequestMessage(httpMethod, new Uri($"{consumerPartnerConfig.PartnerBaseEndpoint}{uri}"));

            var JsonFormatter = new JsonMediaTypeFormatter() { SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() } };

            // For PUT and POST Methods append the required body if available
            if (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Post)
            {
                var data = JsonConvert.SerializeObject(spec, Formatting.None, JsonFormatter.SerializerSettings);
                request.Content = new StringContent(data, Encoding.UTF8, MediaType);
            }

            // Set bearer token from PartnerConfigurations
            request.Headers.Add("Authorization", "Bearer " + consumerPartnerConfig.AuthToken);

            return request;
        }
    }
}