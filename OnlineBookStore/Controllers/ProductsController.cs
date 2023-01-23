using OnlineBookStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

        public ActionResult Index(string search,int? page,string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "BookName DESC" : "";
            ViewBag.SortPriceParameter = sortBy == "Price" ? "Price DESC" : "Price";

            var products = _dbContext.Books.AsQueryable();

            products = _dbContext.Books.Where(b => b.BookName.StartsWith(search) || search == null);

            switch (sortBy)
            {
                case "BookName DESC":
                    products = products.OrderByDescending(b => b.BookName); break;
                case "Price DESC":
                    products = products.OrderByDescending(b => b.Price); break;
                case "Price":
                    products = products.OrderBy(b => b.Price); break;
                default :
                    products = products.OrderBy(b => b.BookName); break;
            }

           // products = products.ToPagedList(page ?? 1, 2);
            return View(products.ToPagedList(page ?? 1, 2));
        }

        // GET: Products/Details/{Id}
        [Authorize]
        [ValidateAntiForgeryToken]
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
        [Authorize(Roles = "Librarian")]
      
        public ActionResult Create()
        {
            var bookTypes = _dbContext.BookTypes.ToList();


            ViewBag.BookTypes = bookTypes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]

        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Books.Add(product);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("BookName", ex.InnerException.InnerException.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("BookName", ex.Message);
            }
            var bookTypes = _dbContext.BookTypes.ToList();


            ViewBag.BookTypes = bookTypes;

            return View();
        }
        [HttpGet]
      
        [Authorize(Roles = "Librarian")]

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]

        public ActionResult Edit(Product product)
        {
            try
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
                        
                        bookInDb.BookTypeId = product.BookTypeId;

                        bookInDb.Price = product.Price;

                        //  _dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;

                        //_context.Products.AddOrUpdate(productInDb);
                        _dbContext.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("BookName", ex.InnerException.InnerException.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("BookName", ex.Message);
            }


            //Populate the DropDownlist(bookTypes)
            var bookTypes = _dbContext.BookTypes.ToList();
            ViewBag.BookTypes = bookTypes;
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var bookInDb = _dbContext.Books.FirstOrDefault(b =>b.Id == id);
            if(bookInDb != null)
            {
                _dbContext.Books.Remove(bookInDb);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}