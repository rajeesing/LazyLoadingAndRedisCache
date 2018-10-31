using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class SalesOrderDbContext : DbContext
    {
        public SalesOrderDbContext() : base("DbConnection")
        {
            bool lazyLoadingStatus = this.Configuration.LazyLoadingEnabled; //default is true, so we are good
        }
        public DbSet<SalesOrder> SalesOrdersContext { get; set; }
        public DbSet<SalesOrderItem> SalesOrdersItemContext { get; set; }
    }
}
