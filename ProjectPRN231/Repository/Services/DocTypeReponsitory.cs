using BussinessObject.DTOS;
using BussinessObject.Models;
using DataAccess.DAO;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class DocTypeReponsitory : IDocTypeRepository
    {
        private readonly DocTypeDAO _docTypeDAO;
        public DocTypeReponsitory(DocTypeDAO docTypeDAO)
        {
            _docTypeDAO = docTypeDAO;
        }
        public async Task<List<DocTypeMapper>> GetCategories()
        {
            try
            {
                return await _docTypeDAO.GetCategories();
            }catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
