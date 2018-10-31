using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
    }

    [Table("SalesOrder")]
    public class SalesOrder
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        [NotMapped]
        public List<ProductItem> ProductList { get; set; }
    }

    [Table("SalesOrderItem")]
    public class SalesOrderItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

    }
}
