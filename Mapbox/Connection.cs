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
                RequestUri = uri
            };

            return await RunRequest<T>(request);
        }

        /// <summary>
        /// Add access token to end of url, ensure we are requesting a .json file too
        /// </summary>
        /// <param name="url"></param>
        static Uri FixupRequestTypeForUrl(Uri url)
        {
            if (!url.ToString().EndsWith(".json"))
                return new Uri(url, new Uri(".json", UriKind.Relative));
            return url;
        }

        Uri AddAccessToken(Uri url)
        {
            var accessToken = "?access_token=" + Credentials.AccessToken;
            var builder = url.ToString() + accessToken;
            return new Uri(builder, UriKind.Relative);
        }

        async Task<T> RunRequest<T>(HttpRequestMessage request)
        {
            Uri endpoint = FixupRequestTypeForUrl(request.RequestUri);
            endpoint = AddAccessToken(endpoint);
            request.RequestUri = endpoint;
            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
            HandleErrors(response);
            return await response.Content.ReadAsAsync<T>();
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
                var content = response.Content.ReadAsStringAsync();
                Console.WriteLine(content.Result);
                throw ex;
            }
        }
    }
}