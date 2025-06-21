using OurEstheticSolution.Models.Entities;

using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Interface
{
    public interface IProduct
    {
        IEnumerable<ProductViewModel>GetAllProducts();

        ProductViewModel GetById(Guid id);

        void InsertProduct(ProductViewModel model);

        void UpdateProduct(ProductViewModel model);

        void DeleteProduct(Guid id);






    }
}
