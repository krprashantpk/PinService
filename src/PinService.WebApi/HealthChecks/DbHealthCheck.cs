using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PinService.WebApi.HealthChecks
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly string connectionstring;
        private readonly string sql;


        public DbHealthCheck(string connectionstring, string sql)
        {
            this.connectionstring = connectionstring ?? throw new ArgumentNullException(nameof(connectionstring));
            this.sql = sql ?? throw new ArgumentNullException(nameof(sql));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    await connection.OpenAsync(cancellationToken);
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        var _ = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                }
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {

                return new HealthCheckResult(context.Registration.FailureStatus, description: ex.Message, exception: ex);
            }


        }
    }
}
