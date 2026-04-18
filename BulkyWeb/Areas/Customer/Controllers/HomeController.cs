using Bulky.DataAccess.Repositary;
using Bulky.Models;
using Bulky.Models.Models;
using BulkyWeb.Models;
using DI_Service_Lifetime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedGuidService _scoped1;
        private readonly IScopedGuidService _scoped2;
        private readonly ITransientGuidService _transient1;
        private readonly ITransientGuidService _transient2;
        private readonly ISingletonGuidService _singleton1;
        private readonly ISingletonGuidService _singleton2;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,
            IScopedGuidService scoped1,
            IScopedGuidService scoped2,
            ITransientGuidService transient1,
            ITransientGuidService transient2,
            ISingletonGuidService singleton1,
            ISingletonGuidService singleton2,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Scoped 1:{_scoped1.GetGuid()}");
            _logger.LogInformation($"Scoped 2:{_scoped2.GetGuid()}");
            _logger.LogInformation($"Transient 1:{_transient1.GetGuid()}");
            _logger.LogInformation($"Transient 2:{_transient2.GetGuid()}");
            _logger.LogInformation($"Singleton 1:{_singleton1.GetGuid()}");
            _logger.LogInformation($"Singleton 2:{_singleton2.GetGuid()}");
            List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(ProductList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
            Product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category"),
            Count=10,
            ProductId=productId

            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart ShoppingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == ShoppingCart.ProductId);
            if (cartFromDb != null)
            {
                // shopping cart exist
                cartFromDb.Count += ShoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCart.Add(ShoppingCart);
            }
            TempData["success"] = "Cart updated successfully.";
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));

            
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
