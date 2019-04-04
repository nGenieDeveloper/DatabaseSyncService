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
    public class Worker47 : BackgroundService
    {
        private readonly ILogger<Worker47> _logger;

        private readonly WorkerModel _Worker47Model;
        public Worker47(ILogger<Worker47> logger,
            WorkerModel _Worker47Model)
        {
            this._Worker47Model = _Worker47Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker47Model.Worker47))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker47Model.MasterDB, _Worker47Model.Worker47);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker47)} running at: {DateTime.Now}");

                await Task.Delay(_Worker47Model.Interval, stoppingToken);
            }
        }
    }
}
