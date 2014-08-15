using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapbox
{
    public interface IConnection
    {
        Credentials Credentials { get; }

        Uri BaseAddress { get; }
    }
}
