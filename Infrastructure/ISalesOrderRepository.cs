using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Get the sales order
    /// </summary>
    public interface ISalesOrderRepository
    {
        List<SalesOrderItem> GetSalesOrderById(int? id);
    }
}
