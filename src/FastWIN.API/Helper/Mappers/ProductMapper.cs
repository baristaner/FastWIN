using fastwin.Entities;
using fastwin.Requests;

namespace fastwin.Helper.Mappers
{
    public static class ProductMapper
    {
        public static Product MapProduct(ProductRequest productRequest)
        {
            return new Product
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Category = Enum.Parse<Category>(productRequest.Category),
                LastUsageDate = productRequest.LastUsageDate,
            };
        }
    }
}
