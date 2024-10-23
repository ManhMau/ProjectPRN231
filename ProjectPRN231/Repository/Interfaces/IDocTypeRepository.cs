using BussinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDocTypeRepository
    {
        Task<List<DocTypeMapper>> GetDocumentType();
        Task<DocTypeMapper> GetDocumentTypeById(int id);
        Task AddDocumentType(DocTypeMapper docTypeMapper);
        Task DeleteDocumentType(int id);
        Task UpdateDocumentType(DocTypeMapper docTypeMapper);
        Task<List<DocTypeMapper>> SearchByTitle(string title);
        Task<List<DocTypeMapper>> SortByName(bool descending);
    }
}
