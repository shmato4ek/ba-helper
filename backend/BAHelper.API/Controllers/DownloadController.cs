using BAHelper.BLL.Services;
using BAHelper.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly DownloadService _downloadService;

        public DownloadController(DownloadService downloadService)
        {
            _downloadService= downloadService;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(int documentId)
        {
            var fileConfig = await _downloadService.DownloadDocument(documentId);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

    }
}
