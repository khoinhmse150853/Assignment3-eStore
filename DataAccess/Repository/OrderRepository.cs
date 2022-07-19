using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddNew(string cs, TblOrder order)
        {
            OrderDAO.Instance.AddNew(cs, order);
        }

        public TblOrder GetOrdersById(string cs, int Id)
        {
            return OrderDAO.Instance.GetOrdersByID(cs, Id);
        }

        public IEnumerable<TblOrder> GetOrdersList(string cs)
        {
            return OrderDAO.Instance.GetAllOrders(cs);
        }

        public void Remove(string cs, int orderId)
        {
            OrderDAO.Instance.Remove(cs, orderId);
        }

        public IEnumerable<TblOrder> SearchOrders(string cs, int id)
        {
            return OrderDAO.Instance.SearchOrder(cs,id);
        }

        public void Update(string cs, TblOrder order)
        {
            OrderDAO.Instance.Update(cs, order);
        }
    }
}
