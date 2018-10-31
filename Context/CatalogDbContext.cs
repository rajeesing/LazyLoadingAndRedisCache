using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext():base("DbConnection")
        { }
        public DbSet<ProductItem> ProductItemContext { get; set; }
    }
}
