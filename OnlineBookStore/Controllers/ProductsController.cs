using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _dbContext = null;

        public ProductsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Products

        public ActionResult Index()
        {
            var products = _dbContext.Books.ToList();
            return View(products);
        }

        // GET: Products/Details/{Id}
        public ActionResult Details(int id)
        {
             var product = _dbContext.Books.Include(p =>p.BookType).FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return View(product);
            }

            return Content($"No Book available with {id}");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _dbContext.Books.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var bookTypes = _dbContext.BookTypes.ToList();

                ViewBag.BookTypes = bookTypes;
                return View(product);
            }
            return HttpNotFound("Book Id Doesn't Exists");
        }


        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (product != null)
            {
                var bookInDb = _dbContext.Books.Find(product.Id);
                if (bookInDb != null)
                {
                    bookInDb.BookName = product.BookName;
                    bookInDb.AuthorName = product.AuthorName;
                    bookInDb.Publisher = product.Publisher;
                    bookInDb.Description = product.Description;
                    bookInDb.BookType = product.BookType;

                    bookInDb.Price = product.Price;
                  
                    //_context.Products.AddOrUpdate(productInDb);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            var bookTypes = _dbContext.BookTypes.ToList();
            ViewBag.BookTypes = bookTypes;
            return View(product);
        }
    }
}