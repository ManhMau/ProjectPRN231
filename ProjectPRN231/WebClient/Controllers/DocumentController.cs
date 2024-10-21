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
            else
            {
                documents = await APIFunction.GetListDocuments();
            }

            // Sort the documents based on the sortOption parameter
            if (sortOption == "desc")
            {
                documents = documents.OrderByDescending(d => d.CreatedAt).ToList();
            }
            else
            {
                documents = documents.OrderBy(d => d.CreatedAt).ToList();
            }

            ViewData["Message"] = TempData["Message"];
            ViewData["SearchTitle"] = searchTitle; // For retaining the search title in the view
            ViewData["SortOption"] = sortOption; // For retaining sort order in the view
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
        public async Task<IActionResult> Create(DocumentDTO model)
        {
            int result = await APIFunction.CreateDocumentAsync(model);

            if (result == 200)
            {
                TempData["Message"] = "Document created successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error creating Document. Please try again.";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await APIFunction.GetDocumentById(id);
            var types = await APIFunction.GetListDocType();

            if (product == null)
            {
                TempData["Message"] = "Document not found.";
                return RedirectToAction("Index");
            }

            var viewModel = new DocumentViewModel
            {
                DocumentId = product.DocumentId,
                Title = product.Title,
                Description = product.Description,
                CreatedAt = DateTime.Now, // Gán thời gian hiện tại nếu CreatedAt là null
                FilePath = product.FilePath,
                Status = product.Status,
                Type = types
            };

            return View(viewModel); // Đảm bảo bạn trả về viewModel
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DocumentDTO model)
        {
            int result = await APIFunction.UpdateDocument(model);

            if (result == 200)
            {
                TempData["Message"] = "Document update successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Error creating Document. Please try again.";
            }
            return View(model);
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
