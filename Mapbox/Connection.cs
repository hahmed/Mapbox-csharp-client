using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox
{
    public class Connection : IConnection
    {
        private Uri _baseAddress;
        private Credentials _credentials;
        public Connection(Uri baseAddress, Credentials credentials)
        {
            _credentials = credentials;
            _baseAddress = baseAddress;
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
    }
}