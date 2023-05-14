using BAHelper.BLL.Services;
using BAHelper.Common.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{documentId:int}")]
        public async Task<ActionResult> DownloadFile(int documentId)
        {
            var fileConfig = await _downloadService.DownloadDocument(documentId);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpPost("RACI")]
        public async Task<ActionResult> DownloadRaci(RaciMatrix raciMatrix)
        {
            var fileConfig = await _downloadService.DownloadRaci(raciMatrix);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpPost("CommunicationPlan")]
        public async Task<ActionResult> DownloadComPlan(List<CommunicationPlan> plan)
        {
            var fileConfig = await _downloadService.DownloadComPlan(plan);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }
    }
}
