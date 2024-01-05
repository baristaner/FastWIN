using fastwin.Entities;
using fastwin.Helper.Mappers;
using fastwin.Interfaces;
using FluentValidation;
using MediatR;

namespace fastwin.Features.Products.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IValidator<Product> _productValidator;

        public AddProductCommandHandler(IRepository<Product> productRepository, IValidator<Product> productValidator)
        {
            _productRepository = productRepository;
            _productValidator = productValidator;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map ProductRequest to Product, i've created this productmapper method to map my product values.
                var product = ProductMapper.MapProduct(request.ProductDto);

                // Validation
                await _productValidator.ValidateAndThrowAsync(product);

                // Business logic
                await _productRepository.AddAsync(product);


                return product;
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }
        }
    }
}
