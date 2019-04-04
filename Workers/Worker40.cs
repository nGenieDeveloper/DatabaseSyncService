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
    public class Worker40 : BackgroundService
    {
        private readonly ILogger<Worker40> _logger;

        private readonly WorkerModel _Worker40Model;
        public Worker40(ILogger<Worker40> logger,
            WorkerModel _Worker40Model)
        {
            this._Worker40Model = _Worker40Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker40Model.Worker40))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker40Model.MasterDB, _Worker40Model.Worker40);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker40)} running at: {DateTime.Now}");

                await Task.Delay(_Worker40Model.Interval, stoppingToken);
            }
        }
    }
}
