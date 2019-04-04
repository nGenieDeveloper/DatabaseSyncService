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
    public class Worker44 : BackgroundService
    {
        private readonly ILogger<Worker44> _logger;

        private readonly WorkerModel _Worker44Model;
        public Worker44(ILogger<Worker44> logger,
            WorkerModel _Worker44Model)
        {
            this._Worker44Model = _Worker44Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker44Model.Worker44))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker44Model.MasterDB, _Worker44Model.Worker44);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker44)} running at: {DateTime.Now}");

                await Task.Delay(_Worker44Model.Interval, stoppingToken);
            }
        }
    }
}
