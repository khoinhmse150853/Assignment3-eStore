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
        public void AddNew(string cs, TblProduct product)
        {
            ProductDAO.Instance.AddNew(cs, product);
        }

        public TblProduct GetProductsById(string cs, int Id)
        {
            return ProductDAO.Instance.GetProductByID(cs, Id);
        }

        public IEnumerable<TblProduct> GetProductsList(string cs)
        {
            return ProductDAO.Instance.GetProductsList(cs);
        }

        public void Remove(string cs, int productId)
        {
            ProductDAO.Instance.Remove(cs, productId);
        }

        public IEnumerable<TblProduct> SearchProducts(string cs, string name, int from, int to)
        {
            return ProductDAO.Instance.SearchProducts(cs, name, from, to);
        }

        public void Update(string cs, TblProduct product)
        {
            ProductDAO.Instance.Update(cs, product);
        }
    }
}
