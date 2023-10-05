using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExample.UseIDisturbutedCache.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Index(string key,string value)
        {
            try
            {
                DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

                cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(3);

                _distributedCache.SetString(key, value, cacheEntryOptions);

                ViewBag.Msj = "Eklendi";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Msj = ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public IActionResult Get(string key)
        {
            var donendeger= _distributedCache.GetString(key);
            return RedirectToAction("Index");
        }
    }
}
