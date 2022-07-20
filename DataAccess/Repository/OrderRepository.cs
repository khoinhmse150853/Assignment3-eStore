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
        public void AddNew(TblOrder order)
        {
            OrderDAO.Instance.AddNew(order);
        }

        public TblOrder GetOrderById(int orderId)
        {
            return OrderDAO.Instance.GetOrderById(orderId);
        }

        public IEnumerable<TblOrder> GetOrdersList()
        {
            return OrderDAO.Instance.GetOrdersList();
        }

        public IEnumerable<TblOrder> MakeReportStatistic(DateTime startDate, DateTime endDate)
        {
            return OrderDAO.Instance.GetOrders(startDate, endDate);
        }

        public void Remove(int orderId)
        {
            OrderDAO.Instance.Remove(orderId);
        }

        public void Update(TblOrder order)
        {
            OrderDAO.Instance.Update(order);
        }
    }
}
