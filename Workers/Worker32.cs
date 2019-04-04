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
    public class Worker32 : BackgroundService
    {
        private readonly ILogger<Worker32> _logger;

        private readonly WorkerModel _Worker32Model;
        public Worker32(ILogger<Worker32> logger,
            WorkerModel _Worker32Model)
        {
            this._Worker32Model = _Worker32Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker32Model.Worker32))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker32Model.MasterDB, _Worker32Model.Worker32);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker32)} running at: {DateTime.Now}");

                await Task.Delay(_Worker32Model.Interval, stoppingToken);
            }
        }
    }
}
