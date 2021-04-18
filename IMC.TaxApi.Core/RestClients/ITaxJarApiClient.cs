using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMC.TaxApi.Core.RestClients
{
    public interface ITaxJarApiClient
    {
        Task<T> GetAsync<T>(string uri);

        Task<T> PostAsync<T>(string uri, object body = null);
    }
}
