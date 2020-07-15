using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeb.HomeWeb.Models.Login;

namespace MyWeb.HomeWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        [Authorize(Roles ="ADMIN")]
        public IActionResult GetCheck()
        {
            return Json(new { a = 9 });

            //return Json(new { a = 1 });
        }

        [AllowAnonymous]
        public IActionResult GetUserCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { a = 9 });
            }

            return Json(new { a = 1 });
        }
    }
}
