using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Document;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly JwtFactory _jwtFactory;
        private readonly DocumentService _documentService;
        public DocumentController(DocumentService documentService, JwtFactory jwtFactory) 
        {
            _documentService= documentService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDocument(NewDocumentDto newDocumentDto)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _documentService.CreateDocument(userId, newDocumentDto));
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetAllDocuments()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _documentService.GetAllUsersDocumentsById(userId));
        }

        [HttpGet("user/archive")]
        public async Task<ActionResult> GetArchivedDocuments()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _documentService.GetArcivedDocuments(userId));
        }

        [HttpPut("archive")]
        public async Task<ActionResult> MoveToArvhive(int documentId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _documentService.MoveToArchive(documentId, userId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDocument(int documentId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _documentService.DeleteDocument(documentId, userId);
            return NoContent();
        }
    }
}
