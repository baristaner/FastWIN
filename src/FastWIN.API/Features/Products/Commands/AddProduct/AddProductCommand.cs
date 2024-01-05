using fastwin.Entities;
using fastwin.Requests;
using MediatR;

public class AddProductCommand : IRequest<Product>
{
    public ProductRequest ProductDto { get; }

    public AddProductCommand(ProductRequest productDto)
    {
        ProductDto = productDto;
    }
}
