using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly WordService _wordService;
        private readonly DocumentService _documentService;

        public WordController(WordService wordService, DocumentService documentService) 
        {
            _wordService = wordService;
            _documentService = documentService;
        }

        [HttpGet("CreateDoc")]
        public async Task<IActionResult> CreateDoc(int userId, [FromBody] NewDocumentDto newDocument)
        {
            await _wordService.CreateDocument(userId, newDocument);
            return Ok();
        }

        [HttpGet("CreateWordFile")]
        public async Task<IActionResult> CreateWordFile(int documentId)
        {
            await _wordService.CreateWordFile(documentId);
            return Ok();
        }

        [HttpGet("Word")]
        public async Task<IActionResult> CreateWordFile(int documentId)
        {
            DocumentDTO documentDTO = await _documentService.CreateWordDocument(documentId);
            return Ok(documentDTO);
        }
    }
}
