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
    public class Worker45 : BackgroundService
    {
        private readonly ILogger<Worker45> _logger;

        private readonly WorkerModel _Worker45Model;
        public Worker45(ILogger<Worker45> logger,
            WorkerModel _Worker45Model)
        {
            this._Worker45Model = _Worker45Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker45Model.Worker45))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker45Model.MasterDB, _Worker45Model.Worker45);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker45)} running at: {DateTime.Now}");

                await Task.Delay(_Worker45Model.Interval, stoppingToken);
            }
        }
    }
}
