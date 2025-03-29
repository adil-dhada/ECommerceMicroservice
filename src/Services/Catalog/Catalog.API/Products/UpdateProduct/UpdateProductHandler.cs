
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool Success);

public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IDocumentSession _session = session;
    private readonly ILogger<UpdateProductHandler> _logger = logger;

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update product command execution starts {commadn}", command);

        var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);
        if(product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Categories = command.Categories;
        product.ImageFile = command.ImageFile;

        _session.Update(product);
        await _session.SaveChangesAsync();

        return new UpdateProductResult(true);
    }
}
