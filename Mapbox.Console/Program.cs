using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MapboxClient(new Credentials("something here"));
            try
            {
                var result = client.GeocodingApi.SearchGeocoding("coventry");
                System.Console.WriteLine(result.Result.ToString());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
