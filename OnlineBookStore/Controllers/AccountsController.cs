using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineBookStore.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _dbContext = null;

        public AccountsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Accounts
        public ActionResult Register()
        {
            RegisterViewModel viewModel = new RegisterViewModel
            {
                Roles = _dbContext.Roles.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        Email = viewModel.EmailId,
                        Name = viewModel.Username,
                        Password = viewModel.Password
                    };

                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();

                  
                    return RedirectToAction("Login");
                }

                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("Username", ex.InnerException.InnerException.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Username", ex.Message);
                }
            }
            return View(viewModel);
        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
      
        public ActionResult Login(LoginViewModel loginViewModel) 
        {
            var validStudent = _dbContext.Users.Any(u => u.Name == loginViewModel.Username && u.Password == loginViewModel.Password);

            if(validStudent)
            {
                FormsAuthentication.SetAuthCookie(loginViewModel.Username, true);
                return RedirectToAction("Index", "Products");
            }

            TempData["Alert"] = "Invalid login credentials. Please try again.";
            return View();
           /* ModelState.AddModelError("", "User Name or Password doesn't match");
            return View();*/
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpCookie myCookie = new HttpCookie("sessionId");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            return RedirectToAction("Login", "Accounts");
        }
        //Apply Role for User
        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public ActionResult AddRole()
        {
            UserRoleViewModel userRole = new UserRoleViewModel
            {
                Users = _dbContext.Users.ToList(),
                Roles = _dbContext.Roles.ToList()
            };
            return View(userRole);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public ActionResult AddRole(UserRoleViewModel userRoleVM)
        {
            if (ModelState.IsValid)
            {
                UserRolesMapping userRolesMapping = new UserRolesMapping
                {
                    UserId = userRoleVM.UserId,
                    RoleId = userRoleVM.RoleId
                };
                _dbContext.UserRolesMappings.Add(userRolesMapping);
                _dbContext.SaveChanges();
            }

            UserRoleViewModel userRole = new UserRoleViewModel
            {
                Users = _dbContext.Users.ToList(),
                Roles = _dbContext.Roles.ToList()
            };
            return RedirectToAction("Index","Products"); //View(userRole);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult RolesAndUsers()
        {
            var userAndRoles = (from user in _dbContext.Users
                                join userRole in _dbContext.UserRolesMappings on user.Id equals userRole.UserId
                                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                                orderby role.RoleName ascending
                                select new UsersAndRoles
                                {
                                    Username = user.Name,
                                    RoleName = role.RoleName
                                }).ToList();
            return View("RolesAndUsers", userAndRoles);
        }
    }
}