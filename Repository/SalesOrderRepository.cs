using Context;
using Infrastructure;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Get the sales order
    /// </summary>
    public class SalesOrderRepository : ISalesOrderRepository
    {
        SalesOrderDbContext context = new SalesOrderDbContext();
        /// <summary>
        /// Get the sales order by id
        /// </summary>
        /// <param name="id">Sales order id</param>
        /// <returns></returns>
        public List<SalesOrderItem> GetSalesOrderById(int? id)
        {
            try
            {
                return context.SalesOrdersItemContext.Where(so => so.SalesOrderId == id).ToList();
            }
            catch (ArgumentNullException)
            {
                throw new ApplicationException("Unable to fetch information. It may be possible that information is not present.");
            }

            catch (Exception)
            {
                throw new ApplicationException("Unable to read data.");
            }
        }
    }
}
