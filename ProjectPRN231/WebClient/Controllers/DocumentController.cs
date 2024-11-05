using BussinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchTitle = "", string sortOption = "asc")
        {
            List<DocumentDTO> documents;

            if (!string.IsNullOrEmpty(searchTitle))
            {
                documents = await APIFunction.SearchDocumentsByTitle(searchTitle);
            }
            else if (sortOption == "group")
            {
                documents = await APIFunction.GroupDocument(); // Call the group method
            }
            else
            {
                documents = await APIFunction.GetListDocuments();
            }

            // Sorting logic (skip if "group" option is selected)
            if (sortOption == "desc")
            {
                documents = documents.OrderByDescending(d => d.CreatedAt).ToList();
            }
            else if (sortOption == "asc")
            {
                documents = documents.OrderBy(d => d.CreatedAt).ToList();
            }

            ViewData["Message"] = TempData["Message"];
            ViewData["SearchTitle"] = searchTitle; // Retain search title in the view
            ViewData["SortOption"] = sortOption; // Retain sort order in the view
            return View(documents);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var types = await APIFunction.GetListDocType();


            var viewModel = new DocumentViewModel
            {
                Type = types
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentAddDTO model)
        {
            if (!ModelState.IsValid)
            {
                var typeeee = await APIFunction.GetListDocType();
                var viewModel = new DocumentViewModel
                {
                    Type = typeeee
                };
                return View(viewModel);
            }

            var result = await APIFunction.CreateDocumentAsync(model);
            var types = await APIFunction.GetListDocType();

            if (result == 200)
            {
                TempData["Message"] = "Document created successfully.";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Error creating Document. Please try again.";
            return View(new DocumentViewModel { Type = types });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the document by its ID
            var document = await APIFunction.GetDocumentById(id);
            var types = await APIFunction.GetListDocType();

            // Check if the document exists
            if (document == null || document.DocumentId <= 0)
            {
                TempData["Message"] = "Document not found.";
                return RedirectToAction("Index");
            }

            // Populate the view model with the document data
            var viewModel = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                Description = document.Description,
                CreatedAt = document.CreatedAt ?? DateTime.Now, // Use created date if available
                Status = document.Status,
                Type = types // Assuming types are needed in the view for selection
            };

            // Return the view with the populated view model
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DocumentAddDTO model)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Please correct the errors in the form.";
                return View(model);
            }

            // Call UpdateDocument and capture the result
            int result = await APIFunction.UpdateDocument(model.DocumentId, model); // Pass the DocumentId along with the model

            // Check the result of the update operation
            if (result == 200)
            {
                TempData["Message"] = "Document updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error updating document. Please try again.";
            }

            // If the update failed, return the same view with the model to show errors
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var (result, filePath) = await APIFunction.DownloadDocument(id); // Gọi phương thức DownloadDocument

            if (result == 200 && filePath != null)
            {
                TempData["Message"] = "Document downloaded successfully.";
                TempData["FilePath"] = filePath; // Lưu đường dẫn tệp vào TempData
            }
            else
            {
                TempData["Message"] = "Error downloading document. Please try again.";
            }

            // Redirect to the Index action
            return RedirectToAction("Index");
        }





        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await APIFunction.DeleteDocument(id);
            if (result == 200)
            {
                TempData["Message"] = "Document deleted successfully!";

            }
            else
            {
                TempData["Message"] = "Error updating Document. Please try again.";

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var document = await APIFunction.GetDocumentById(id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }




    }
}
