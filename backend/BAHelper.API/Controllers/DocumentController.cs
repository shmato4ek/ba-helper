using BAHelper.API.Extensions;
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
        public async Task<IActionResult> CreateDocument(NewDocumentDto newDocumentDto)
        {
            DocumentDTO createdDocument = await _documentService.CreateDocument(5, newDocumentDto);
            return Ok(createdDocument);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllDocuments(int userId)
        {
            return Ok(await _documentService.GetAllUsersDocumentsById(userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDocument([FromBody] UpdateDocumentDTO updatedDocument)
        {
            return Ok(await _documentService.UpdateDocument(updatedDocument));
        }

        [HttpPut("archive/{documentId:int}")]
        public async Task<ActionResult> MoveToArvhive(int documentId)
        {
            await _documentService.MoveToArchive(documentId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            await _documentService.DeleteDocument(documentId, this.GetUserIdFromToken());
            return NoContent();
        }
    }
}
