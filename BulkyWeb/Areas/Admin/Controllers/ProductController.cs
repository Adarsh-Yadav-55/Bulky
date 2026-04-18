using Bulky.DataAccess.Repositary;
using Bulky.Models;
using Bulky.Models.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ProductList=_unitOfWork.Product.GetAll(includeProperties : "Category").ToList();
            return View(ProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
            {
                Text= u.Name,
                Value=u.Id.ToString()
            });
            //ViewBag.CategoryList=CategoryList;
            //ViewBag["CategoryList"] = CategoryList;
            ProductVM ProductVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            return View(ProductVM);
        }

        public IActionResult Upsert(int? Id)
        {
            
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            //ViewBag.CategoryList=CategoryList;
            //ViewBag["CategoryList"] = CategoryList;
            ProductVM ProductVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            if (Id == null || Id == 0)
            {
                return View(ProductVM);
            }
            else
            {
                ProductVM.Product = _unitOfWork.Product.Get(u => u.Id == Id);
                return View(ProductVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, IFormFile? file)
        {
            //if (file == null)
            //{
            //    ModelState.AddModelError("ImageUrl", "Please Upload the Image");
            //}
            if (ModelState.IsValid)
            {
                string wwwRothPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRothPath, @"images\product");

                    // Update => ImageUrl not null => File Exist => File Delete
                    if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRothPath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //new File Upload Continue
                    using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    ProductVM.Product.ImageUrl = @"\images\product\" + fileName;
                    if (ProductVM.Product.Id == 0)
                    {
                        _unitOfWork.Product.Add(ProductVM.Product);
                        _unitOfWork.Save();
                        TempData["success"] = "Product created Successfully.";
                        return RedirectToAction("Index");
                    }
                }
                //update
                if (ProductVM.Product.Id != 0)
                {
                    _unitOfWork.Product.Update(ProductVM.Product);
                    TempData["success"] = "Product updated Successfully.";
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }

            ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(ProductVM);
        }



        [HttpPost]
        public IActionResult Create(ProductVM ProductVM, IFormFile? file)
        {
            if (file == null) 
            {
                ModelState.AddModelError("ImageUrl","Please Upload the Image");
            }
            if (ModelState.IsValid)
            {
                string wwwRothPath=_webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName=Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath=Path.Combine(wwwRothPath, @"images\product");
                    using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    ProductVM.Product.ImageUrl = @"\images\product\" + fileName;

                    _unitOfWork.Product.Add(ProductVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product created Successfully.";
                    return RedirectToAction("Index");
                }

            }

            ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
                return View(ProductVM);
        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.Get(c => c.Id == Id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product ProductObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(ProductObj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated Successfully.";
                return RedirectToAction("Index");
            }
            return View();

        }

        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? ProductFromDb = _unitOfWork.Product.Get(c => c.Id == Id);
        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? Id)
        //{
        //    Product? ProductFromDb = _unitOfWork.Product.Get(c => c.Id == Id);
        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(ProductFromDb);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product deleted Successfully.";
        //    return RedirectToAction("Index");
        //}

        #region API Calls
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = ProductList }  );
        }

        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            Product? ProductFromDb = _unitOfWork.Product.Get(c => c.Id == Id);
            if (ProductFromDb == null)
            {
                return Json(new {success=false,message="Error While Deleting"});
            }
            //Add code for to remove the old image
            var oldImagePath =
                Path.Combine(_webHostEnvironment.WebRootPath,
                ProductFromDb.ImageUrl.TrimStart('\\'));

            _unitOfWork.Product.Remove(ProductFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted Successfully.";
            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}
