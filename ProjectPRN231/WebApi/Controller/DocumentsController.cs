using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IDocumentRepository documentRepository;
        public DocumentsController(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        [HttpGet("GetAllDocuments")]
        public async Task<List<DocumentDTO>> GetListDocuments()
        {
            try
            {
                return await documentRepository.GetListDocuments();
            }catch (Exception ex)
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
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("AddDocument")]
        public async Task<IActionResult> AddDocument([FromBody] DocumentAddDTO document)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await documentRepository.AddDocument(document);
                return Ok(new { message = "Documents added successfully." });
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentAddDTO document)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await documentRepository.UpdateDocument(document);
                return Ok(new { message = "Documents uppdated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var documents = await documentRepository.GetDocumentById(id);
                if(documents == null)
                {
                    return NotFound(new { message = "Document not found." });
                }
                await documentRepository.DeleteDocument(id);
                return Ok(new { message = "Document deleted successfully." });
            }catch (Exception ex)
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
            }catch (Exception ex)
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
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
