using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB.Client.Data
{
    [CollectionProperty(Naming = NamingConvention.ToCamelCase)]
    public class CollectionData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }
    }
}
