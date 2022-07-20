using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public partial class SaleManagementContext : DbContext
    {
        public static string GetConn { get; set; }
        public SaleManagementContext(string conn)
        {
            this.Database.SetConnectionString(conn);
            GetConn = conn;
        }
    }
}
