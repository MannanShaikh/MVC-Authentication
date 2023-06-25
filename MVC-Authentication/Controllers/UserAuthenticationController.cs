using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Authentication.Models.DTO;
using MVC_Authentication.Repositories.Abstract;

namespace MVC_Authentication.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService _service;
        public UserAuthenticationController(IUserAuthenticationService service)
        {
            this._service = service;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            if (result.Message == "User has been registered")
            {
                return RedirectToAction(nameof(Login)); 
            }
            else
            {
                return RedirectToAction(nameof(Registration));
            }
        }

        public async  Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            if (result.Message == "User has been registered")
            {
                return RedirectToAction(nameof(Login)); 
            }
            else
            {
                return RedirectToAction(nameof(Registration));
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        //public async Task<IActionResult> AdminRegistration()
        //{
        //    var model = new RegistrationModel()
        //    {
        //        UserName = "Admin",
        //        Name = "Admin",
        //        Email="admin@mail.com",
        //        Role="admin",
        //        Password="Admin@123"
        //    };
        //    var result = await _service.RegistrationAsync(model);
        //    return Ok(result);
        //}

    }
}
