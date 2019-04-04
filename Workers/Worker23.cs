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
    public class Worker23 : BackgroundService
    {
        private readonly ILogger<Worker23> _logger;

        private readonly WorkerModel _Worker23Model;
        public Worker23(ILogger<Worker23> logger,
            WorkerModel _Worker23Model)
        {
            this._Worker23Model = _Worker23Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker23Model.Worker23))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker23Model.MasterDB, _Worker23Model.Worker23);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker23)} running at: {DateTime.Now}");

                await Task.Delay(_Worker23Model.Interval, stoppingToken);
            }
        }
    }
}
