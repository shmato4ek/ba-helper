using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class DocumentService : BaseService
    {
        public DocumentService(BAHelperDbContext context, IMapper mapper) :
            base (context, mapper) { }

        public async Task<DocumentDTO> CreateDocument(int userId, NewDocumentDto newDocumentDto)
        {
            var documentEntity = _mapper.Map<DAL.Entities.Document>(newDocumentDto);
            documentEntity.UserId = userId;
            _context.Documents.Add(documentEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDTO>(documentEntity);
        }

        public async Task<DocumentDTO> AddProjectAim(int documentId, string projectAim)
        {
            var document = _context.Documents.FirstOrDefaultAsync(document => document.Id == documentId);
            if (document.Result == null)
            {
                return null;
            }
            else
            {
                document.Result.ProjectAim = projectAim;
            }
            await _context.SaveChangesAsync();
            var updatedDocument = _context.Documents.FirstOrDefaultAsync(document=> document.Id == documentId).Result;
            return _mapper.Map<DocumentDTO>(updatedDocument);
        }

        public async Task<List<DocumentDTO>> GetAllUsersDocumentsById(int userId)
        {
            var documentsEntities = await _context.Documents.Where(document => document.UserId == userId).ToListAsync();
            if (documentsEntities == null)
            {
                return null;
            }
            else
            {
                var documentsDTO = _mapper.Map<List<DocumentDTO>>(documentsEntities);
                return documentsDTO;
            }
        }

        public async Task<DocumentDTO> AddGlossary(NewGlossaryDTO newGlossaryDTO)
        {
            var documentEntity = await _context.Documents.FirstOrDefaultAsync(doc => doc.Id == newGlossaryDTO.DocumentId);
            if (documentEntity != null)
            {
                var glossaryEntity = _mapper.Map<Glossary>(newGlossaryDTO);
                if (documentEntity.Glossary == null)
                {
                    documentEntity.Glossary = new List<Glossary>();
                }
                documentEntity.Glossary.Add(glossaryEntity);
                _context.Documents.Update(documentEntity);
                await _context.SaveChangesAsync();
                var updatedDocument = await _context.Documents
                    .Where(doc => doc.Id == newGlossaryDTO.DocumentId)
                    .Include(doc => doc.Glossary)
                    .FirstOrDefaultAsync();
                return _mapper.Map<DocumentDTO>(updatedDocument);
            }
            return null;
        }
    }
}
