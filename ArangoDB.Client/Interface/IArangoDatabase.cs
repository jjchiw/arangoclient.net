﻿using ArangoDB.Client.ChangeTracking;
using ArangoDB.Client.Data;
using ArangoDB.Client.Http;
using ArangoDB.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB.Client
{
    public interface IArangoDatabase : IDisposable
    {
        IHttpConnection Connection { get; set; }

        DocumentTracker ChangeTracker { get; set; }

        DatabaseSharedSetting SharedSetting { get; set; }

        DatabaseSetting Setting { get; set; }

        ICursor<T> CreateStatement<T>(string query, IList<QueryParameter> bindVars = null, bool? count = null,
            int? batchSize = 0, TimeSpan? ttl = null, QueryOption options = null);

        AqlQueryable<T> Query<T>();

        /// <summary>
        /// Get Document JsonObject and Identifiers
        /// </summary>
        /// <param name="id">id of document</param>
        /// <returns>A DocumentContainer</returns>
        DocumentContainer FindDocumentInfo(string id);

        /// <summary>
        /// Get Document JsonObject and Identifiers
        /// </summary>
        /// <param name="id">document object</param>
        /// <returns>A DocumentContainer</returns>
        DocumentContainer FindDocumentInfo(object document);

        /// <summary>
        /// Creates a new document in the collection for specific type
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> InsertAsync<T>(object document, bool? createCollection = null, bool? waitForSync = null);

        /// <summary>
        /// Creates a new document in the collection for specific type
        /// </summary>
        /// <param name="document">Representation of the document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        DocumentIdentifierResult Insert<T>(object document, bool? createCollection = null, bool? waitForSync = null);

        /// <summary>
        /// Creates a new edge document in the collection
        /// </summary>
        /// <param name="from">The document handle or key of the start point</param>
        /// <param name="to"> The document handle or key of the end point</param>
        /// <param name="edgeDocument">Representation of the edge document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        DocumentIdentifierResult InsertEdge<T>(string from, string to, object edgeDocument, bool? createCollection = null, bool? waitForSync = null);

        /// <summary>
        /// Creates a new edge document in the collection
        /// </summary>
        /// <param name="from">The document handle or key of the start point</param>
        /// <param name="to"> The document handle or key of the end point</param>
        /// <param name="edgeDocument">Representation of the edge document</param>
        /// <param name="createCollection">If true, then the collection is created if it does not yet exist</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> InsertEdgeAsync<T>(string from, string to, object edgeDocument, bool? createCollection = null, bool? waitForSync = null);

        /// <summary>
        /// Completely updates the document
        /// </summary>
        /// <param name="document">Representation of the new document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        DocumentIdentifierResult Replace<T>(object document, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Completely updates the document
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <param name="document">Representation of the new document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> ReplaceAsync<T>(object document, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Completely updates the document with no change tracking
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <param name="document">Representation of the new document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        DocumentIdentifierResult ReplaceById<T>(string id, object document, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Completely updates the document with no change tracking
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <param name="document">Representation of the new document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> ReplaceByIdAsync<T>(string id, object document, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        ///<summary>
        ///Partially updates the document without change tracking
        ///</summary>
        ///<param name="id">The document handle or key of document</param>
        ///<param name="document">Representation of the patch document</param>
        ///<param name="keepNull">For remove any attributes from the existing document that are contained in the patch document with an attribute value of null</param>
        ///<param name="mergeObjects">Controls whether objects (not arrays) will be merged if present in both the existing and the patch document</param>
        ///<param name="rev">Conditionally replace a document based on revision id</param>
        ///<param name="policy">To control the update behavior in case there is a revision mismatch</param>
        ///<param name="waitForSync">Wait until document has been synced to disk</param>
        ///<returns>Document identifiers</returns>
        DocumentIdentifierResult UpdateById<T>(string id, object document, bool? keepNull = null, bool? mergeObjects = null, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        ///<summary>
        ///Partially updates the document without change tracking
        ///</summary>
        ///<param name="id">The document handle or key of document</param>
        ///<param name="document">Representation of the patch document</param>
        ///<param name="keepNull">For remove any attributes from the existing document that are contained in the patch document with an attribute value of null</param>
        ///<param name="mergeObjects">Controls whether objects (not arrays) will be merged if present in both the existing and the patch document</param>
        ///<param name="rev">Conditionally replace a document based on revision id</param>
        ///<param name="policy">To control the update behavior in case there is a revision mismatch</param>
        ///<param name="waitForSync">Wait until document has been synced to disk</param>
        ///<returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> UpdateByIdAsync<T>(string id, object document, bool? keepNull = null, bool? mergeObjects = null, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        ///<summary>
        ///Partially updates the document
        ///</summary>
        ///<param name="document">Representation of the patch document</param>
        ///<param name="keepNull">For remove any attributes from the existing document that are contained in the patch document with an attribute value of null</param>
        ///<param name="mergeObjects">Controls whether objects (not arrays) will be merged if present in both the existing and the patch document</param>
        ///<param name="policy">To control the update behavior in case there is a revision mismatch</param>
        ///<param name="waitForSync">Wait until document has been synced to disk</param>
        ///<returns>Document identifiers</returns>
        DocumentIdentifierResult Update<T>(object document, bool? keepNull = null, bool? mergeObjects = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        ///<summary>
        ///Partially updates the document
        ///</summary>
        ///<param name="document">Representation of the patch document</param>
        ///<param name="keepNull">For remove any attributes from the existing document that are contained in the patch document with an attribute value of null</param>
        ///<param name="mergeObjects">Controls whether objects (not arrays) will be merged if present in both the existing and the patch document</param>
        ///<param name="policy">To control the update behavior in case there is a revision mismatch</param>
        ///<param name="waitForSync">Wait until document has been synced to disk</param>
        ///<returns>Document identifiers</returns>
        Task<DocumentIdentifierResult> UpdateAsync<T>(object document, bool? keepNull = null, bool? mergeObjects = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Deletes the document without change tracking
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns></returns>
        DocumentIdentifierResult RemoveById<T>(string id, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Deletes the document without change tracking
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <param name="rev">Conditionally replace a document based on revision id</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns></returns>
        Task<DocumentIdentifierResult> RemoveByIdAsync<T>(string id, string rev = null, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Deletes the document
        /// </summary>
        /// <param name="document">document reference</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns></returns>
        DocumentIdentifierResult Remove<T>(object document, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Deletes the document
        /// </summary>
        /// <param name="document">document reference</param>
        /// <param name="policy">To control the update behavior in case there is a revision mismatch</param>
        /// <param name="waitForSync">Wait until document has been synced to disk</param>
        /// <returns></returns>
        Task<DocumentIdentifierResult> RemoveAsync<T>(object document, ReplacePolicy? policy = null, bool? waitForSync = null);

        /// <summary>
        /// Reads a single document
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <returns>A Document</returns>
        T Document<T>(string id);

        /// <summary>
        /// Reads a single document
        /// </summary>
        /// <param name="id">The document handle or key of document</param>
        /// <returns>A Document</returns>
        Task<T> DocumentAsync<T>(string id);

        /// <summary>
        /// Read in or outbound edges
        /// </summary>
        /// <param name="vertexId">The document handle of the start vertex</param>
        /// <param name="direction">Selects in or out direction for edges. If not set, any edges are returned</param>
        /// <returns>Returns a list of edges starting or ending in the vertex identified by vertex document handle</returns>
        List<T> Edges<T>(string vertexId, EdgeDirection? direction = null);

        /// <summary>
        /// Read in or outbound edges
        /// </summary>
        /// <param name="vertexId">The document handle of the start vertex</param>
        /// <param name="direction">Selects in or out direction for edges. If not set, any edges are returned</param>
        /// <returns>Returns a list of edges starting or ending in the vertex identified by vertex document handle</returns>
        Task<List<T>> EdgesAsync<T>(string vertexId, EdgeDirection? direction = null);

        /// <summary>
        /// Returns all documents of a collections
        /// </summary>
        /// <param name="skip">The number of documents to skip in the query</param>
        /// <param name="limit">The maximal amount of documents to return. The skip is applied before the limit restriction</param>
        /// <param name="batchSize">Limits the number of results to be transferred in one batch</param>
        /// <returns>Returns a cursor</returns>
        ICursor<T> All<T>(int? skip = null, int? limit = null, int? batchSize = null);

        /// <summary>
        /// Finds all documents within a given range
        /// </summary>
        /// <param name="attribute">The attribute to check</param>
        /// <param name="left">The lower bound</param>
        /// <param name="right">The upper bound</param>
        /// <param name="closed">If true, use interval including left and right, otherwise exclude right, but include left</param>
        /// <param name="skip">The number of documents to skip in the query</param>
        /// <param name="limit">The maximal amount of documents to return. The skip is applied before the limit restriction</param>
        /// <param name="batchSize">Limits the number of results to be transferred in one batch</param>
        /// <returns>Returns a cursor</returns>
        ICursor<T> Range<T>(Expression<Func<T, object>> attribute, object left, object right, bool? closed = false,
            int? skip = null, int? limit = null, int? batchSize = null);

        /// <summary>
        /// Finds all documents matching a given example
        /// </summary>
        /// <param name="example">The example document</param>
        /// <param name="skip">The number of documents to skip in the query</param>
        /// <param name="limit">The maximal amount of documents to return. The skip is applied before the limit restriction</param>
        /// <param name="batchSize">Limits the number of results to be transferred in one batch</param>
        /// <returns>Returns a cursor</returns>
        ICursor<T> ByExample<T>(object example, int? skip = null, int? limit = null, int? batchSize = null);

        /// <summary>
        /// Returns the first document matching a given example
        /// </summary>
        /// <param name="example">The example document</param>
        /// <returns>A Document</returns>
        T FirstExample<T>(object example);

        /// <summary>
        /// Returns the first document matching a given example
        /// </summary>
        /// <param name="example">The example document</param>
        /// <returns>A Document</returns>
        Task<T> FirstExampleAsync<T>(object example);

        /// <summary>
        /// Returns a random document
        /// </summary>
        /// <returns>A Document</returns>
        T Any<T>();

        /// <summary>
        /// Returns a random document
        /// </summary>
        /// <returns>A Document</returns>
        Task<T> AnyAsync<T>();

        /// <summary>
        /// Retrieves information about the current database
        /// </summary>
        /// <returns>DatabaseInformation</returns>
        DatabaseInformation CurrentDatabaseInformation();

        /// <summary>
        /// Retrieves information about the current database
        /// </summary>
        /// <returns>DatabaseInformation</returns>
        Task<DatabaseInformation> CurrentDatabaseInformationAsync();

        /// <summary>
        /// List of accessible databases
        /// </summary>
        /// <returns>List of database names</returns>
        List<string> ListAccessibleDatabases();

        /// <summary>
        /// List of accessible databases
        /// </summary>
        /// <returns>List of database names</returns>
        Task<List<string>> ListAccessibleDatabasesAsync();

        /// <summary>
        /// List of databases
        /// </summary>
        /// <returns>List of database names</returns>
        List<string> ListDatabases();

        /// <summary>
        /// List of databases
        /// </summary>
        /// <returns>List of database names</returns>
        Task<List<string>> ListDatabasesAsync();

        /// <summary>
        /// Creates a database
        /// </summary>
        /// <param name="name">Name of the database</param>
        /// <param name="users">list of database user</param>
        /// <returns></returns>
        void CreateDatabase(string name, List<DatabaseUser> users = null);

        /// <summary>
        /// Creates a database
        /// </summary>
        /// <param name="name">Name of the database</param>
        /// <param name="users">list of database user</param>
        /// <returns></returns>
        Task CreateDatabaseAsync(string name, List<DatabaseUser> users = null);

        /// <summary>
        /// Creates a collection
        /// </summary>
        /// <param name="name">Name of the collection</param>
        /// <param name="waitForSync">If true then the data is synchronised to disk before returning from a document create, update, replace or removal operation</param>
        /// <param name="doCompact">Whether or not the collection will be compacted</param>
        /// <param name="journalSize"> The maximal size of a journal or datafile in bytes. The value must be at least 1048576 (1 MB).</param>
        /// <param name="isSystem"> If true, create a system collection. In this case collection-name should start with an underscore</param>
        /// <param name="isVolatile">If true then the collection data is kept in-memory only and not made persistent</param>
        /// <param name="type"> The type of the collection to create</param>
        /// <param name="numberOfShards">In a cluster, this value determines the number of shards to create for the collection</param>
        /// <param name="shardKeys">In a cluster, this attribute determines which document attributes are used to determine the target shard for documents</param>
        /// <returns>CreateCollectionResult</returns>
        CreateCollectionResult CreateCollection(string name, bool? waitForSync = null, bool? doCompact = null, decimal? journalSize = null,
            bool? isSystem = null, bool? isVolatile = null, CollectionType? type = null, int? numberOfShards = null, string shardKeys = null);

        /// <summary>
        /// Creates a collection
        /// </summary>
        /// <param name="name">Name of the collection</param>
        /// <param name="waitForSync">If true then the data is synchronised to disk before returning from a document create, update, replace or removal operation</param>
        /// <param name="doCompact">Whether or not the collection will be compacted</param>
        /// <param name="journalSize"> The maximal size of a journal or datafile in bytes. The value must be at least 1048576 (1 MB).</param>
        /// <param name="isSystem"> If true, create a system collection. In this case collection-name should start with an underscore</param>
        /// <param name="isVolatile">If true then the collection data is kept in-memory only and not made persistent</param>
        /// <param name="type"> The type of the collection to create</param>
        /// <param name="numberOfShards">In a cluster, this value determines the number of shards to create for the collection</param>
        /// <param name="shardKeys">In a cluster, this attribute determines which document attributes are used to determine the target shard for documents</param>
        /// <returns>CreateCollectionResult</returns>
        Task<CreateCollectionResult> CreateCollectionAsync(string name, bool? waitForSync = null, bool? doCompact = null, decimal? journalSize = null,
            bool? isSystem = null, bool? isVolatile = null, CollectionType? type = null, int? numberOfShards = null, string shardKeys = null);

        /// <summary>
        /// List of collections name
        /// </summary>
        /// <returns>List of collections names</returns>
        List<CollectionData> ListCollections();

        /// <summary>
        /// List of collections name
        /// </summary>
        /// <returns>List of collections names</returns>
        Task<List<CollectionData>> ListCollectionsAsync();

        /// <param name="name">Name of the database</param>
        /// <param name="users">list of database user</param>
        /// <returns></returns>
        void DeleteDatabase(string name);
        
        /// <param name="name">Name of the database</param>
        /// <param name="users">list of database user</param>
        /// <returns></returns>
        Task DeleteDatabaseAsync(string name);

        /// <summary>
        /// Creates a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <param name="edgeDefinitions">If true then the data is synchronised to disk before returning from a document create, update, replace or removal operation</param>
        /// <param name="orphanCollection">Whether or not the collection will be compacted</param>
        /// <returns>CreateGraphResult</returns>
        CreateGraphResult CreateGraph(string name, List<EdgeDefinitionData> edgeDefinitions, List<string> orphanCollections = null);

        /// <summary>
        /// Creates a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <param name="edgeDefinitions">If true then the data is synchronised to disk before returning from a document create, update, replace or removal operation</param>
        /// <param name="orphanCollection">Whether or not the collection will be compacted</param>
        /// <returns>CreateGraphResult</returns>
        Task<CreateGraphResult> CreateGraphAsync(string name, List<EdgeDefinitionData> edgeDefinitions, List<string> orphanCollections = null);

        /// <summary>
        /// Deletes a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <param name="dropCollections">Drop collections of this graph as well. Collections will only be dropped if they are not used in other graphs.</param>
        /// <returns></returns>
        void DeleteGraph(string name, bool dropCollections = false);

        /// <summary>
        /// Deletes a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <param name="dropCollections">Drop collections of this graph as well. Collections will only be dropped if they are not used in other graphs.</param>
        /// <returns>Task</returns>
        Task DeleteGraphAsync(string name, bool dropCollections = false);

        /// <summary>
        /// Get a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <returns>GraphIdentifierResult</returns>
        GraphIdentifierResult GetGraph(string name);

        /// <summary>
        /// Deletes a graph
        /// </summary>
        /// <param name="name">Name of the graph</param>
        /// <returns>GraphIdentifierResult</returns>
        Task<GraphIdentifierResult> GetGraphAsync(string name);
        
        event EventHandler<ArangoDatabaseEventArgs> BeforeItemRemoved;
        event EventHandler<ArangoDatabaseEventArgs> BeforeItemInsertd;
        event EventHandler<ArangoDatabaseEventArgs> BeforeItemUpdated;
        event EventHandler<ArangoDatabaseEventArgs> BeforeItemReplaced;
    }
}
