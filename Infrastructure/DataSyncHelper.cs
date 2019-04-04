using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSyncService
{
    public static class DataSyncHelper
    {
        public static async Task Sync(string ConnectionStringSource, string ConnectionStringDestination)
        {
            Policy retryPolicy = Policy.Handle<SqlException>().WaitAndRetry(
                                retryCount: 3,
                                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(1000));

            await retryPolicy.Execute(async () =>
             {
                 await ExecuteSyncCommand(ConnectionStringSource, ConnectionStringDestination);
             });
        }

        private static async Task ExecuteSyncCommand(string ConnectionStringSource, string ConnectionStringDestination)
        {
            using (var destinationConnection = new SqlConnection(ConnectionStringDestination))
            {
                await destinationConnection.OpenAsync();
                try
                {
                    var script = @$" INSERT INTO dbo.MasterSyncDb 
                                           (SyncDate
                                           ,WorkerName
                                           ,SourceDatabaseName)
                                     VALUES
                                           (@SyncDate,@WorkerName,@SourceDatabaseName)";

                    using (var destinationSql = new SqlCommand(script, destinationConnection))
                    {
                        using (var sourceConnection = new SqlConnection(ConnectionStringSource))
                        {
                            await sourceConnection.OpenAsync();

                            var SourceScript = $"SELECT GETDATE()";
                            using (var Sourcecommand = new SqlCommand(SourceScript, sourceConnection))
                            {
                                var SourceResult = await Sourcecommand.ExecuteReaderAsync();

                                while (await SourceResult.ReadAsync())
                                {
                                    destinationSql.Parameters.AddWithValue("@SyncDate", SourceResult[0]);
                                    destinationSql.Parameters.AddWithValue("@WorkerName", "");
                                    destinationSql.Parameters.AddWithValue("@SourceDatabaseName", "From Destination Db");
                                    await destinationSql.ExecuteNonQueryAsync();
                                }
                                destinationConnection.Close();
                                destinationConnection.Close();
                            }
                        }
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            };
        }
    }
}
