using Temp.Data;
using Temp.Objects;
using Temp.Objects.Models;
using Temp.Objects.Views;

namespace Temp.Services
{
    public class ProductService : AService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TView? Get<TView>(Int64 id) where TView : AView
        {
            return UnitOfWork.GetAs<Product, TView>(id);
        }
        /*
        public List<ProductView> GetProducts()
        {
            return UnitOfWork.Select<Product>().To<ProductView>().ToList();
        }*/
        public IQueryable<ProductView> GetProducts()
        {
            return UnitOfWork
                .Select<Product>()
                .To<ProductView>()
                .OrderByDescending(product => product.Id);
        }
        public void Create(ProductView view)
        {
            Product product = UnitOfWork.To<Product>(view);
            UnitOfWork.Insert(product);
            UnitOfWork.Commit();
        }
        public void Edit(ProductView view)
        {
            Product product = UnitOfWork.Get<Product>(view.Id)!;

            product.Name = view.Name;
            product.Description = view.Description;
            product.Price = view.Price;
            

            UnitOfWork.Update(product);
            UnitOfWork.Commit();
        }
        public void Delete(Int64 id)
        {
            UnitOfWork.Delete<Product>(id);
            UnitOfWork.Commit();
        }

    }
}
