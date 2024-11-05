

using BussinessObject.DTOS;
using BussinessObject.Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<DocumentDTO>> GetListDocuments();
        Task<DocumentDTO> GetDocumentById(int id);

        Task DeleteDocument(int id);

        Task<List<DocumentDTO>> SearchByTitle(string title);
        Task<List<DocumentDTO>> SortByDate(bool descending);
        Task<List<DocumentDTO>> GroupDocumentsByFileExtension();
    }
}
