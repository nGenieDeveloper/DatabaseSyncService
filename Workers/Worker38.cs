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
    public class Worker38 : BackgroundService
    {
        private readonly ILogger<Worker38> _logger;

        private readonly WorkerModel _Worker38Model;
        public Worker38(ILogger<Worker38> logger,
            WorkerModel _Worker38Model)
        {
            this._Worker38Model = _Worker38Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker38Model.Worker38))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker38Model.MasterDB, _Worker38Model.Worker38);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker38)} running at: {DateTime.Now}");

                await Task.Delay(_Worker38Model.Interval, stoppingToken);
            }
        }
    }
}
