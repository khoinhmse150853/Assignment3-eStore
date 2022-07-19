using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance;
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
        public IEnumerable<TblOrder> GetAllOrders(string cs)
        {
            using (var db = new SaleManagementContext(cs))
            {
                return db.TblOrders.ToList();
            }
        }

        public TblOrder GetOrdersByID(string cs, int orderID)
        {
            TblOrder t = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
                {
                    t = db.TblOrders.SingleOrDefault(p => p.OrderId == orderID);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return t;
        }

        public void Update(string cs, TblOrder t)
        {
            try
            {
                TblOrder _order = GetOrdersByID(cs, t.OrderId);
                using (var db = new SaleManagementContext(cs))
                {
                    if (_order != null)
                    {
                        db.TblOrders.Update(t);
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

        public void AddNew(string cs, TblOrder t)
        {
            try
            {
                TblOrder _order = GetOrdersByID(cs, t.OrderId);
                using (var db = new SaleManagementContext(cs))
                {
                    if (_order == null)
                    {
                        db.TblOrders.Add(t);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The order is already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string cs, int orderId)
        {
            try
            {
                TblOrder _order = GetOrdersByID(cs, orderId);
                using (var db = new SaleManagementContext(cs))
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

        public List<TblOrder> SearchOrder(string cs, int id)
        {
            List<TblOrder> t = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
                {
                    t = db.TblOrders.Where(p => p.OrderId.Equals(id)).ToList();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return t;
        }
    }
}
