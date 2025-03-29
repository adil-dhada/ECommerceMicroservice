﻿namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler(
    IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductByIdHandler> _logger = logger;

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Product by id request with id: {id}:", query.Id);

        var product = await _session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
