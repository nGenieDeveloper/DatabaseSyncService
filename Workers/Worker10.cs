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
    public class Worker10 : BackgroundService
    {
        private readonly ILogger<Worker10> _logger;

        private readonly WorkerModel _WorkerModel;
        public Worker10(ILogger<Worker10> logger,
            WorkerModel _WorkerModel)
        {
            this._WorkerModel = _WorkerModel;

            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_WorkerModel.Worker10))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_WorkerModel.MasterDB, _WorkerModel.Worker10);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker10)} running at: {DateTime.Now}");

                await Task.Delay(_WorkerModel.Interval, stoppingToken);
            }
        }
    }
}
