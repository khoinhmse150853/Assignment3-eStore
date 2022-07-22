using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static object instanceLook = new object();

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public List<TblOrder> GetOrdersList()
        {
            using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
            {
                return db.TblOrders.Include(x => x.Member).ToList();
            }
        }

        public List<TblOrder> GetOrdersListByMember(int memberId)
        {
            using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
            {
                return db.TblOrders.Include(x => x.Member).Where(o => o.MemberId == memberId).ToList();
            }
        }

        public List<TblOrder> GetOrders(DateTime startDate, DateTime endDate)
        {
            List<TblOrder> orders = null;

            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    orders = db.TblOrders.Where(or =>
                        DateTime.Compare(or.OrderDate, startDate) >= 0 &&
                        DateTime.Compare(or.OrderDate, endDate) <= 0).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }

        public TblOrder GetOrderById(int Id)
        {
            TblOrder order = null;
            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    order = db.TblOrders.Include(x => x.Member).SingleOrDefault(p => p.OrderId == Id);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddNew(TblOrder order)
        {
            try
            {
                TblOrder _order = GetOrderById(order.OrderId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_order == null)
                    {
                        db.TblOrders.Add(order);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The order ia already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TblOrder order)
        {
            try
            {
                TblOrder _order = GetOrderById(order.OrderId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_order != null)
                    {
                        db.TblOrders.Update(order);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The order does not already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int orderId)
        {
            try
            {
                TblOrder _order = GetOrderById(orderId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_order != null)
                    {
                        db.TblOrders.Remove(_order);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The order does not already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
