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
    public class Worker50 : BackgroundService
    {
        private readonly ILogger<Worker50> _logger;

        private readonly WorkerModel _Worker50Model;
        public Worker50(ILogger<Worker50> logger,
            WorkerModel _Worker50Model)
        {
            this._Worker50Model = _Worker50Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker50Model.Worker50))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker50Model.MasterDB, _Worker50Model.Worker50);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker50)} running at: {DateTime.Now}");

                await Task.Delay(_Worker50Model.Interval, stoppingToken);
            }
        }
    }
}
