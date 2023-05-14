using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common;
using BAHelper.Common.DTOs;
using BAHelper.Common.Services;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class DownloadService : BaseService
    {
        private readonly WordService _wordService;
        public DownloadService(BAHelperDbContext context, IMapper mapper, WordService wordService)
            : base(context, mapper) 
        {
            _wordService = wordService;
        }

        public async Task<DownloadFileModel> DownloadDocument(int documentId)
        {
            await _wordService.CreateWordFile(documentId);
            var foundDocument = await _context
                .Documents
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (foundDocument is null)
            {
                throw new NotFoundException(nameof(Document), documentId);
            }

            string fileName = foundDocument.Name + ".docx";

            if (string.IsNullOrEmpty(fileName) || fileName == null)
            {
                throw new NotFoundException(nameof(Document));
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "LocalFiles", fileName);

            var memoryStream = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            var mimeType = (string file) =>
            {
                var mimeTypes = MimeTypes.GetMimeTypes();
                var extension = Path.GetExtension(file).ToLowerInvariant();
                return mimeTypes[extension];
            };
            DownloadFileModel result = new DownloadFileModel(memoryStream, mimeType(filePath), Path.GetFileName(filePath));
            return result;
        }

        public async Task<DownloadFileModel> DownloadRaci(RaciMatrix raciMatrix)
        {
            await _wordService.CreateRaciMatrixFile(raciMatrix);
            string fileName = "RaciMatrix.docx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "LocalFiles", fileName);
            var memoryStream = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            var mimeType = (string file) =>
            {
                var mimeTypes = MimeTypes.GetMimeTypes();
                var extension = Path.GetExtension(file).ToLowerInvariant();
                return mimeTypes[extension];
            };
            DownloadFileModel result = new DownloadFileModel(memoryStream, mimeType(filePath), Path.GetFileName(filePath));
            return result;
        }
    }
}
