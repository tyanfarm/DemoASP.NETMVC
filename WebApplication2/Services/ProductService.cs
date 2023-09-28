using WebApplication2.Areas.ProductManage.Models;

namespace WebApplication2.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[] {
                new ProductModel() {Id = 1, Name = "Iphone X", Price = 60},
                new ProductModel() {Id = 2, Name = "Iphone 11", Price = 70},
                new ProductModel() {Id = 3, Name = "Iphone 11 Pro", Price = 75},
                new ProductModel() {Id = 4, Name = "Iphone 12", Price = 80},
                new ProductModel() {Id = 5, Name = "Iphone 13 Pro Max", Price = 90}
            });
        }
    }
}
