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
    public class Worker39 : BackgroundService
    {
        private readonly ILogger<Worker39> _logger;

        private readonly WorkerModel _Worker39Model;
        public Worker39(ILogger<Worker39> logger,
            WorkerModel _Worker39Model)
        {
            this._Worker39Model = _Worker39Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker39Model.Worker39))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker39Model.MasterDB, _Worker39Model.Worker39);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker39)} running at: {DateTime.Now}");

                await Task.Delay(_Worker39Model.Interval, stoppingToken);
            }
        }
    }
}
