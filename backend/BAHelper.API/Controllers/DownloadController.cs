using BAHelper.BLL.Services;
using BAHelper.BLL.Services.Cache;
using BAHelper.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly DownloadService _downloadService;
        private readonly InMemoryCache _cache;

        public DownloadController(DownloadService downloadService, InMemoryCache cache)
        {
            _downloadService= downloadService;
            _cache = cache;
        }

        [HttpGet("{documentId:int}")]
        public async Task<ActionResult> DownloadFile(int documentId)
        {
            var fileConfig = await _downloadService.DownloadDocument(documentId);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpPost("raci")]
        public async Task<ActionResult> DownloadRaci(RaciMatrix raciMatrix)
        {
            _cache.Raci = raciMatrix;
            var fileConfig = await _downloadService.DownloadRaci(raciMatrix);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpGet("raci")]
        public async Task<ActionResult> DownRaci()
        {
            var fileConfig = await _downloadService.DownloadRaci(_cache.Raci);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpPost("plan")]
        public async Task<ActionResult> DownloadComPlan(List<CommunicationPlan> plan)
        {
            _cache.CachedPlan = plan;
            var fileConfig = await _downloadService.DownloadComPlan(plan);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }

        [HttpGet("plan")]
        public async Task<ActionResult> DownPlan()
        {
            var fileConfig = await _downloadService.DownloadComPlan(_cache.CachedPlan);
            return File(fileConfig.MemoryStream, fileConfig.MimeType, fileConfig.FileName);
        }
    }
}
