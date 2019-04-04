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
    public class Worker11 : BackgroundService
    {
        private readonly ILogger<Worker11> _logger;

        private readonly WorkerModel _workerModel;
        public Worker11(ILogger<Worker11> logger,
            WorkerModel _workerModel)
        {
            this._workerModel = _workerModel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_workerModel.Worker11))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_workerModel.MasterDB, _workerModel.Worker11);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker11)} running at: {DateTime.Now}");

                await Task.Delay(_workerModel.Interval, stoppingToken);
            }
        }
    }
}
