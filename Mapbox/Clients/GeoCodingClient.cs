using Mapbox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox
{
    public class GeocodingClient
    {
        private IConnection _connection;

        public GeocodingClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            _connection = connection;
        }

        /// <summary>
        /// http://api.tiles.mapbox.com/v4/geocode/{index}/{query}.json
        /// </summary>
        /// <param name="query"></param>
        public async Task<string> SearchGeocoding(string query)
        {
            var url = new Uri("geocode/mapbox.places-v1/" + query + ".json", UriKind.Relative);
            var result = await _connection.Get<string>(url, null);
            return result;
        }

        /// <summary>
        /// http://api.tiles.mapbox.com/v4/geocode/{index}/{lon},{lat}.json
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitute"></param>
        public async Task<string> ReverseGeocodingLookup(decimal longitude, decimal latitute)
        {
            var client = new HttpClient();
            client.BaseAddress = _connection.BaseAddress;
            var result = await client.GetAsync("geocode/mapbox.places-postcode-v1/" + longitude + "," + latitute + ".json?access_token=" + _connection.Credentials.AccessToken);
            result.EnsureSuccessStatusCode();
            var content = result.Content;
            return await content.ReadAsStringAsync();
        }
    }

    public enum GeocodingSources
    {
        FullStack,
        Countries,
        Provinces,
        Postcodes,
        Places,
        USAddresses
    }
}
