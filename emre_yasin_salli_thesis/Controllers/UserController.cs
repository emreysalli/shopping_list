using emre_yasin_salli_thesis.Areas.Identity.Data;
using emre_yasin_salli_thesis.Data;
using emre_yasin_salli_thesis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace emre_yasin_salli_thesis.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ShoppingListDbContext context;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
        {
            context = new ShoppingListDbContext();
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingList()
        {
            var shoppingLists = context.ShoppingLists.Select(s => s).ToList();
            return View(shoppingLists);
        }

        [HttpGet]
        public IActionResult ShoppingListCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShoppingListCreate(ShoppingList model)
        {
            string UserId = _userManager.GetUserId(this.User);

            if (ModelState.IsValid)
            {
                ShoppingList shoppingList = new ShoppingList();
                shoppingList.ListName = model.ListName;
                shoppingList.IsShopping = false;
                shoppingList.IsComplete = false;
                shoppingList.UserId = UserId;
                context.ShoppingLists.Add(shoppingList);
                context.SaveChanges();
                return RedirectToAction("ShoppingList");
            }
            return View(model);
        }

        public IActionResult DeleteShoppingList(int id)
        {
            var entity = context.ShoppingLists.Find(id);
            if (entity != null)
            {
                context.ShoppingLists.Remove(entity);
            }
            context.SaveChanges();
            return RedirectToAction("ShoppingList");
        }

        public IActionResult GoShopping(int id)
        {
            var entity = context.ShoppingLists.Find(id);
            if (entity != null)
            {
                entity.IsShopping = true;
            }
            context.SaveChanges();
            return RedirectToAction("ListProductsInShoppingList", new {id = id});
        }

        public IActionResult CompletedShopping(int id)
        {
            var entity = context.ShoppingLists.Find(id);
            if (entity != null)
            {
                entity.IsShopping = false;
            }
            context.SaveChanges();
            return RedirectToAction("ShoppingList");
        }

        //public IActionResult ListProductsForShoppingList(int id)
        //{
        //    ViewBag.ShoppingList = context.ShoppingLists.Find(id);
        //    ViewBag.Categories = context.Categories.Select(c => c).ToList();
        //    ViewBag.Products = context.Products.Select(p => p).ToList();
        //    return View();
        //}

        public IActionResult ListProductsForShoppingList(int id, int categoryId)
        {
            ViewBag.ShoppingList = context.ShoppingLists.Find(id);
            ViewBag.Categories = context.Categories.Select(c => c).ToList();
            if(categoryId == 0)
            {
                ViewBag.Products = context.Products.Select(p => p).ToList();
            }
            else
            {
                ViewBag.SelectedCategory = categoryId;
                ViewBag.Products = context.Products.Where(p => p.CategoryId == categoryId).Select(p => p).ToList();
            }
            return View();
        }

        public IActionResult ListProductsInShoppingList(int id)
        {
            var shoppingListDetails = context.ShoppingListDetails.Where(s => s.ShoppingListId == id).Select(s => s).Include(s => s.Product).ToList();
            ViewBag.ShoppingList = context.ShoppingLists.Where(s => s.Id == id).Select(s => new
                                                                                    {
                                                                                        Id = s.Id,
                                                                                        ListName = s.ListName,
                                                                                        IsShopping = s.IsShopping
                                                                                        
                                                                                    }).ToList();
            return View(shoppingListDetails);
        }
        
        public IActionResult DeleteProductinShoppingList(int shoppingListDetailId, int shoppingListId)
        {
            
            var entity = context.ShoppingListDetails.Find(shoppingListDetailId);
            if (entity != null)
            {
                context.ShoppingListDetails.Remove(entity);
            }
            context.SaveChanges();
            return RedirectToAction("listproductsinshoppinglist", new { id = shoppingListId } );
        }

        [HttpGet]
        public IActionResult AddProductToShoppingList(int productId, int shoppingListId)
        {
            ViewBag.Product = context.Products.Find(productId);
            ViewBag.ShoppingListId = shoppingListId;

            return View();
        }

        [HttpPost]
        public IActionResult AddProductToShoppingList(ShoppingListDetail model)
        {
            string UserId = _userManager.GetUserId(this.User);

            if (ModelState.IsValid)
            {
                ShoppingListDetail shoppingListDetail = new ShoppingListDetail();   
                shoppingListDetail.ProductId = model.ProductId;
                shoppingListDetail.ShoppingListId = model.ShoppingListId;
                shoppingListDetail.Explanation = model.Explanation;
                shoppingListDetail.IsTaken = false;
                context.ShoppingListDetails.Add(shoppingListDetail);
                context.SaveChanges();
                return RedirectToAction("ShoppingList");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ShoppingListDetailEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = context.ShoppingListDetails.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ShoppingListDetail()
            {
                ShoppingListDetailId = entity.ShoppingListDetailId,
                Explanation = entity.Explanation, 
                ProductId = entity.ProductId,
                ShoppingListId = entity.ShoppingListId
            };
            ViewBag.Product = context.Products.Find(entity.ProductId);
            return View(model);
        }

        [HttpPost]
        public IActionResult ShoppingListDetailEdit(ShoppingListDetail model)
        {
            if (ModelState.IsValid)
            {

                var entity = context.ShoppingListDetails.Find(model.ShoppingListDetailId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Explanation = model.Explanation;

                context.ShoppingListDetails.Update(entity);
                context.SaveChanges();
                return RedirectToAction("ListProductsInShoppingList", new {id = model.ShoppingListId});
            }
            return View(model);
        }

        public IActionResult Search(string q, int id)
        {
            var products = context
                                .Products
                                .Where(i => i.Name.ToLower().Contains(q.ToLower()))
                                .AsQueryable();
            ViewBag.ShoppingList = context.ShoppingLists.Find(id);
            ViewBag.Categories = context.Categories.Select(c => c).ToList();
            ViewBag.Products = products.ToList();
            return View("ListProductsForShoppingList");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
