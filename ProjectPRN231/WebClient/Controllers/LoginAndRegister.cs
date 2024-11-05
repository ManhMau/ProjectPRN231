using BussinessObject.DTOS;
using BussinessObject.DTOS.User;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class LoginAndRegister : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await APIFunction.GetUserByUserNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account is inactive. Please contact support.");
                return View(model);
            }

            var result = await APIFunction.LoginAsync(model);

            if (result.IsSuccess)
            {
                HttpContext.Session.SetString("UserName", model.UserName);
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("", "Login failed. Please try again.");
            return View(model);
        }



        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await APIFunction.RegisterAsync(model);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login", "LoginAndRegister");
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        public IActionResult Logout()
        {
            // Xóa session
            HttpContext.Session.Clear();

            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home"); // Điều hướng về trang chủ
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            // Lấy UserName từ session
            var userName = HttpContext.Session.GetString("UserName");

            // Nếu UserName không tồn tại trong session, điều hướng về trang đăng nhập
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "LoginAndRegister");
            }

            // Gọi API để lấy thông tin người dùng từ tên
            var user = await APIFunction.GetUserByName(userName);
            if (user == null)
            {
                return NotFound();
            }

            // Trả về trang UserProfile với thông tin người dùng
            return View(user);
        }
    }
}

