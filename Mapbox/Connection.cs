using Mapbox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox
{
    public class Connection : IConnection
    {
        private Uri _baseAddress;
        private Credentials _credentials;
        private HttpClient _httpClient;

        public Connection(Uri baseAddress, Credentials credentials)
        {
            _credentials = credentials;
            _baseAddress = baseAddress;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        public Credentials Credentials
        {
            get
            {
                return _credentials;
            }
        }

        public Uri BaseAddress
        {
            get
            {
                return _baseAddress;
            }
        }

        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = BaseAddress,
            };

            return await RunRequest<T>(request);
        }

        async Task<T> RunRequest<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            HandleErrors(response);
            return response.Content;
        }

        static readonly Dictionary<HttpStatusCode, Exception> _httpExceptionMap =
            new Dictionary<HttpStatusCode, Exception>
            {
                { HttpStatusCode.Unauthorized, new Exception("Unauthorized") },
                { HttpStatusCode.Forbidden, new Exception("Forbidden") },
                { HttpStatusCode.NotFound, new Exception("NotFound") }
            };

        static void HandleErrors(HttpResponseMessage response)
        {
            Exception ex;
            if (_httpExceptionMap.TryGetValue(response.StatusCode, out ex))
            {
                throw ex;
            }
        }
    }
}