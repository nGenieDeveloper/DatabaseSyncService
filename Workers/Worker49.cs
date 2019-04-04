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
    public class Worker49 : BackgroundService
    {
        private readonly ILogger<Worker49> _logger;

        private readonly WorkerModel _Worker49Model;
        public Worker49(ILogger<Worker49> logger,
            WorkerModel _Worker49Model)
        {
            this._Worker49Model = _Worker49Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker49Model.Worker49))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker49Model.MasterDB, _Worker49Model.Worker49);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker49)} running at: {DateTime.Now}");

                await Task.Delay(_Worker49Model.Interval, stoppingToken);
            }
        }
    }
}
