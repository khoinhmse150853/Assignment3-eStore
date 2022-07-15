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

        public List<TblProduct> GetProductsList(string cs)
        {
            using(var db = new SaleManagementContext(cs))
            {
                return db.TblProducts.ToList();
            }
        }

        public TblProduct GetProductByID(string cs, int productId)
        {
            TblProduct product = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
                {
                    product = db.TblProducts.SingleOrDefault(p => p.ProductId == productId);
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public List<TblProduct> SearchProducts (string cs, string name, int from, int to)
        {
            List<TblProduct> products = null;
            try
            {
                using (var db = new SaleManagementContext(cs))
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

        public void Update(string cs, TblProduct product)
        {
            try
            {
                TblProduct _product = GetProductByID(cs, product.ProductId);
                using (var db = new SaleManagementContext(cs))
                {
                    if(_product != null)
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

        public void AddNew(string cs, TblProduct product)
        {
            try
            {
                TblProduct _product = GetProductByID(cs, product.ProductId);
                using (var db = new SaleManagementContext(cs))
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

        public void Remove(string cs, int productId)
        {
            try
            {
                TblProduct _product = GetProductByID(cs, productId);
                using (var db = new SaleManagementContext(cs))
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
