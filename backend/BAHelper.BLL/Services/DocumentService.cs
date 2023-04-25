using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.UserStory;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Word;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
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
            var documentEntity = new DAL.Entities.Document();
            documentEntity.UserId = userId;
            documentEntity.ProjectAim = newDocumentDto.ProjectAim;
            documentEntity.Name = newDocumentDto.Name;

            _context.Documents.Add(documentEntity);
            await _context.SaveChangesAsync();

            documentEntity.Glossary = _mapper.Map<List<Glossary>>(newDocumentDto.Glossary);
            foreach(var glossary in documentEntity.Glossary)
            {
                glossary.DocumentId = documentEntity.Id;
            }
            await _context.SaveChangesAsync();
            foreach(var userStory in newDocumentDto.UserStories)
            {
                await AddUserStory(documentEntity.Id, userStory);
            }
            return _mapper.Map<DocumentDTO>(documentEntity);
        }

        public async Task<UserStoryDTO> AddUserStory(int documentId, NewUserStoryDTO newUserStory)
        {
            var documentEntity = await _context
                .Documents
                .Include(doc => doc.UserStories)
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (documentEntity is null)
            {
                return null;
            }
            var userStoryEntity = new UserStory { Name = newUserStory.Name, DocumentId = documentId };
            _context.UserStories.Add(userStoryEntity);
            await _context.SaveChangesAsync();
            if (userStoryEntity.AcceptanceCriterias == null)
            {
                userStoryEntity.AcceptanceCriterias = new List<AcceptanceCriteria>();
            }
            foreach(var criteria in newUserStory.AcceptanceCriterias)
            {
                criteria.UserStoryId = userStoryEntity.Id;
                userStoryEntity.AcceptanceCriterias.Add(_mapper.Map<AcceptanceCriteria>(criteria));
            }
            if (userStoryEntity.Formulas == null)
            {
                userStoryEntity.Formulas = new List<UserStoryFormula>();
            }
            foreach(var formula in newUserStory.UserStoryFormulas)
            {
                formula.UserStoryId = userStoryEntity.Id;
                userStoryEntity.Formulas.Add(_mapper.Map<UserStoryFormula>(formula));
            }
            _context.UserStories.Update(userStoryEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserStoryDTO>(userStoryEntity);
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
            var documentsEntities = await _context
                .Documents
                .Include(doc => doc.Glossary)
                .Where(document => document.UserId == userId)
                .ToListAsync();
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

        //public async Task<DocumentDTO> AddGlossary(NewGlossaryDTO newGlossaryDTO)
        //{
        //    var documentEntity = await _context
        //        .Documents
        //        .Include(doc => doc.Glossary)
        //        .FirstOrDefaultAsync(doc => doc.Id == newGlossaryDTO.DocumentId);
        //    if (documentEntity != null)
        //    {
        //        var glossaryEntity = _mapper.Map<Glossary>(newGlossaryDTO);
        //        if (documentEntity.Glossary == null)
        //        {
        //            documentEntity.Glossary = new List<Glossary>();
        //        }
        //        documentEntity.Glossary.Add(glossaryEntity);
        //        _context.Documents.Update(documentEntity);
        //        await _context.SaveChangesAsync();
        //        var updatedDocument = await _context.Documents
        //            .Where(doc => doc.Id == newGlossaryDTO.DocumentId)
        //            .Include(doc => doc.Glossary)
        //            .FirstOrDefaultAsync();
        //        return _mapper.Map<DocumentDTO>(updatedDocument);
        //    }
        //    return null;
        //}

        public async Task<DocumentDTO> DeleteDocument(int documentId, int userId)
        {
            var docEntity = await _context
                .Documents
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (docEntity != null) 
            {
                var userEntity = await _context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == userId);
                if(userEntity != null)
                {
                    if(userEntity.Id == docEntity.UserId)
                    {
                        _context.Documents.Remove(docEntity);
                        _context.SaveChanges();
                        return _mapper.Map<DocumentDTO>(docEntity);
                    }
                    return null;
                }
                return null;
            }
            return null;
        }
    }
}
