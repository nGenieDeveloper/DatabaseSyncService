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
    public class Worker37 : BackgroundService
    {
        private readonly ILogger<Worker37> _logger;

        private readonly WorkerModel _Worker37Model;
        public Worker37(ILogger<Worker37> logger,
            WorkerModel _Worker37Model)
        {
            this._Worker37Model = _Worker37Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker37Model.Worker37))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker37Model.MasterDB, _Worker37Model.Worker37);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker37)} running at: {DateTime.Now}");

                await Task.Delay(_Worker37Model.Interval, stoppingToken);
            }
        }
    }
}
