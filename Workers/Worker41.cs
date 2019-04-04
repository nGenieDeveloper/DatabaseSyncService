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
    public class Worker41 : BackgroundService
    {
        private readonly ILogger<Worker41> _logger;

        private readonly WorkerModel _Worker41Model;
        public Worker41(ILogger<Worker41> logger,
            WorkerModel _Worker41Model)
        {
            this._Worker41Model = _Worker41Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker41Model.Worker41))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker41Model.MasterDB, _Worker41Model.Worker41);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker41)} running at: {DateTime.Now}");

                await Task.Delay(_Worker41Model.Interval, stoppingToken);
            }
        }
    }
}
