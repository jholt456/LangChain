using LangChain.Providers;
using LangChain.Retrievers;
using LangChain.Sources;

namespace LangChain.Databases;

/// <summary>
/// 
/// </summary>
public static class VectorStoreRetrieverExtensions
{
    /// <summary>
    /// Return vector collection as retriever
    /// </summary>
    /// <param name="vectorCollection">vector store</param>
    /// <param name="embeddingModel"></param>
    /// <param name="searchType">search type</param>
    /// <param name="scoreThreshold">score threshold</param>
    /// <returns></returns>
    public static VectorStoreRetriever AsRetriever(
        this IVectorCollection vectorCollection,
        IEmbeddingModel embeddingModel,
        VectorSearchType searchType = VectorSearchType.Similarity,
        float? scoreThreshold = null)
    {
        return new VectorStoreRetriever(vectorCollection, embeddingModel, searchType, scoreThreshold);
    }

    /// <summary>
    /// Return vector store as retriever
    /// </summary>
    /// <param name="vectorCollection">vector store</param>
    /// <param name="embeddingModel"></param>
    /// <param name="query"></param>
    /// <param name="amount"></param>
    /// <param name="searchType">search type</param>
    /// <param name="scoreThreshold">score threshold</param>
    /// <returns></returns>
    public static async Task<IReadOnlyCollection<Document>> GetSimilarDocuments(
        this IVectorCollection vectorCollection,
        IEmbeddingModel embeddingModel,
        string query,
        int amount = 4,
        VectorSearchType searchType = VectorSearchType.Similarity,
        float? scoreThreshold = null)
    {
        var retriever = vectorCollection.AsRetriever(embeddingModel, searchType, scoreThreshold);
        retriever.K = amount;
        
        return await retriever.GetRelevantDocumentsAsync(query).ConfigureAwait(false);
    }
}