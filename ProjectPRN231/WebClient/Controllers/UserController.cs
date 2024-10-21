using BussinessObject.DTOS;
using BussinessObject.DTOS.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class UserController : Controller
    {


        // Default action, showing the user index page
        [HttpGet]
        public async Task<IActionResult> Index(string searchUsername = "", string sortOption = "asc")
        {
            List<UserDTO> users;

            if (!string.IsNullOrEmpty(searchUsername))
            {
                users = await APIFunction.SearchUser(searchUsername); // Search users by username
            }
            else
            {
                users = await APIFunction.GetAllUsersAsync(); // Get all users if no search is performed
            }

            // Sort the users based on the sortOption parameter
            if (sortOption == "desc")
            {
                users = users.OrderByDescending(u => u.UserName).ToList(); // Sort by Username descending
            }
            else
            {
                users = users.OrderBy(u => u.UserName).ToList(); // Sort by Username ascending
            }

            // Pass the current search and sort options to the view
            ViewData["Message"] = TempData["Message"]; // Retains any message from TempData
            ViewData["SearchUsername"] = searchUsername; // Retain search term in the view
            ViewData["SortOption"] = sortOption; // Retain the sort option in the view
            return View(users); // Pass the sorted and filtered user list to the view
        }






        // Get User by ID
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var user = await APIFunction.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit User - GET
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var user = await APIFunction.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit User - POST
        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await APIFunction.UpdateUserAsync(model);
            if (result == 200)
            {
                TempData["Message"] = "User updated successfully."; // Flash message
                return RedirectToAction(nameof(Index)); // Use nameof for refactoring safety
            }

            ViewBag.ErrorMessage = "Update failed.";
            return View(model);
        }

        // Delete User
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var result = await APIFunction.DeleteUserAsync(id);
            if (result == 200)
            {
                TempData["Message"] = "User deleted successfully.";
                return RedirectToAction(nameof(Index)); // Use nameof for refactoring safety
            }

            ViewBag.ErrorMessage = "Delete failed.";
            return RedirectToAction(nameof(Index)); // Redirect even on failure to stay on the same page
        }
    }
}
