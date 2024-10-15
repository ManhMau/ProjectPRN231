

using BussinessObject.DTOS;
using BussinessObject.Models;

namespace Repository.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<DocumentDTO>> GetListDocuments();
        Task<DocumentDTO> GetDocumentById(int id);
        Task  AddDocument(DocumentAddDTO documentAddDTO);
        Task DeleteDocument(int id);
        Task UpdateDocument(DocumentAddDTO documentDTO);
        Task<List<DocumentDTO>> SearchByTitle(string title);
        Task<List<DocumentDTO>> SortByDate(bool descending);
    }
}
