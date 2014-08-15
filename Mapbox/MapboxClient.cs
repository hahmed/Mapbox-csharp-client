using Mapbox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox
{
    public class MapboxClient
    {
        public static readonly Uri MapboxApiUrl = new Uri("https://api.tiles.mapbox.com/v4/");

        public MapboxClient(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");
            Connection = new Connection(MapboxApiUrl, credentials);
            GeocodingApi = new GeocodingClient(Connection);
        }

        public IConnection Connection { get; private set; }

        /// <summary>
        /// Geocoding api
        /// </summary>
        public GeocodingClient GeocodingApi { get; private set; }
    }
}
