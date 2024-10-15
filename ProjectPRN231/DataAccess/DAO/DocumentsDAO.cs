using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DocumentsDAO
    {
        private readonly ProjectPRN231Context _context;
        private readonly IMapper mapper;
        public DocumentsDAO(ProjectPRN231Context context, IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
        }
        public async Task<List<DocumentDTO>> GetDocumentsAsync()
        {
            try
            {

                var documents = await _context.Documents
                    .Include(x => x.Type).ToListAsync();
                return mapper.Map<List<DocumentDTO>>(documents);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            try
            {
                var product = await _context.Documents
                    .Include(x => x.Type)
                    .FirstOrDefaultAsync(x => x.DocumentId == id);
                if (product == null)
                {
                    throw new Exception("Document not found");
                }
                return mapper.Map<DocumentDTO>(product);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddDocuments(DocumentAddDTO d)
        {
            if (d == null) throw new ArgumentNullException(nameof(d));
            try
            {
                var documentEntity = mapper.Map<Document>(d);
                await _context.Documents.AddAsync(documentEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task DeleteDocuments(int id)
        {
            try
            {
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);

                if (document != null)
                {
                    _context.Documents.Remove(document);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception(" Document not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task UpdateDocuments(DocumentAddDTO updateDocument)
        {
            if (updateDocument == null)
            {
                throw new ArgumentNullException(nameof(updateDocument));
            }
            try
            {
                var exitDocuments = await _context.Documents.FindAsync(updateDocument.DocumentId);
                if (exitDocuments != null)
                {
                    exitDocuments.Title = updateDocument.Title;
                    exitDocuments.Description = updateDocument.Description;
                    exitDocuments.FilePath = updateDocument.FilePath;
                    exitDocuments.CreatedAt = updateDocument.CreatedAt;
                    exitDocuments.TypeId = updateDocument.TypeId;
                    exitDocuments.Status = updateDocument.Status;

                    await _context.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
        public async Task<List<DocumentDTO>> SearchDocumentsByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            try
            {
                var documents = await _context.Documents.Include(x=>x.Type)
                    .Where(d => d.Title.Contains(title))
                    .ToListAsync();

                return mapper.Map<List<DocumentDTO>>(documents);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DocumentDTO>> GetDocumentsSortedByDateAsync(bool descending = true)
        {
            try
            {
                var documents = descending
                    ? await _context.Documents.OrderByDescending(d => d.CreatedAt).ToListAsync()
                    : await _context.Documents.OrderBy(d => d.CreatedAt).ToListAsync();

                return mapper.Map<List<DocumentDTO>>(documents);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
