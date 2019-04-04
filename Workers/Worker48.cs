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
    public class Worker48 : BackgroundService
    {
        private readonly ILogger<Worker48> _logger;

        private readonly WorkerModel _Worker48Model;
        public Worker48(ILogger<Worker48> logger,
            WorkerModel _Worker48Model)
        {
            this._Worker48Model = _Worker48Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker48Model.Worker48))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker48Model.MasterDB, _Worker48Model.Worker48);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker48)} running at: {DateTime.Now}");

                await Task.Delay(_Worker48Model.Interval, stoppingToken);
            }
        }
    }
}
