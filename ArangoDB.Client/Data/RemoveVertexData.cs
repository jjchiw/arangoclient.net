namespace ArangoDB.Client.Data
{
    [CollectionProperty(Naming = NamingConvention.ToCamelCase)]
    public class DropGraphCollectionData
    {
        public bool DropCollection { get; set; }

        public string Name { get; set; }
    }
}
