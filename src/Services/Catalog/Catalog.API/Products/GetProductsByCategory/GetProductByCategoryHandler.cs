
namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryHandler(
    IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
 {
    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductByCategoryHandler> _logger = logger;

    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Products by category request processing");

        var products = await _session.Query<Product>()
            .Where(t => t.Categories.Contains(query.Category))
            .ToListAsync();

        return new GetProductsByCategoryResult(products);
    }
}
