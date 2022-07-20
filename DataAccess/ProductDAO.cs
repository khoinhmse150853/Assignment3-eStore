using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static object instanceLook = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<TblProduct> GetProductsList()
        {
            using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
            {
                return db.TblProducts.ToList();
            }
        }

        public TblProduct GetProductByID(int productId)
        {
            TblProduct product = null;
            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    product = db.TblProducts.SingleOrDefault(p => p.ProductId == productId);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public List<TblProduct> SearchProductsByUnitPirce(string name, int from, int to)
        {
            List<TblProduct> products = null;
            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    products = db.TblProducts.Where(p => p.ProductName.Contains(name) && p.UnitPrice <= to && p.UnitPrice >= from).ToList();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<TblProduct> SearchProductsByProductName(string name)
        {
            List<TblProduct> products = null;
            try
            {
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    products = db.TblProducts.Where(p => p.ProductName.Contains(name)).ToList();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public void Update(TblProduct product)
        {
            try
            {
                TblProduct _product = GetProductByID(product.ProductId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_product != null)
                    {
                        db.TblProducts.Update(product);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The product does not already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNew(TblProduct product)
        {
            try
            {
                TblProduct _product = GetProductByID(product.ProductId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_product == null)
                    {
                        db.TblProducts.Add(product);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The product ia already exists");
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int productId)
        {
            try
            {
                TblProduct _product = GetProductByID(productId);
                using (var db = new SaleManagementContext(SaleManagementContext.GetConn))
                {
                    if (_product != null)
                    {
                        db.TblProducts.Remove(_product);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The product does not already exists");
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
