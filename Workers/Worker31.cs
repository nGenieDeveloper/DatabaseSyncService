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
    public class Worker31 : BackgroundService
    {
        private readonly ILogger<Worker31> _logger;

        private readonly WorkerModel _Worker31Model;
        public Worker31(ILogger<Worker31> logger,
            WorkerModel _Worker31Model)
        {
            this._Worker31Model = _Worker31Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker31Model.Worker31))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker31Model.MasterDB, _Worker31Model.Worker31);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker31)} running at: {DateTime.Now}");

                await Task.Delay(_Worker31Model.Interval, stoppingToken);
            }
        }
    }
}
