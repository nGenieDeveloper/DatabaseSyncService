using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatabaseSyncService.Models;

namespace DatabaseSyncService.Workers
{
    public class Worker36 : BackgroundService
    {
        private readonly ILogger<Worker36> _logger;

        private readonly WorkerModel _Worker36Model;
        public Worker36(ILogger<Worker36> logger,
            WorkerModel _Worker36Model)
        {
            this._Worker36Model = _Worker36Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker36Model.Worker36))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker36Model.MasterDB, _Worker36Model.Worker36);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker36)} running at: {DateTime.Now}");

                await Task.Delay(_Worker36Model.Interval, stoppingToken);
            }
        }
    }
}
