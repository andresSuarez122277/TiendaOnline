namespace TiendaOnline.Web.Helpers
{
    using System;
    using TiendaOnline.Web.Models;
    using System.Threading.Tasks;

    public interface IConverterHelper
    {
        Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew);

        CategoryViewModel ToCategoryViewModel(Category category);

        Task<Product> ToProductAsync(ProductViewModel model, bool isNew);

        ProductViewModel ToProductViewModel(Product product);

    }

}
