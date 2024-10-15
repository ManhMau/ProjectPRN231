using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocTypeController : ControllerBase
    {
        public IDocTypeRepository docTypeRepository;
        public DocTypeController(IDocTypeRepository categoryRepository)
        {
            docTypeRepository = categoryRepository;
        }
        [HttpGet("GetAllCategories")]
        public async Task<List<DocTypeMapper>> GetAllCategories()
        {
            try
            {
                var result = await docTypeRepository.GetCategories();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
