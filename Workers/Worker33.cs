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
    public class Worker33 : BackgroundService
    {
        private readonly ILogger<Worker33> _logger;

        private readonly WorkerModel _Worker33Model;
        public Worker33(ILogger<Worker33> logger,
            WorkerModel _Worker33Model)
        {
            this._Worker33Model = _Worker33Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker33Model.Worker33))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker33Model.MasterDB, _Worker33Model.Worker33);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker33)} running at: {DateTime.Now}");

                await Task.Delay(_Worker33Model.Interval, stoppingToken);
            }
        }
    }
}
