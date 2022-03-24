using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CS872_WebApp.DataAccessLayer;
using CS872_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace CS872_WebApp.Controllers
{
    public class UserAccountController : Controller
    {
        MySqlDBContext dbContext = new MySqlDBContext();

        // GET: StandardViewModel
        public ActionResult Index()
        {
            if(HttpContext.User.Identity.IsAuthenticated) 
            { 
                return RedirectToAction("Index", "UserAccount");
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<ActionResult> Index(LoginViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new[] { new Claim(ClaimTypes.Name, model.emailAddress), new Claim(ClaimTypes.Role, model.userType) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ResponseModel<string> resultInfo = await dbContext.login(model);
            //ResponseModel<string> resultInfo = await dbContext.login(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            //context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (resultInfo.resultCode == 200)
            {
                
                //FormsAuthentication.SetAuthCookie(model.emailAddress, false);
                return RedirectToAction("Index", "UserAccount", resultInfo.Data); //redirect to login form
            }
            else
            {
                ModelState.AddModelError("", resultInfo.message);
                
            }

            return View();
        }

        public ActionResult Register() 
        {
            // Initialization.  
            //CommonViewModel model = new CommonViewModel();
            //model.AccountVM = new AccountViewModel();
            //model.ResultSetVM = new ResultSetViewModel();

            //dynamic dynamicModel = new ExpandoObject();
            //dynamicModel.AccountViewModels = new List<AccountViewModel>().;

            //// Get Result  
            //model.ResultSetVM.ResultSet = this.LoadData();

           return View();

        }


        [HttpPost]
        public async Task<ActionResult> Register(LoginViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await dbContext.register(model);

            if (result.resultCode == 200)
            {
                return RedirectToAction("Index"); 
            }
            else
            {
                ModelState.AddModelError("", result.message);
            }

            return View();
        }

        //public ActionResult Logout() 
        //{
        //    HttpContext.User.SignOut();
        //    return RedirectToAction("Index"); 
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}