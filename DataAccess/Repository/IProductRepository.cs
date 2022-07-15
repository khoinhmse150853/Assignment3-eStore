using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<TblProduct> GetProductsList(string cs);
        TblProduct GetProductsById(string cs, int Id);
        IEnumerable<TblProduct> SearchProducts(string cs, string name, int from, int to);
        void Update(string cs, TblProduct product);
        void AddNew(string cs, TblProduct product);
        void Remove(string cs, int productId);
    }
}
