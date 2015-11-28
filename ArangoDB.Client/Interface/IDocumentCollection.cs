using ArangoDB.Client.Data;
using System;
using System.Threading.Tasks;

namespace ArangoDB.Client
{
    public interface IDocumentCollection : IArangoCollection
    {
        /// <summary>
        /// Creates a new document in the collection
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<IDocumentIdentifierResult> InsertAsync(object document, bool? createCollection = null, bool? waitForSync = null, Action<BaseResult> baseResult = null, EventHandler<ArangoDatabaseEventArgs> evt = null);

        /// <summary>
        /// Creates a new document in the collection
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        IDocumentIdentifierResult Insert(object document, bool? createCollection = null, bool? waitForSync = null, Action<BaseResult> baseResult = null, EventHandler<ArangoDatabaseEventArgs> evt = null);
    }

    public interface IDocumentCollection<T> : IArangoCollection<T>
    {
        /// <summary>
        /// Creates a new document in the collection
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<IDocumentIdentifierResult> InsertAsync(object document, bool? createCollection = null, bool? waitForSync = null, Action<BaseResult> baseResult = null, EventHandler<ArangoDatabaseEventArgs> evt = null);

        /// <summary>
        /// Creates a new document in the collection
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        IDocumentIdentifierResult Insert(object document, bool? createCollection = null, bool? waitForSync = null, Action<BaseResult> baseResult = null, EventHandler<ArangoDatabaseEventArgs> evt = null);
    }
}
