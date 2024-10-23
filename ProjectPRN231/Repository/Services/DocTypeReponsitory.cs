using BussinessObject.DTOS;
using DataAccess.DAO;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class DocTypeRepository : IDocTypeRepository
    {
        private readonly DocTypeDAO _docTypeDAO;

        public DocTypeRepository(DocTypeDAO docTypeDAO)
        {
            _docTypeDAO = docTypeDAO;
        }

        public async Task AddDocumentType(DocTypeMapper docTypeMapper)
        {
            try
            {
                await _docTypeDAO.AddDocTypeAsync(docTypeMapper);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding DocType: " + ex.Message);
            }
        }

        public async Task DeleteDocumentType(int id)
        {
            try
            {
                await _docTypeDAO.DeleteDocTypeAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DocType: " + ex.Message);
            }
        }

        public async Task<List<DocTypeMapper>> GetDocumentType()
        {
            try
            {
                return await _docTypeDAO.GetAllDocTypesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving DocTypes: " + ex.Message);
            }
        }

        public async Task<DocTypeMapper> GetDocumentTypeById(int id)
        {
            try
            {
                return await _docTypeDAO.GetDocTypeByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving DocType: " + ex.Message);
            }
        }

        public async Task<List<DocTypeMapper>> SearchByTitle(string title)
        {
            try
            {
                return await _docTypeDAO.SearchDocTypesByNameAsync(title);
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching DocTypes: " + ex.Message);
            }
        }

        public async Task<List<DocTypeMapper>> SortByName(bool descending)
        {
            try
            {
                return await _docTypeDAO.GetDocTypesSortedByDateAsync(descending);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sorting DocTypes: " + ex.Message);
            }
        }

        public async Task UpdateDocumentType(DocTypeMapper docTypeMapper)
        {
            try
            {
                await _docTypeDAO.UpdateDocTypeAsync(docTypeMapper);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating DocType: " + ex.Message);
            }
        }
    }
}
