using Infrastructure;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StackExchange.Redis;
using System.Configuration;
using Model;
using Newtonsoft.Json;

namespace LazyLoadingSample.Controllers
{
    public class HomeController : Controller
    {

        #region Cache Properties
        // Redis Connection string info
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = ConfigurationManager.AppSettings["CacheConnection"].ToString();
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        #endregion


        private ISalesOrderRepository repo;
        public HomeController(ISalesOrderRepository _repo)
        {
            this.repo = _repo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            ViewBag.Message = "Order Details (Lazy Loading Pattern)";
            Order oOrder = new Order(); //this will not load the items, until you don't call the items (get method)
            return View(oOrder);
        }



        public ActionResult Cache(int? id)
        {
            ViewBag.Message = "Cache Sample";
            try
            {
                var saleOrderData = GetSalesOrder(id);
                return View(saleOrderData);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cache(string source)
        {
            ClearCachedTeams();
            return RedirectToAction("Cache");
        }


        public List<SalesOrderItem> GetSalesOrder(int? id)
        {
            List<SalesOrderItem> salesOrder = null;
            IDatabase cache = Connection.GetDatabase();
            string serializeSalesOrder = cache.StringGet("orderlist" + id);
            if (!String.IsNullOrEmpty(serializeSalesOrder))
            {
                ViewBag.Where = "Cached";
                salesOrder = JsonConvert.DeserializeObject<List<SalesOrderItem>>(serializeSalesOrder);
            }
            else
            {
                ViewBag.Where = "DB";
                // Get from database and store in cache
                salesOrder = repo.GetSalesOrderById(id);
                cache.StringSet("orderlist"+id, JsonConvert.SerializeObject(salesOrder));
            }
            return salesOrder;
        }

        void ClearCachedTeams()
        {
            IDatabase cache = Connection.GetDatabase();
            //All keys has to delete, in this two sales order with id 1 & 2 will be deleted
            cache.KeyDelete("orderlist1");
            cache.KeyDelete("orderlist2");
            ViewBag.msg += "Order has been removed from cache";
        }
    }

    #region SAMPLE CLASS TO DEMONSTRATE LAZY LOADING
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        private Lazy<List<Item>> _items = null; //see this mark as "Lazy"
        public List<Item> Items
        {
            get
            {
                return _items.Value;
                
            }
        }

        public Order()
        {
            Id = 1;
            OrderDate = DateTime.Now;
            OrderNumber = "O2323";
            TotalPrice = 30;
            _items = new Lazy<List<Item>>(() => BindItems());
        }

        private List<Item> BindItems()
        {
            List<Item> items = new List<Item>();
            items.Add(new Item { Id = 1, Name = "Shoes 1", Price = 30 });
            items.Add(new Item { Id = 2, Name = "Shoes 2", Price = 66 });
            items.Add(new Item { Id = 3, Name = "Leotard 1", Price = 33 });
            items.Add(new Item { Id = 4, Name = "Leotard 2", Price = 33 });
            items.Add(new Item { Id = 5, Name = "Leotard 3", Price = 33 });
            items.Add(new Item { Id = 6, Name = "Tank Unitard 1", Price = 33 });
            items.Add(new Item { Id = 7, Name = "Tank Unitard 2", Price = 33 });
            return items;
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        
    }
    #endregion
}