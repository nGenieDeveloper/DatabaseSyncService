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
    public class Worker34 : BackgroundService
    {
        private readonly ILogger<Worker34> _logger;

        private readonly WorkerModel _Worker34Model;
        public Worker34(ILogger<Worker34> logger,
            WorkerModel _Worker34Model)
        {
            this._Worker34Model = _Worker34Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker34Model.Worker34))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker34Model.MasterDB, _Worker34Model.Worker34);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker34)} running at: {DateTime.Now}");

                await Task.Delay(_Worker34Model.Interval, stoppingToken);
            }
        }
    }
}
