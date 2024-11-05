using BussinessObject.DTOS;
using BussinessObject.Models;
using DataAccess.DAO;
using Repository.Interfaces;


namespace Repository.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentsDAO _documents;
        public DocumentRepository(DocumentsDAO documents)
        {
            _documents = documents;
        }



        public async Task DeleteDocument(int id)
        {
            try
            {
                await _documents.DeleteDocuments(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            try
            {
                return await _documents.GetDocumentById(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<DocumentDTO>> GetListDocuments()
        {
            try
            {
                return await _documents.GetDocumentsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<List<DocumentDTO>> GroupDocumentsByFileExtension()
        {
            try
            {
                return await _documents.GroupDocumentsByFileExtension();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DocumentDTO>> SearchByTitle(string title)
        {
            try
            {
                return await _documents.SearchDocumentsByTitleAsync(title);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<DocumentDTO>> SortByDate(bool descending)
        {
            try
            {
                return await _documents.GetDocumentsSortedByDateAsync(descending);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /* public async Task UpdateDocument(DocumentAddDTO documentDTO)
         {
             try
             {
                 await _documents.UpdateDocuments(documentDTO);
             }catch(Exception ex)
             {
                 throw new Exception();
             }
         }*/
    }
}
