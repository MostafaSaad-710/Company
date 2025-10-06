using System.Diagnostics;
using System.Text;
using Company.G01.PL.Models;
using Company.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly ITransentService transentService01;
        private readonly ISingletolService singletolService01;
        private readonly ISingletolService singletolService02;

        public IScopedService ScopedService02 { get; }
        public ITransentService TransentService02 { get; }

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransentService transentService01,
            ITransentService transentService02,
            ISingletolService singletolService01,
            ISingletolService singletolService02
             
            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.ScopedService02 = scopedService02;
            this.transentService01 = transentService01;
            this.TransentService02 = transentService02;
            this.singletolService01 = singletolService01;
            this.singletolService02 = singletolService02;
        }
        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedService01 :: {scopedService01.GetGuid()}\n");
            builder.Append($"ScopedService02 :: {ScopedService02.GetGuid()}\n\n");
            builder.Append($"transentService01 :: {transentService01.GetGuid()}\n");
            builder.Append($"TransentService02 :: {TransentService02.GetGuid()}\n\n");
            builder.Append($"singletolService01 :: {singletolService01.GetGuid()}\n");
            builder.Append($"singletolService02 :: {singletolService02.GetGuid()}\n\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
