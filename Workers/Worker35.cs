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
    public class Worker35 : BackgroundService
    {
        private readonly ILogger<Worker35> _logger;

        private readonly WorkerModel _Worker35Model;
        public Worker35(ILogger<Worker35> logger,
            WorkerModel _Worker35Model)
        {
            this._Worker35Model = _Worker35Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker35Model.Worker35))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker35Model.MasterDB, _Worker35Model.Worker35);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker35)} running at: {DateTime.Now}");

                await Task.Delay(_Worker35Model.Interval, stoppingToken);
            }
        }
    }
}
