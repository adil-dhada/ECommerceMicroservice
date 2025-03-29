
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger) : 
    IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductsHandler> _logger = logger;

    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Product Query has start execution {query}", request);

        var products = await _session.Query<Product>().ToListAsync();

        return new GetProductsResult(products);
    }
}
