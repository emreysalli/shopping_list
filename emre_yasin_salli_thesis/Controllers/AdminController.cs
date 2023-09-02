using emre_yasin_salli_thesis.Data;
using emre_yasin_salli_thesis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace emre_yasin_salli_thesis.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ShoppingListDbContext context;

        public AdminController() {
            context = new ShoppingListDbContext();
        }

        public IActionResult ProductList()
        {
            var products = context.Products.Select(p => p).ToList();
            return View(products);
        }

        public IActionResult CategoryList()
        {
            var categories = context.Categories.Select(c => c).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            ViewBag.Categories = context.Categories.Select(c => c).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(Product model)
        {
            ViewBag.Categories = context.Categories.Select(c => c).ToList();

            var entity = context.Products.Where(p => p.Name == model.Name).Select(p => p).ToList();
            if (entity.Count != 0)
            {
                ModelState.AddModelError("", "Product name already exists");
                return View();
            }

            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = model.Name;
                product.Image = model.Image;
                product.CategoryId = model.CategoryId;
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("ProductList");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(Category model)
        {
            var entity = context.Categories.Where(c => c.Name == model.Name).Select(c => c).ToList();
            if (entity.Count != 0)
            {
                ModelState.AddModelError("", "Category name already exists");
                return View();
            }

            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = model.Name;
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = context.Products.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new Product()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Image = entity.Image,
                CategoryId = entity.CategoryId,
            };
            ViewBag.Categories = context.Categories.Select(c => c).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductEdit(Product model)
        {
            if (ModelState.IsValid)
            {

                var entity = context.Products.Find(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Image = model.Image;
                entity.CategoryId = model.CategoryId;

                context.Products.Update(entity);
                context.SaveChanges();
                return RedirectToAction("ProductList");
            }
            ViewBag.Categories = context.Categories.Select(c => c).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = context.Categories.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new Category()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(Category model)
        {
            if (ModelState.IsValid)
            {

                var entity = context.Categories.Find(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;

                context.Categories.Update(entity);
                context.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }

        public IActionResult DeleteProduct(int productId)
        {
            var entity = context.Products.Find(productId);
            if (entity != null)
            {
                context.Products.Remove(entity);
            }
            context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = context.Categories.Find(categoryId);
            if (entity != null)
            {
                context.Categories.Remove(entity);
            }
            context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}
