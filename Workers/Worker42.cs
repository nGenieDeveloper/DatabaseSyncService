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
    public class Worker42 : BackgroundService
    {
        private readonly ILogger<Worker42> _logger;

        private readonly WorkerModel _Worker42Model;
        public Worker42(ILogger<Worker42> logger,
            WorkerModel _Worker42Model)
        {
            this._Worker42Model = _Worker42Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker42Model.Worker42))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker42Model.MasterDB, _Worker42Model.Worker42);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker42)} running at: {DateTime.Now}");

                await Task.Delay(_Worker42Model.Interval, stoppingToken);
            }
        }
    }
}
