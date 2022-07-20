using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddNew(TblProduct product)
        {
            ProductDAO.Instance.AddNew(product);
        }

        public TblProduct GetProductsById(int Id)
        {
            return ProductDAO.Instance.GetProductByID(Id);
        }

        public IEnumerable<TblProduct> GetProductsList()
        {
            return ProductDAO.Instance.GetProductsList();
        }

        public void Remove(int productId)
        {
            ProductDAO.Instance.Remove(productId);
        }

        public IEnumerable<TblProduct> SearchProductsByProductName(string name)
        {
            return ProductDAO.Instance.SearchProductsByProductName(name);
        }

        public IEnumerable<TblProduct> SearchProductsByUnitPrice(string name, int from, int to)
        {
            return ProductDAO.Instance.SearchProductsByUnitPirce(name, from, to);
        }

        public void Update(TblProduct product)
        {
            ProductDAO.Instance.Update(product);
        }
    }
}
