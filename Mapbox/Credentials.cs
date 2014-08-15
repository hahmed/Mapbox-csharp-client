using Mapbox.Helpers;

namespace Mapbox
{
    public class Credentials
    {
        public Credentials(string token)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, "token");
            AccessToken = token;
        }

        public string AccessToken
        {
            get;
            private set;
        }
    }
}
