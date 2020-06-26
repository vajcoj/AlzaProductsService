using FluentValidation;
using ProductsService.Models;

namespace ProductsService.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.ImgUri).NotEmpty();
            RuleFor(p => p.Price).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
