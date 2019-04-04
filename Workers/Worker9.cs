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
    public class Worker9 : BackgroundService
    {
        private readonly ILogger<Worker9> _logger;

        private readonly WorkerModel _Worker9Model;
        public Worker9(ILogger<Worker9> logger,
            WorkerModel _Worker9Model)
        {
            this._Worker9Model = _Worker9Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker9Model.Worker9))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker9Model.MasterDB, _Worker9Model.Worker9);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker9)} running at: {DateTime.Now}");

                await Task.Delay(_Worker9Model.Interval, stoppingToken);
            }
        }
    }
}
