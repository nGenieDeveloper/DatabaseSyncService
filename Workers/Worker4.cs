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
    public class Worker4 : BackgroundService
    {
        private readonly ILogger<Worker4> _logger;

        private readonly WorkerModel _workerModel;
        public Worker4(ILogger<Worker4> logger,
            WorkerModel _workerModel)
        {
     
            this._workerModel = _workerModel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_workerModel.Worker4))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_workerModel.MasterDB, _workerModel.Worker4);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                }
                _logger.LogInformation($"{typeof(Worker4)} running at: {DateTime.Now}");

                await Task.Delay(_workerModel.Interval, stoppingToken);
            }
        }
    }
}
