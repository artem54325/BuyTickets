using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClickLikeVk.Models;
using BuyingTicketCore.Database;

namespace ClickLikeVk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostgresContext _context;

        public HomeController(ILogger<HomeController> logger, PostgresContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Res = _context.LikeVkModels;
            ViewBag.Groups = _context.LikeVkModels.GroupBy(a => a.IdPage).Select(a => new GroupLike { Count = a.Count(), Value = a.Key });
            return View();
        }

        public IActionResult SecondPage()
        {
            ViewBag.Res = _context.LikeVkModels;
            ViewBag.Groups = _context.LikeVkModels.GroupBy(a => a.IdPage).Select(a => new GroupLike {Count = a.Count(), Value =a.Key});
            return View();
        }
        public IActionResult ThirdPage()
        {
            ViewBag.Res = _context.LikeVkModels;
            ViewBag.Groups = _context.LikeVkModels.GroupBy(a => a.IdPage).Select(a => new GroupLike { Count = a.Count(), Value = a.Key });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveClick([FromForm] string idPage)
        {
            LikeVkModel model = new LikeVkModel
            {
                Id = Guid.NewGuid().ToString(),
                IdPage = idPage,
                DateTime = DateTime.Now
            };
            await _context.LikeVkModels.AddAsync(model);
            try
            {
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
