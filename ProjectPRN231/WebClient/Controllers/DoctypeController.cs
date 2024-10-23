using BusinessObject.DTO;
using BussinessObject.DTOS;
using DataAccess.DAO;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class DocTypeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchDocTypeName = "", string sortOption = "id", string sortDirection = "asc")
        {
            List<DocTypeMapper> docTypes = new List<DocTypeMapper>();

            try
            {
                // Perform search if DocType name is provided
                if (!string.IsNullOrEmpty(searchDocTypeName))
                {
                    docTypes = await APIFunction.SearchDocTypesByName(searchDocTypeName);
                }
                else
                {
                    docTypes = await APIFunction.GetListDocTypes();
                }

                // Sort docTypes by sortOption and sortDirection
                docTypes = SortDocTypes(docTypes, sortOption, sortDirection);
            }
            catch (Exception ex)
            {
                // Log error (you might want to use a logging framework)
                TempData["ErrorMessage"] = "An error occurred while fetching DocTypes. Please try again later.";
            }

            // Store search and sorting information in ViewData for display
            ViewData["Message"] = TempData["Message"];
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            ViewData["SearchDocTypeName"] = searchDocTypeName;
            ViewData["SortOption"] = sortOption;
            ViewData["SortDirection"] = sortDirection;

            return View(docTypes);
        }

        // Sorting method
        private List<DocTypeMapper> SortDocTypes(List<DocTypeMapper> docTypes, string sortOption, string sortDirection)
        {
            switch (sortOption.ToLower())
            {
                case "id":
                    docTypes = sortDirection.ToLower() == "desc"
                        ? docTypes.OrderByDescending(dt => dt.Id).ToList()
                        : docTypes.OrderBy(dt => dt.Id).ToList();
                    break;

                case "name":
                    docTypes = sortDirection.ToLower() == "desc"
                        ? docTypes.OrderByDescending(dt => dt.TypeName).ToList()
                        : docTypes.OrderBy(dt => dt.TypeName).ToList();
                    break;

                default:
                    break; // Handle default case or log a warning
            }

            return docTypes;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new DocTypeMapper());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocTypeMapper model)
        {
            try
            {
                int result = await APIFunction.CreateDocTypeAsync(model);

                if (result == 200)
                {
                    TempData["Message"] = "DocType created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error creating DocType. Please try again.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var docType = await APIFunction.GetDocTypeById(id);

            if (docType == null)
            {
                TempData["ErrorMessage"] = "DocType not found.";
                return RedirectToAction("Index");
            }

            return View(docType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DocTypeMapper model)
        {
            try
            {
                int result = await APIFunction.UpdateDocType(model);

                if (result == 200 || result == 204) // 200 OK or 204 No Content means success
                {
                    TempData["SuccessMessage"] = "DocType updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error updating DocType. Please try again.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var docType = await APIFunction.GetDocTypeById(id);

            if (docType == null)
            {
                return NotFound();
            }

            return View(docType);
        }
    }
}
