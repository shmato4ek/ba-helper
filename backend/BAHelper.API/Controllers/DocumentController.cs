using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        public DocumentController(DocumentService documentService) 
        {
            _documentService= documentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument(int userId, NewDocumentDto newDocumentDto)
        {
            DocumentDTO createdDocument = await _documentService.CreateDocument(userId, newDocumentDto);
            return Ok(createdDocument);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments(int userId)
        {
            List<DocumentDTO> documents = await _documentService.GetAllUsersDocumentsById(userId);
            return Ok(documents);
        }

        [HttpPut("ProjectAim")]
        public async Task<IActionResult> AddProjectAim(int documentId, string projectAim)
        {
            DocumentDTO updatedDocument = await _documentService.AddProjectAim(documentId, projectAim);
            return Ok(updatedDocument);
        }

        [HttpPut("AddGlossary")]
        public async Task<IActionResult> AddGlossary(NewGlossaryDTO newGlossary)
        {
            DocumentDTO updatedDocument = await _documentService.AddGlossary(newGlossary);
            return Ok(updatedDocument);
        }
    }
}
