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
    public class Worker43 : BackgroundService
    {
        private readonly ILogger<Worker43> _logger;

        private readonly WorkerModel _Worker43Model;
        public Worker43(ILogger<Worker43> logger,
            WorkerModel _Worker43Model)
        {
            this._Worker43Model = _Worker43Model;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrEmpty(_Worker43Model.Worker43))
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DataSyncHelper.Sync(_Worker43Model.MasterDB, _Worker43Model.Worker43);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation($"{typeof(Worker43)} running at: {DateTime.Now}");

                await Task.Delay(_Worker43Model.Interval, stoppingToken);
            }
        }
    }
}
