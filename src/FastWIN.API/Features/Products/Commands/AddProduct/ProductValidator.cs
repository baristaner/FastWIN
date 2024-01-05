using fastwin.Entities;
using FluentValidation;

namespace fastwin.Features.Products.Commands.AddProduct
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .Length(1, 15)
                .WithMessage("Product Name must be between 1 and 15 chars");

            RuleFor(product => product.Category)
                .NotNull().WithMessage("Product Category can't be null.")
                .IsInEnum() // i don't know why i can't use IsEnumName()
                .WithMessage("Invalid Category, please provide a valid category name.");
        }
    }
}
