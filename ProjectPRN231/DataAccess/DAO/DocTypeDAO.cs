using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DocTypeDAO
    {
        private readonly ProjectPRN231Context _context;
        private readonly IMapper _mapper;

        public DocTypeDAO(ProjectPRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy danh sách tất cả Types
        public async Task<List<DocTypeMapper>> GetAllDocTypesAsync()
        {
            try
            {
                var docTypes = await _context.Types.ToListAsync();
                return _mapper.Map<List<DocTypeMapper>>(docTypes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Types: " + ex.Message);
            }
        }

        // Lấy DocType theo ID
        public async Task<DocTypeMapper> GetDocTypeByIdAsync(int id)
        {
            try
            {
                var docType = await _context.Types.FindAsync(id);
                if (docType == null)
                {
                    throw new Exception("DocType not found");
                }
                return _mapper.Map<DocTypeMapper>(docType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving DocType: " + ex.Message);
            }
        }

        // Thêm mới DocType
        public async Task AddDocTypeAsync(DocTypeMapper docTypeMapper)
        {
            if (docTypeMapper == null) throw new ArgumentNullException(nameof(docTypeMapper));
            try
            {
                var docTypeEntity = _mapper.Map<BussinessObject.Models.Type>(docTypeMapper);
                await _context.Types.AddAsync(docTypeEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding DocType: " + ex.Message);
            }
        }

        // Cập nhật DocType
        public async Task UpdateDocTypeAsync(DocTypeMapper updateDocType)
        {
            if (updateDocType == null) throw new ArgumentNullException(nameof(updateDocType));
            try
            {
                var existingDocType = await _context.Types.FindAsync(updateDocType.Id);
                if (existingDocType != null)
                {
                    existingDocType.TypeName = updateDocType.TypeName;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("DocType not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating DocType: " + ex.Message);
            }
        }

        // Xóa DocType
        public async Task DeleteDocTypeAsync(int id)
        {
            try
            {
                var docType = await _context.Types.FindAsync(id);
                if (docType != null)
                {
                    _context.Types.Remove(docType);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("DocType not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting DocType: " + ex.Message);
            }
        }
        public async Task<List<DocTypeMapper>> SearchDocTypesByNameAsync(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException(nameof(typeName));
            try
            {
                var docTypes = await _context.Types
                    .Where(dt => dt.TypeName.Contains(typeName))
                    .ToListAsync();

                return _mapper.Map<List<DocTypeMapper>>(docTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DocTypeMapper>> GetDocTypesSortedByDateAsync(bool descending = true)
        {
            try
            {
                var docTypes = descending
                    ? await _context.Types.OrderByDescending(dt => dt.TypeName).ToListAsync()
                    : await _context.Types.OrderBy(dt => dt.TypeName).ToListAsync();

                return _mapper.Map<List<DocTypeMapper>>(docTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
