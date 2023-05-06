using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.UserStory;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Word;
using ServiceStack;
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

            var glossaryEntity = _mapper.Map<List<Glossary>>(newDocumentDto.Glossary);
            foreach(var glossary in glossaryEntity)
            {
                glossary.DocumentId = documentEntity.Id;
            }
            documentEntity.Glossary = glossaryEntity;

            var userStoriesEntity = new List<UserStory>();
            foreach(var userStory in newDocumentDto.UserStories)
            {
                userStoriesEntity.Add(await AddUserStory(documentEntity.Id, userStory));
            }
            var createdDocument = await _context
                .Documents
                .Include(doc => doc.Glossary)
                .Include(doc => doc.UserStories)
                .FirstOrDefaultAsync(doc => doc.Id == documentEntity.Id);
            if (createdDocument is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document));
            }
            createdDocument.UserStories = userStoriesEntity;
            createdDocument.Glossary = glossaryEntity;
            _context.Documents.Update(createdDocument);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDTO>(createdDocument);
        }

        public async Task<UserStory> AddUserStory(int documentId, NewUserStoryDTO newUserStory)
        {
            var documentEntity = await _context
                .Documents
                .Include(doc => doc.UserStories)
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (documentEntity is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document), documentId);
            }
            var userStoryEntity = new UserStory { Name = newUserStory.Name, DocumentId = documentId };
            _context.UserStories.Add(userStoryEntity);
            await _context.SaveChangesAsync();
            var createdUserStory = await _context
                .UserStories
                .Include(story => story.AcceptanceCriterias)
                .Include(story => story.Formulas)
                .FirstOrDefaultAsync(story => story.Id == userStoryEntity.Id);

            if(createdUserStory is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document));
            }

            if (createdUserStory.AcceptanceCriterias == null)
            {
                createdUserStory.AcceptanceCriterias = new List<AcceptanceCriteria>();
            }
            foreach(var criteria in newUserStory.AcceptanceCriterias)
            {
                AcceptanceCriteria criteriaEntity = new AcceptanceCriteria();
                criteriaEntity.Text = criteria.Text;
                criteriaEntity.UserStoryId = userStoryEntity.Id;
                createdUserStory.AcceptanceCriterias.Add(criteriaEntity);
            }
            if (createdUserStory.Formulas == null)
            {
                createdUserStory.Formulas = new List<UserStoryFormula>();
            }
            foreach(var formula in newUserStory.UserStoryFormulas)
            {
                UserStoryFormula userStoryFormula = new UserStoryFormula();
                userStoryFormula.Text = formula.Text;
                userStoryFormula.UserStoryId = userStoryEntity.Id;
                createdUserStory.Formulas.Add(userStoryFormula);
            }
            _context.UserStories.Update(createdUserStory);
            await _context.SaveChangesAsync();
            return createdUserStory;
        }

        public async Task<List<DocumentDTO>> GetAllUsersDocumentsById(int userId)
        {
            var documentsEntities = await _context
                .Documents
                .Include(doc => doc.Glossary)
                .Where(document => document.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<DocumentDTO>>(documentsEntities);
        }
        public async Task<DocumentDTO> UpdateDocument(UpdateDocumentDTO updatedDocument, int userId)
        {
            var documentEntity = await _context
                .Documents
                .Include(doc => doc.UserStories)
                .Include(doc => doc.Glossary)
                .FirstOrDefaultAsync(doc => doc.Id == updatedDocument.Id);
            if (documentEntity is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document), updatedDocument.Id);
            }
            if (documentEntity.UserId != userId)
            {
                throw new NoAccessException(userId);
            }
            documentEntity.Name = updatedDocument.Name;
            documentEntity.ProjectAim = updatedDocument.ProjectAim;
            documentEntity.Glossary = _mapper.Map<List<Glossary>>(updatedDocument.Glossaries);
            documentEntity.UserStories = _mapper.Map<List<UserStory>>(updatedDocument.UserStories);
            _context.Documents.Update(documentEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDTO>(documentEntity);
        }

        public async System.Threading.Tasks.Task MoveToArchive(int documentId, int userId)
        {
            var docEntity = await _context
                .Documents
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (docEntity is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document), documentId);
            }
            if (docEntity.UserId != userId)
            {
                throw new NoAccessException(userId);
            }
            docEntity.IsDeleted = true;
            _context.Documents.Update(docEntity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteDocument(int documentId, int userId)
        {
            var docEntity = await _context
                .Documents
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (docEntity is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Document), documentId);
            }

            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            if (userEntity.Id != docEntity.UserId)
            {
                throw new NoAccessException(userId);
            }
            _context.Documents.Remove(docEntity);
            await _context.SaveChangesAsync();
        }
    }
}
