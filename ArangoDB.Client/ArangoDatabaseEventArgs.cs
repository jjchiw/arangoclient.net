using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB.Client
{
    public class ArangoDatabaseEventArgs : EventArgs
    {
        public object Item { get; set; }
        public Type Type { get; set; }
    }
}
