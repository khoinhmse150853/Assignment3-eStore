using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<TblOrder> GetOrdersList();
        IEnumerable<TblOrder> GetOrdersListByMember(int memberId);
        IEnumerable<TblOrder> MakeReportStatistic(DateTime startDate, DateTime endDate);
        TblOrder GetOrderById(int orderId);
        void AddNew(TblOrder order);
        void Update(TblOrder order);
        void Remove(int orderId);
    }
}
