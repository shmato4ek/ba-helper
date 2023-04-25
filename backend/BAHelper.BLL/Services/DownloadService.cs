using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common;
using BAHelper.Common.DTOs;
using BAHelper.DAL.Context;
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
            var foundDocument = await _context.Documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (foundDocument == null)
            {
                return null;
            }
            else
            {
                string fileName = foundDocument.Name + ".docx";
                if (string.IsNullOrEmpty(fileName) || fileName == null)
                {
                    return null;
                }

                // get the filePath

                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "LocalFiles", fileName);

                // create a memorystream
                var memoryStream = new MemoryStream();

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                // set the position to return the file from
                memoryStream.Position = 0;

                // Get the MIMEType for the File
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
}
