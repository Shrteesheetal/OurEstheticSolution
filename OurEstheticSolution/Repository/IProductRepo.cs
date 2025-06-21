using Microsoft.EntityFrameworkCore;
using OurEstheticSolution.Data;
using OurEstheticSolution.Interface;
using OurEstheticSolution.ViewModel;
using System.Data;
using Dapper;
using OurEstheticSolution.Models.Entities;




namespace OurEstheticSolution.Repository
{
   
    public class IProductRepo : IProduct
    {
        private readonly AppDBContext _context;

        public IProductRepo(AppDBContext context)
        {
            _context = context;
        }

        public void DeleteProduct(Guid id)
        {
            var product = _context.Products.Find(id); 
            if (product != null)
            {
                _context.Products.Remove(product);   // Remove the entity
                _context.SaveChanges();                // Commit changes
            }
            else
            {
                throw new Exception("Products not found.");
            }
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            return _context.Products
                .Select(prod => new ProductViewModel
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Description = prod.Description,
                    ExpiryDate = prod.ExpiryDate,
                    ManufacturedBy = prod.ManufacturedBy,
                    ServiceId = prod.ServiceId,
                    CreatedAt = prod.CreatedAt,
                    UpdatedAt = prod.UpdatedAt
                })
                .ToList();
        }




        public ProductViewModel GetById(Guid id)
        {
            var product = _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ExpiryDate = p.ExpiryDate,
                    ManufacturedBy = p.ManufacturedBy,
                    ServiceId = p.ServiceId
                })
                .FirstOrDefault();

            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            return product;
        }


        public void InsertProduct(ProductViewModel model)
        {
            // Map ProductViewModel to Product entity
            var productEntity = new Product
            {
                Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                ExpiryDate = model.ExpiryDate,
                ManufacturedBy = model.ManufacturedBy,
                ServiceId = model.ServiceId,
                CreatedAt = model.CreatedAt == default ? DateTime.UtcNow : model.CreatedAt,
                UpdatedAt = model.UpdatedAt == default ? DateTime.UtcNow : model.UpdatedAt
            };

            _context.Products.Add(productEntity); // Add the mapped entity to the DbSet
            _context.SaveChanges(); // Save changes to the database
        }


        public void UpdateProduct(ProductViewModel prod)
        {

            var  product = _context.Products.FirstOrDefault(e => e.Id == prod.Id);

            if (product != null)
            {
                // Map fields from ViewModel to Entity
                product.Name = prod.Name;
                product.Description = prod.Description;
                product.ExpiryDate = prod.ExpiryDate;
                product.ManufacturedBy = prod.ManufacturedBy;
                product.ServiceId = prod.ServiceId;
                _context.SaveChanges(); // Commit changes
            }
            else
            {
                throw new Exception("product not found.");
            }
        }


    }
}
