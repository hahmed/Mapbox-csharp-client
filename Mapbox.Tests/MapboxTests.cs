using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox.Tests
{
    public class MapboxTests
    {
        private string token = "pk.eyJ1Ijoic3VydmVudHJ4IiwiYSI6IkoyaFI1Y1UifQ.VNkFQprin8MMlWK90ecnmg";
        
        [Test]
        public void EnsureTokenIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Credentials(null));
        }

        [Test]
        public void EnsureCredentialsIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MapboxClient(null));
        }

        [Test]
        public void GetGeoding()
        {
            var credentials = new Credentials(token);
            var client = new MapboxClient(credentials);
            var result = client.GeocodingApi.SearchGeocoding("cv2");
            Console.WriteLine(result.Result);
        }
    }
}