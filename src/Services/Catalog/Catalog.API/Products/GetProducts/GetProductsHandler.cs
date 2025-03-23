
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsHandler(IDocumentSession session) : 
    IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly IDocumentSession _session = session;

    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>().ToListAsync();

        return new GetProductsResult(products);
    }
}
