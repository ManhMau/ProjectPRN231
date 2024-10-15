using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DAO
{
    public class DocTypeDAO
    {
        private readonly ProjectPRN231Context _context;
        private readonly IMapper _mapper;

        public DocTypeDAO(ProjectPRN231Context dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public async Task<List<DocTypeMapper>> GetCategories()
        {
            try
            {
                var listCategory = await _context.Types.ToListAsync();
                return _mapper.Map<List<DocTypeMapper>>(listCategory);
            }            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
