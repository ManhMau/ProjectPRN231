using BussinessObject.DTOS;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocTypeController : ControllerBase
    {
        private readonly IDocTypeRepository _docTypeRepository;

        public DocTypeController(IDocTypeRepository docTypeRepository)
        {
            _docTypeRepository = docTypeRepository;
        }

        [HttpGet("GetAllDocTypes")]
        public async Task<List<DocTypeMapper>> GetAllDocTypes()
        {
            try
            {
                return await _docTypeRepository.GetDocumentType();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetDocTypeById/{id}")]
        public async Task<DocTypeMapper> GetDocTypeById(int id)
        {
            try
            {
                return await _docTypeRepository.GetDocumentTypeById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddDocType")]
        public async Task<IActionResult> AddDocType([FromBody] DocTypeMapper docTypeMapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _docTypeRepository.AddDocumentType(docTypeMapper);
                return Ok(new { message = "DocType added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateDocType")]
        public async Task<IActionResult> UpdateDocType([FromBody] DocTypeMapper docTypeMapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _docTypeRepository.UpdateDocumentType(docTypeMapper);
                return Ok(new { message = "DocType updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteDocType/{id}")]
        public async Task<IActionResult> DeleteDocType(int id)
        {
            try
            {
                var docType = await _docTypeRepository.GetDocumentTypeById(id);
                if (docType == null)
                {
                    return NotFound(new { message = "DocType not found." });
                }
                await _docTypeRepository.DeleteDocumentType(id);
                return Ok(new { message = "DocType deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("SearchByName")]
        public async Task<List<DocTypeMapper>> SearchByName(string typeName)
        {
            try
            {
                return await _docTypeRepository.SearchByTitle(typeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("SortByTypeName")]
        public async Task<List<DocTypeMapper>> SortByTypeName(bool descending)
        {
            try
            {
                return await _docTypeRepository.SortByName(descending);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
