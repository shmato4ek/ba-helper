﻿using BAHelper.API.Extensions;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        public DocumentController(DocumentService documentService) 
        {
            _documentService= documentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDocument(NewDocumentDto newDocumentDto)
        {
            return Ok(await _documentService.CreateDocument(this.GetUserIdFromToken(), newDocumentDto));
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetAllDocuments()
        {
            return Ok(await _documentService.GetAllUsersDocumentsById(this.GetUserIdFromToken()));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDocument([FromBody] UpdateDocumentDTO updatedDocument)
        {
            return Ok(await _documentService.UpdateDocument(updatedDocument, this.GetUserIdFromToken()));
        }

        [HttpPut("archive")]
        public async Task<ActionResult> MoveToArvhive(int documentId)
        {
            await _documentService.MoveToArchive(documentId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDocument(int documentId)
        {
            await _documentService.DeleteDocument(documentId, this.GetUserIdFromToken());
            return NoContent();
        }
    }
}
