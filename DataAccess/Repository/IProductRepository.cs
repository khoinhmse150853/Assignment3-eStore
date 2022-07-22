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
        IEnumerable<TblProduct> GetProductsList();
        TblProduct GetProductsById(int Id);
        IEnumerable<TblProduct> SearchProductsByUnitPrice(string name, int from, int to);
        IEnumerable<TblProduct> SearchProductsByProductName(string name);
        void Update(TblProduct product);
        void AddNew(TblProduct product);
        void Remove(int productId);
    }
}
