using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Controllers
{
    public class CategoryController : Controller
    {
        private ShopbridgedbContext _db;
        public CategoryController(ShopbridgedbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var data = _db.Categories.ToList();
            return Ok(data);
        }
    }
}
