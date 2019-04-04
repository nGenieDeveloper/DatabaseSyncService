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
    public class Worker46 : BackgroundService
    {
        private readonly ILogger<Worker46> _logger;

        private readonly WorkerModel _Worker46Model;
        public Worker46(ILogger<Worker46> logger,
            WorkerModel _Worker46Model)
        {
            this._Worker46Model = _Worker46Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker46Model.Worker46))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker46Model.MasterDB, _Worker46Model.Worker46);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker46)} running at: {DateTime.Now}");

                await Task.Delay(_Worker46Model.Interval, stoppingToken);
            }
        }
    }
}
