using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services.Backgound
{
    public class ScheduledService : IHostedService
    {
        private Timer? _timer = null;
        private readonly ProjectService _projectService;
        private readonly DocumentService _documentService;
        public ScheduledService(ProjectService projectService, DocumentService documentService) 
        {
            _documentService = documentService;
            _projectService = projectService;
        }
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            await _projectService.DeleteArchivedProjects();
            await _documentService.DeleteArchivedDocuments();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

    }
}
