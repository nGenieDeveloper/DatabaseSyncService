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
    public class Worker30 : BackgroundService
    {
        private readonly ILogger<Worker30> _logger;

        private readonly WorkerModel _Worker30Model;
        public Worker30(ILogger<Worker30> logger,
            WorkerModel _Worker30Model)
        {
            this._Worker30Model = _Worker30Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker30Model.Worker30))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker30Model.MasterDB, _Worker30Model.Worker30);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker30)} running at: {DateTime.Now}");

                await Task.Delay(_Worker30Model.Interval, stoppingToken);
            }
        }
    }
}
