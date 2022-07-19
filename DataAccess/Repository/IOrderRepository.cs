using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<TblOrder> GetOrdersList(string cs);
        TblOrder GetOrdersById(string cs, int Id);
        IEnumerable<TblOrder> SearchOrders(string cs, int name);
        void Update(string cs, TblOrder order);
        void AddNew(string cs, TblOrder order);
        void Remove(string cs, int orderId);
    }
}
