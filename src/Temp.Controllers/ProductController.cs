using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Temp.Components.Security;
using Temp.Objects.Views;
using Temp.Services;

namespace Temp.Controllers
{
    [AllowUnauthorized]
    public class Product : ServicedController<ProductService>
    {
        public Product(ProductService productService) : base(productService)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<ProductView> products = Service.GetProducts(); 
            return View(products); 
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductView productView)
        {
            if (ModelState.IsValid)
            {
                Service.Create(productView); 
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(productView); 
        }
        public IActionResult Edit(int id)
        {
            ProductView? productView = Service.Get<ProductView>(id);

            if (productView == null)
            {
                return NotFound(); 
            }

            return View(productView); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductView productView)
        {
            if (id != productView.Id)
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Service.Edit(productView); 
                    TempData["Success"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index)); 
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while updating the product.";
                    return View(productView); 
                }
            }

            return View(productView); 
        }

        public IActionResult Delete(int id)
        {
            ProductView? productView = Service.Get<ProductView>(id);
            if (productView == null)
            {
                return NotFound(); 
            }
            return View(productView); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Service.Delete(id);

                TempData["Success"] = "Product deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while deleting the product: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
