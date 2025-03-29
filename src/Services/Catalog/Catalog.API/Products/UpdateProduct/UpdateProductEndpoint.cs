namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(string Name, string Description, List<string> Categories, string ImageFile, decimal Price);

public record UpdateProductResponse(bool Success);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductRequest request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateProductCommand(id, request.Name, request.Categories, request.Description, request.ImageFile, request.Price));

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Update Product")
        .WithSummary("Update Product");
    }
}
