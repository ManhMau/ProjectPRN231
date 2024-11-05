using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentsController : ControllerBase
    {
        private IDocumentRepository documentRepository;
        private static ProjectPRN231Context context;

        public DocumentsController(IDocumentRepository documentRepository, ProjectPRN231Context projectPRN231Context)
        {
            this.documentRepository = documentRepository;
            context = projectPRN231Context;

        }

        [HttpGet("GetAllDocuments")]
        public async Task<List<DocumentDTO>> GetListDocuments()
        {
            try
            {
                return await documentRepository.GetListDocuments();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetDocumentById/{id}")]
        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            try
            {
                return await documentRepository.GetDocumentById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddDocument")]

        public async Task<IActionResult> AddDocument([FromForm] DocumentAddDTO d)
        {
            var document = new Document
            {
                DocumentId = d.DocumentId,

                Title = d.Title,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
                TypeId = d.TypeId,
                Status = d.Status,




            };
            if (d.FilePath.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AllFiles", d.FilePath.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await d.FilePath.CopyToAsync(stream);
                }
                document.FilePath = d.FilePath.FileName;
            }
            else
            {
                document.FilePath = "";
            }
            context.Documents.Add(document);
            context.SaveChanges();
            return Ok(new { message = "Document created successfully." });


        }

        [HttpPut("UpdateDocument/{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromForm] DocumentAddDTO documentDto)
        {
            try
            {
                // Retrieve the existing document from the database
                var existingDocument = await context.Documents.FindAsync(documentDto.DocumentId = id);
                if (existingDocument == null)
                {
                    return NotFound(new { message = "Document not found." });
                }

                // Update the properties of the existing document
                existingDocument.Title = documentDto.Title;
                existingDocument.Description = documentDto.Description;
                existingDocument.TypeId = documentDto.TypeId;
                existingDocument.Status = documentDto.Status;
                existingDocument.CreatedAt = documentDto.CreatedAt;

                // Handle file upload if a new file is provided
                if (documentDto.FilePath != null && documentDto.FilePath.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AllFiles", documentDto.FilePath.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await documentDto.FilePath.CopyToAsync(stream);
                    }
                    existingDocument.FilePath = documentDto.FilePath.FileName;
                }

                // Save changes to the database
                context.Documents.Update(existingDocument);
                await context.SaveChangesAsync();

                return Ok(new { message = "Document updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            try
            {
                // Lấy tài liệu từ cơ sở dữ liệu bằng ID
                var document = await context.Documents.FindAsync(id);
                if (document == null)
                {
                    return NotFound(new { message = "Document not found." });
                }

                // Đường dẫn đến tệp tài liệu
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AllFiles", document.FilePath);

                // Kiểm tra nếu tệp tồn tại
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { message = "File not found." });
                }

                // Đọc nội dung tệp dưới dạng byte
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                // Xác định loại MIME dựa trên phần mở rộng của tệp
                var fileExtension = Path.GetExtension(filePath);
                var mimeType = GetMimeType(fileExtension);

                // Trả về tệp để người dùng tải xuống
                return File(fileBytes, mimeType, Path.GetFileName(document.FilePath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Hàm hỗ trợ để xác định MIME type dựa trên phần mở rộng tệp
        private string GetMimeType(string extension)
        {
            return extension.ToLower() switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream",
            };
        }





        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var documents = await documentRepository.GetDocumentById(id);
                if (documents == null)
                {
                    return NotFound(new { message = "Document not found." });
                }
                await documentRepository.DeleteDocument(id);
                return Ok(new { message = "Document deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet("SearchByTitle")]
        public async Task<List<DocumentDTO>> SearchByTitle(string title)
        {
            try
            {
                return await documentRepository.SearchByTitle(title);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("SortByDate")]
        public async Task<List<DocumentDTO>> SortbyDate(bool descending)
        {
            try
            {
                return await documentRepository.SortByDate(descending);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GroupByFileExtension")]
        public async Task<List<DocumentDTO>> GroupDocumentsByFileExtension()
        {
            try
            {
                return await documentRepository.GroupDocumentsByFileExtension();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






    }
}
